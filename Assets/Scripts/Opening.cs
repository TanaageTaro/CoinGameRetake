using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    //BGMの宣言
    [SerializeField] private AudioClip OpeningBGM;//タイトルのBGM
    [SerializeField] private GameObject TapText;//Please tap here!!と書かれたテキスト
    [SerializeField] private GameObject playstart;//ゲーム画面に進むプレーンオブジェクト
     private AudioSource audioSource;//オーディオソース
    // Start is called before the first frame update
    void Start()
    {
        //オーディオソース取得、BGM再生
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.clip = OpeningBGM;
        audioSource.Play();

        //タップするとゲーム画面に進む透明な平面が最前面に出現
        StartCoroutine(SpawnWithDelay(playstart, this.transform.position, Quaternion.identity, 5.0f));
    }

    IEnumerator SpawnWithDelay(GameObject obj, Vector3 position, Quaternion rotation, float delay)
    {   //delay秒の待ち
        yield return new WaitForSeconds(delay);
        //タップするとゲーム画面に進む平面出現
        var spawnedObject = Instantiate(obj, position, rotation);
        //Please tap here!!というテキスト出現
        var Tap = (GameObject)Instantiate(TapText, this.transform.position, Quaternion.identity);
        yield return null;
    }
}
