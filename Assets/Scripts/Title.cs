using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title : MonoBehaviour
{
    
    // Meinシーンの宣言
    private string targetSceneName = "Main";
    [SerializeField] private AudioClip TapSE;//コインが燃えて消えるときのSE
    private AudioSource audioSource;//オーディオソース

    void Start()
	{
		//オーディオソース取得
        audioSource = this.gameObject.GetComponent<AudioSource>();
	}

    void Update()
    {
        // タップされたらSceneを切り替える
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Rayがオブジェクトにヒットした場合
                if (hit.collider != null)
                {
                    audioSource.PlayOneShot(TapSE);
                    //SEが鳴って5秒後にゲームシーンへ移行
                    StartCoroutine(SpawnWithDelay(targetSceneName, 3.0f));
                }
            }
        }
    }

    IEnumerator SpawnWithDelay(string Scene,float delay)
    {   //delay秒の待ち
        yield return new WaitForSeconds(delay);
        // Sceneを切り替える
        SceneManager.LoadScene(Scene);
        yield return null;
    }
}
