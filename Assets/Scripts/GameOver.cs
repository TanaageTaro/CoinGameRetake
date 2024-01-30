using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip EvilSmile;
    [SerializeField] private AudioClip GameOverSe;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject TapText;
    [SerializeField] private GameObject BackTitle;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnEvilSmile());
        StartCoroutine(SpawnGameOverAndTap());
    }

    IEnumerator SpawnEvilSmile()
    {
        while (true)
        {
            audioSource.PlayOneShot(EvilSmile);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnGameOverAndTap()
    {
        yield return new WaitForSeconds(2.0f);
        audioSource.PlayOneShot(GameOverSe);
        var gameOverObject = Instantiate(GameOverText, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        var tapObject = Instantiate(TapText, transform.position, Quaternion.identity);
        var backTitleObject = Instantiate(BackTitle, transform.position, Quaternion.identity);
    }
}
