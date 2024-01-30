using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台座の移動
/// </summary>
public class MoveStand : MonoBehaviour
{
	//奥に移動
	private bool _isBack = false;
	private Rigidbody rigidbody;

	void Start(){
		rigidbody = this.GetComponent<Rigidbody>();
	}

	//Update処理は毎回呼ばれる
	void FixedUpdate()
	{
		//今現在の台の位置をpositionへ格納
		//var position = transform.localPosition;
		if (transform.localPosition.y == -2.0)
		{
			if (_isBack)
			{
				//奥に移動
				rigidbody.MovePosition(this.transform.position + new Vector3(0, 0, Time.deltaTime * 2));//Time.deltaTime * 2 1秒あたりにポジションが２動く
			}
			else
			{
				//手前に移動
				rigidbody.MovePosition(this.transform.position - new Vector3(0, 0, Time.deltaTime * 2));
			}
			if (this.transform.position.z >= 7)
			{
				//ある程度奥に行ったら奥に行くフラグをオフ
				_isBack = false;
			}
			else if (this.transform.position.z <= 4)
			{
				//ある程度手前なら奥に行くフラグをオン
				_isBack = true;
			}
			//if文で変更した移動位置を台座の位置に適応(やらないとMoveStandは動かない)
			//transform.localPosition = position;
		}
		
	}

	
}