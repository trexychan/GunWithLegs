using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public EventSound eventSoundPrefab;

    public AudioClip gunAddedAudio;
    public AudioClip itemAddedAudio;
    public AudioClip playerDamagedAudio;
    public AudioClip playerSoftLandAudio;
    public AudioClip playerHardLandAudio;
    public AudioClip playerDeathAudio;
    public AudioClip playerDashAudio;
    public AudioClip playerSwitchAudio;
    public AudioClip[] playerFootStepAudio = null;

    // fire sounds for each gun in the game~~~
    public AudioClip pistolFireAudio;
    public AudioClip shotgunFireAudio;
    public AudioClip rpgFireAudio;
    public AudioClip dualpistolsFireAudio;
    public AudioClip railgunFireAudio;

    
    public AudioClip bulletDeflect;
    public AudioClip[] bulletHitEnemy;
    public AudioClip bulletHitGround;
    public AudioClip ratDeathAudio;
    public AudioClip enemyDeathAudio;
    public AudioClip explosionAudio;

    private UnityAction<Vector3> gunAddedEventListener;
    private UnityAction<Vector3> itemAddedEventListener;
    private UnityAction<Vector3> playerDamagedEventListener;
    private UnityAction<Vector3, float> playerLandEventListener;
    private UnityAction<Vector3> playerDashEventListener;
    private UnityAction<Vector3> playerFootstepEventListener;

    void Awake()
    {
        gunAddedEventListener = new UnityAction<Vector3>(gunAddedEventHandler);
        itemAddedEventListener = new UnityAction<Vector3>(itemAddedEventHandler);
        playerDamagedEventListener = new UnityAction<Vector3>(playerDamagedEventHandler);
        playerLandEventListener = new UnityAction<Vector3, float>(playerLandEventHandler);
        playerDashEventListener = new UnityAction<Vector3>(playerDashEventHandler);
        playerFootstepEventListener = new UnityAction<Vector3>(playerFootstepEventHandler);
    }

    void OnEnable()
    {
        EventManager.StartListening<GunAddedEvent, Vector3>(gunAddedEventListener);
        EventManager.StartListening<ItemAddedEvent, Vector3>(itemAddedEventListener);
        EventManager.StartListening<PlayerDamagedEvent, Vector3>(playerDamagedEventListener);
        EventManager.StartListening<PlayerLandEvent, Vector3, float>(playerLandEventListener);
        EventManager.StartListening<PlayerDashEvent, Vector3>(playerDashEventListener);
        EventManager.StartListening<PlayerFootstepEvent, Vector3>(playerFootstepEventListener);
    }

    void gunAddedEventHandler(Vector3 pos)
    {
        if (gunAddedAudio)
        {
            EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = this.gunAddedAudio;
            snd.audioSrc.Play();
        }
    }

    void itemAddedEventHandler(Vector3 pos)
    {
        if (eventSoundPrefab)
        {
            EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = itemAddedAudio;
            snd.audioSrc.volume = 0.7f;
            snd.audioSrc.Play();
        }
    }
    void playerDamagedEventHandler(Vector3 pos)
    {
        if (eventSoundPrefab)
        {
            EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = playerDamagedAudio;
            snd.audioSrc.Play();
        }

    }

    void playerLandEventHandler(Vector3 pos, float fallspeed)
    {
        if (eventSoundPrefab)
        {
            if (fallspeed < 50f)
            {
                EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
                snd.audioSrc.clip = this.playerSoftLandAudio;
                snd.audioSrc.Play();
            } else if (fallspeed >= 50f)
            {
                EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
                snd.audioSrc.clip = this.playerHardLandAudio;
                snd.audioSrc.Play();
            }
        }
    }

    void playerDashEventHandler(Vector3 pos)
    {
        if (eventSoundPrefab)
        {
            EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = this.playerDashAudio;
            snd.audioSrc.volume = 0.5f;
            snd.audioSrc.pitch = 1.3f;
            snd.audioSrc.Play();
        }
    }

    void playerFootstepEventHandler(Vector3 pos)
    {
        if (gunAddedAudio)
        {
            EventSound snd = Instantiate(eventSoundPrefab, pos, Quaternion.identity, null);
            snd.audioSrc.clip = this.playerFootStepAudio[Random.Range(0, playerFootStepAudio.Length)];
            snd.audioSrc.volume = 0.5f;
            snd.audioSrc.Play();
        }
    }
}
