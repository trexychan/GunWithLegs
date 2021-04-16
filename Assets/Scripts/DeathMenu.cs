using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    IEnumerator delayedAppearance()
    {
        yield return new WaitForSeconds(1f);
        deathscreen.SetActive(true);
        gameObject.GetComponent<AudioSource>().clip = deathnoise;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
