using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathscreen;
    public AudioClip deathnoise;
    private AudioSource[] allAudio;
    // Start is called before the first frame update
    void Start()
    {
        allAudio = FindObjectsOfType<AudioSource>();
    }

    public void playDeathScreen()
    {
        foreach (var src in allAudio)
        {
            src.Stop();
        }
        StartCoroutine(delayedAppearance());
        StartCoroutine(restartGame());
    }

    IEnumerator delayedAppearance()
    {
        yield return new WaitForSeconds(1f);
        deathscreen.SetActive(true);
        gameObject.GetComponent<AudioSource>().clip = deathnoise;
        gameObject.GetComponent<AudioSource>().Play();
    }

    IEnumerator restartGame() {
        yield return new WaitForSeconds(4f);
        Destroy(PlayerData.Instance);
        SceneManager.LoadScene(0);
    }
}
