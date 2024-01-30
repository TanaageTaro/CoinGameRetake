using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// コインの挙動
/// </summary>
public class CoinObject : MonoBehaviour
{
	//消滅エフェクトの追加
	[SerializeField] private GameObject prticle;
	[SerializeField] private GameObject Darkprticle;
	
	//SEの宣言
	[SerializeField] private AudioClip CoinDrop;//コインが台座に落ちたときのSE

	private GameObject gameManager;//GameManagerの宣言
	private GameObject Under;//UnderStandの宣言
	private string skullTag = "RedSkull";//動く赤い骸骨のタグ
	private string MoveTag = "UnderStand";//下の台座のタグ
	private string UnderTag = "MoveStand";//動く台座のタグ
	private AudioSource audioSource;//オーディオソース

	void Start(){
		//オーディオソース取得
        audioSource = this.gameObject.GetComponent<AudioSource>();
		//GameManagerの取得
		gameManager = GameObject.Find("GameManager");
		//下の台座オブジェクトの取得
		Under = GameObject.Find("UnderStand");
	}
	//Update処理は毎回呼ばれる
	void Update()
	{	
		//コインがマグマに落ちたらポイント+1
		//一定以下の高さになった場合
		if (transform.localPosition.y <= -4.5)
		{
			
			//エフェクトの再生
			var effectPlay = (GameObject)Instantiate(prticle, this.transform.position, Quaternion.identity);
			//オブジェクト削除
			Destroy(gameObject);
			//下の台座がDestroyされていない時だけスコアの加算をgameManagerから呼び出し
			if (Under != null) gameManager.GetComponent<GameManager>().ScoreGet(1);	
		}
	}

	//赤い骸骨とぶつかった場合はポイント-１
	void OnCollisionEnter(Collision collision)//OnColliderメソッドはUpdateメソッド内に記述すると正しく動作しない。
    {
       	// 衝突したオブジェクトが指定したタグを持っているか確認
        if (collision.gameObject.CompareTag(skullTag))
        {
			//エフェクトの再生
			var effectPlay = (GameObject)Instantiate(Darkprticle, this.transform.position, Quaternion.identity);
            // 衝突した相手が指定したタグを持っている場合、自身を破棄
            Destroy(gameObject);
			//下の台座がDestroyされていない時だけスコアの減算をgameManagerから呼び出し
			if (Under != null) gameManager.GetComponent<GameManager>().ScoreGet(-1);	
        }
		// 衝突したオブジェクトが指定したタグを持っているか確認
        if (collision.gameObject.CompareTag(MoveTag) || collision.gameObject.CompareTag(UnderTag))
		{
			//コインが台座に落ちたときのSE再生
			audioSource.PlayOneShot(CoinDrop);
		}
    }
}
