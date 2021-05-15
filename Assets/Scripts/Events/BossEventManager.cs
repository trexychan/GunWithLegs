using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossEventManager : MonoBehaviour
{
    private UnityAction<Vector3> bossDeathEventListener;


    void Awake()
    {
        bossDeathEventListener = new UnityAction<Vector3>(bossDeathEventHandler);
    }

    void OnEnable()
    {
        EventManager.StartListening<BossDeathEvent, Vector3>(bossDeathEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<BossDeathEvent, Vector3>(bossDeathEventListener);
    }

    void bossDeathEventHandler(Vector3 pos)
    {
        
    }
}
