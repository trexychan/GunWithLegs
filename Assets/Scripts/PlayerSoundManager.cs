using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource footstep;
    public AudioClip[] pistol_fire;
    public AudioClip[] shotgun_fire;
    public StatePlayerController player;
    public float gun_volume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (player.isGrounded && player.getVelocity().magnitude > 2f && !footstep.isPlaying)
        // {
        //     footstep.volume = Random.Range(0.8f, 1);
        //     footstep.pitch = Random.Range(0.8f, 1.1f);
        //     footstep.Play();
        // }
    }

    public void PlayPistolFire()
    {
        AudioSource tmp = gameObject.AddComponent<AudioSource>();
        tmp.clip = pistol_fire[0];
        tmp.volume = gun_volume;
        tmp.Play();
        Destroy(tmp,tmp.clip.length);
    }

    public void PlayShotgunFire()
    {
        AudioSource tmp = gameObject.AddComponent<AudioSource>();
        tmp.clip = shotgun_fire[0];
        tmp.volume = gun_volume;
        tmp.Play();
        Destroy(tmp,tmp.clip.length);
    }
}
