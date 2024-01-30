using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台座の移動
/// </summary>
public class MoveSkull : MonoBehaviour
{
    //奥に移動
    private bool _isBack = false;
    //右に移動
    private bool _isRight = false;
	//前後か左右どちらかの頻度の割り振り（大きいほど左右移動の割合が増える。２以上で設定）
	[SerializeField] private int rndLength = 4;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    //Update処理は毎回呼ばれる
    void FixedUpdate()
    {
		int rnd = UnityEngine.Random.Range(0, rndLength);//0~2の間でランダムな整数を代入

    	if(rnd == 0 || rnd == 1) MoveForwardBackward();//前後に移動
    	else if(rnd == 2) MoveLeftRight();//左右に移動
		else if(rnd == 3) MoveForwardBackwardShort();//前後に小さく早く移動
		else MoveLeftRightShort();//左右に小さく早く移動
    }

    void MoveForwardBackward()
    {
        if (_isBack)
        {
            //奥に移動
            rigidbody.MovePosition(this.transform.position + new Vector3(0, 0, Time.deltaTime * 3));
        }
        else
        {
            //手前に移動
            rigidbody.MovePosition(this.transform.position - new Vector3(0, 0, Time.deltaTime * 3));
        }

        if (this.transform.position.z >= 6.5)
        {
            //ある程度奥に行ったら奥に行くフラグをオフ
            _isBack = false;
        }
        else if (this.transform.position.z <= -3.5)
        {
            //ある程度手前なら奥に行くフラグをオン
            _isBack = true;
        }
    }

    void MoveLeftRight()
    {
        if (_isRight)
        {
            //右に移動
            rigidbody.MovePosition(this.transform.position + new Vector3(Time.deltaTime * 3, 0, 0));
        }
        else
        {
            //左に移動
            rigidbody.MovePosition(this.transform.position - new Vector3(Time.deltaTime * 3, 0, 0));
        }

        if (this.transform.position.x >= 1.85)
        {
            //ある程度右に行ったら奥に行くフラグをオフ
            _isRight = false;
        }
        else if (this.transform.position.x <= -1.89)
        {
            //ある程度手前なら奥に行くフラグをオン
            _isRight = true;
        }
    }

	void MoveForwardBackwardShort()
    {
        if (_isBack)
        {
            //奥に移動
            rigidbody.MovePosition(this.transform.position + new Vector3(0, 0, Time.deltaTime * 10));
        }
        else
        {
            //手前に移動
            rigidbody.MovePosition(this.transform.position - new Vector3(0, 0, Time.deltaTime * 10));
        }

        if (this.transform.position.z >= 4.0)
        {
            //ある程度奥に行ったら奥に行くフラグをオフ
            _isBack = false;
        }
        else if (this.transform.position.z <= 0)
        {
            //ある程度手前なら奥に行くフラグをオン
            _isBack = true;
        }
    }

    void MoveLeftRightShort()
    {
        if (_isRight)
        {
            //右に移動
            rigidbody.MovePosition(this.transform.position + new Vector3(Time.deltaTime * 10, 0, 0));
        }
        else
        {
            //左に移動
            rigidbody.MovePosition(this.transform.position - new Vector3(Time.deltaTime * 10, 0, 0));
        }

        if (this.transform.position.x >= 0.85)
        {
            //ある程度右に行ったら奥に行くフラグをオフ
            _isRight = false;
        }
        else if (this.transform.position.x <= -0.2)
        {
            //ある程度手前なら奥に行くフラグをオン
            _isRight = true;
        }
    }
}
