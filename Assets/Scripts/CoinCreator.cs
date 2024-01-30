using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインの作成を行う
/// </summary>
public class CoinCreator : MonoBehaviour
{
	//"[SerializeField] private"と”public”の違いは他のクラスから書き換えできないか、できるかの違い
	//コピーする元となるコイン
	[SerializeField] private GameObject[] _originalCoin;//←他のクラスから書き換えできないが、Inspecterから数値変更可能。
	
	void Start() {
		//_originalCoin = new GameObject[3];
	}
	//Update処理は毎回呼ばれる
	void Update()
	{
		//マウス左（０）クリックをした　右（１）真ん中（２）
		if (Input.GetMouseButtonDown(0))
		{
			//クリック地点からレイを作成する
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			int rnd = Random.Range(0, 3);//0~2の間でランダムな整数を代入
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
				position.y += 5;
				//インスペクターで設定したコインの生成
				var objectBase = (GameObject)Instantiate(_originalCoin[rnd]);
				//算出した位置を設定
				objectBase.gameObject.transform.localPosition = position;
				//有効化する
				objectBase.gameObject.SetActive(true);
			}
		}
	}
}
