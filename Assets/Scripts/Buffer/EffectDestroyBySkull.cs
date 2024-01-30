using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// effectの挙動
/// </summary>

public class EffectDestroyBySkull : MonoBehaviour
{
	[SerializeField] private AudioClip CoinVanish;//コインが消えるときのSE
	private bool hasStartedFadeOut = false;
	private AudioSource audioSource;//オーディオソース
	void Start()
	{
		//オーディオソース取得
        audioSource = this.gameObject.GetComponent<AudioSource>();
		//コインが消えるSE再生
		audioSource.PlayOneShot(CoinVanish);
	}
	
	//Update処理は毎回呼ばれる
	void Update()
	{
		// フェードアウトが開始されていなければ開始
        if (!hasStartedFadeOut)
        {
            StartCoroutine(DestroyWithFadeOut());
            hasStartedFadeOut = true;
        }
		
		
		//オブジェクト削除
		Destroy(gameObject,3.0f);
	}

	 IEnumerator DestroyWithFadeOut()
    {
        float duration = 0.5f;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // オーディオのボリュームを徐々に下げてフェードアウト
            audioSource.volume = 1.0f - (timer / duration);

            yield return null;
        }
    }
}
