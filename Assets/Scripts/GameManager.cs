using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject textScore;//スコアのテキスト
    [SerializeField] private GameObject textCoin;//コインのテキスト
    [SerializeField] private GameObject explotion;//クリアエフェクト
    [SerializeField] private GameObject flame;//クリアエフェクト

    //SEの宣言
    [SerializeField] private AudioClip explotionEnd;//最後に爆発するときのSE

    //BGMの宣言
    [SerializeField] private AudioClip playBGM;//ゲーム中のBGM
    [SerializeField] private AudioClip endBGM;//ゲーム終了後のBGM



    [SerializeField] private GameObject playback;//タイトル画面に戻すプレーンオブジェクト

    //"[SerializeField] private"と”public”の違いは他のクラスから書き換えできないか、できるかの違い
	//コピーする元となるコイン
	[SerializeField] private GameObject[] _originalCoin;//←他のクラスから書き換えできないが、Inspecterから数値変更可能。
    [SerializeField] private GameObject GameText;//Gameと書かれたテキスト
    [SerializeField] private GameObject ClearText;//Clear!!と書かれたテキスト
    [SerializeField] private GameObject TapText;//Please tap here!!と書かれたテキスト

    //IsKinematicでバラバラにするオブジェクト宣言
    [SerializeField] private GameObject wall;//wallオブジェクト
    [SerializeField] private GameObject under;//UnderStandオブジェクト
    [SerializeField] private GameObject move;//MoveStandオブジェクト
    [SerializeField] private GameObject StaffRight;//StaffOfPainRightオブジェクト
    [SerializeField] private GameObject StaffLeft;//StaffOfPainLeftオブジェクト
    [SerializeField] private GameObject skull;//RedSkullオブジェクト
    //IsKinematicを操作するオブジェクトをRendererとして配列に格納
    //オブジェクトの不透明度を後で操作するため、各オブジェクトのRendererコンポーネント（オブジェクトの描写関連）を格納する。
    //通常はありえないが、万が一オブジェクトにRendererコンポーネントがアタッチされていなければnullを返してしまうので注意。
    private Renderer[] Kinematic;
    private int score = 0;//現在のスコア
    [SerializeField] private int ClearScore = 100;//レベルアップに必要なスコア
    [SerializeField] private int correntCoin = 200;//残りのコイン数
    [SerializeField] private  float fadeSpeed = 0.5f; // 爆発後の不透明度のフェードの速さ
    private string targetSceneName = "GameOver";
    private AudioSource audioSource;//オーディオソース

   
    
    // Start is called before the first frame update
    void Start()
    {
         // ゲームオブジェクトのRendererコンポーネントを取得
        Kinematic = new Renderer[] {
            wall.GetComponent<Renderer>(),
            under.GetComponent<Renderer>(),
            move.GetComponent<Renderer>(),
            StaffRight.GetComponent<Renderer>(),
            StaffLeft.GetComponent<Renderer>()
        };
        //オーディオソース取得
        audioSource = this.gameObject.GetComponent<AudioSource>();

        //プレイ中のBGMを再生
        playingBGM();
        //残コイン数表示
        RefreshCoinText();
        //スコア表示
        RefreshScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        //クリックしたら残りコイン数が減る
        if(score < ClearScore) CoinCreate();
        
        //残コイン数表示
        RefreshCoinText();

        if (correntCoin == 0) SceneManager.LoadScene(targetSceneName);
    }
    
    public void ScoreGet(int GetScore){
        if(score < ClearScore){
            score += GetScore; 
        }
        //スコア表示
        RefreshScoreText();
        
        //クリア時の演出
        if (score == ClearScore) {
            ClearEffect();
            //ゲームクリア表記
            GameText.GetComponent<TextMeshProUGUI>().text = "Game";
            ClearText.GetComponent<TextMeshProUGUI>().text = "Clear!!";
        }
        
    }

    //クリックしたらGold,Silver,Copperの内のどれかのコインが出現する。
    void CoinCreate()
    {
        //マウス左（０）クリックをした　右（１）真ん中（２）
		if (Input.GetMouseButtonDown(0))
		{
			//クリック地点からレイを作成する
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int rnd = UnityEngine.Random.Range(0, 3);//0~2の間でランダムな整数を代入
			//レイの先にオブジェクト（壁など）がある時
			if (Physics.Raycast(ray, out hit, 500))//ray 左クリックした座標を　out hit ヒットに出力してください　 500 の範囲内で
			{
				//コインの生成位置の算出
				//カメラの座標からレイが当たったポイントまでの距離
				var distance = Vector3.Distance(Camera.main.transform.position, hit.point);
				//クリック地点
				//ｘ，ｙ座標はマウスの有る場所、ｚ座標はレイが当たったポイント
				var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
				//クリック地点をスクリーン座標⇛ワールド座標に変換　
				//ワールド座標：３D空間における（ｘ，ｙ、ｚ）座標　スクリーン座標：画面における（ｘ，ｙ）座標　ローカル座標：オブジェクト基準の（ｘ，ｙ，ｚ）座標
				//よってスクリーン座標⇛ワールド座標に変換しないと
				var position = Camera.main.ScreenToWorldPoint(mousePosition);
				//少し高い位置に生成
				position.y += 10;
				//インスペクターで設定したコインの生成
				var objectBase = (GameObject)Instantiate(_originalCoin[rnd]);
				//算出した位置を設定
				objectBase.gameObject.transform.localPosition = position;
				//有効化する
				objectBase.gameObject.SetActive(true);
                correntCoin--;
			}
		}
    }

    void RefreshScoreText(){
        //あと何点取ったらクリアか表記する。
        textScore.GetComponent<TextMeshProUGUI>().text = "Score : " + score + "/" + ClearScore;
    }

    void RefreshCoinText(){
        //コインが残り何枚か表記する。
        textCoin.GetComponent<TextMeshProUGUI>().text = "Coin  : " + correntCoin;
    }

    void ClearEffect()
    {
        //クリア時、BGMなどのボリュームを半分にする。
        audioSource.volume = 0.5f;
        //爆発時にSEを全長通りに再生、爆発音のみボリュームをMAX
        audioSource.PlayOneShot(explotionEnd, 1.0f);
        //クリア時に爆発エフェクトを出現
	    var effectPlay = (GameObject)Instantiate(explotion, this.transform.position, Quaternion.identity);
        Destroy(effectPlay,2.0f);
       

        // ゲームクリア後のBGMを2.0秒後に再生する
        //Invoke("PlayEndBGM", 5.0f);

        //台座などのオブジェクトのIsKinematicを外して吹き飛ばす
        // Rigidbodyコンポーネントを取得（ない場合はアタッチする）
        Rigidbody[] allRigidbody = {
            wall.GetComponent<Rigidbody>(),
            under.GetComponent<Rigidbody>(),
            move.GetComponent<Rigidbody>(),
            StaffRight.GetComponent<Rigidbody>(),
            StaffLeft.GetComponent<Rigidbody>()
        };

        for (int i = 0; i < allRigidbody.Length; i++)
        {
            if (allRigidbody[i] == null)
            {
                // Rigidbodyがアタッチされていない場合、アタッチする
                allRigidbody[i] = allRigidbody[i].gameObject.AddComponent<Rigidbody>();
            }

            // IsKinematicを無効化
            allRigidbody[i].isKinematic = false;
            // useGravityを有効化
            //allRigidbody[i].useGravity = true;
        }
        // オブジェクトを徐々に薄くする処理
        for (int i = 0; i < Kinematic.Length; i++)
        {
            StartCoroutine(FadeObject(Kinematic[i]));
        }
        Destroy(skull);
        //オブジェクトを燃やすエフェクト
        var Flame = (GameObject)Instantiate(flame, new Vector3(0f, -4.0f, 1.2f), Quaternion.identity);
        //タップするとタイトル画面に戻る透明な平面が最前面に出現
        StartCoroutine(SpawnWithDelay(playback, this.transform.position, Quaternion.identity, 5.0f));
    }

    // オブジェクトを徐々に薄くするコルーチン
    IEnumerator FadeObject(Renderer renderer)
    {
        Color objectColor = renderer.material.color;

         while (objectColor.a > 0.1f)
        {
            // 不透明度を徐々に下げる
            objectColor.a -= fadeSpeed * (Time.deltaTime / 2.0f);

            // ゲームオブジェクトに新しい色を適用
        if (renderer != null) // もしRendererがnullでなければ色を設定
        {
            renderer.material.color = objectColor;
        }

            yield return null;
        }
        // オブジェクトを消す前に再度確認
        if (renderer != null)
        {
            Destroy(renderer.gameObject);
        }
    }

    IEnumerator SpawnWithDelay(GameObject obj, Vector3 position, Quaternion rotation, float delay)
    {   //delay秒の待ち
        yield return new WaitForSeconds(delay);
        //タップするとタイトルに戻る平面出現
        var spawnedObject = Instantiate(obj, position, rotation);
        //Please tap here!!というテキスト出現
        var Tap = (GameObject)Instantiate(TapText, this.transform.position, Quaternion.identity);
        yield return null;
    }

    void playingBGM()
    {
        audioSource.clip = playBGM;
        audioSource.Play();
    }

    void PlayEndBGM()
    {
        audioSource.clip = endBGM;
        audioSource.Play();
    }
}
