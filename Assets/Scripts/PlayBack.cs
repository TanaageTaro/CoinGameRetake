using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Openingシーンの宣言
    private string targetSceneName = "Opening";

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
                    // ここでオブジェクトがタップされた場合の処理を追加できます

                    // Sceneを切り替える
                    SceneManager.LoadScene(targetSceneName);
                }
            }
        }
    }
}
