using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectEventManager : MonoBehaviour
{
    public EventEffect eventEffectPrefab;
    public RuntimeAnimatorController dash_cloud;
    private UnityAction<Transform> dashCloudEventListener;

    void Awake()
    {
        dashCloudEventListener = new UnityAction<Transform>(dashCloudEventHandler);
    }

    void OnEnable()
    {
        EventManager.StartListening<PlayerDashEvent, Transform>(dashCloudEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<PlayerDashEvent, Transform>(dashCloudEventListener);
    }

    void dashCloudEventHandler(Transform pos)
    {
        if (eventEffectPrefab)
        {
            EventEffect eff = Instantiate(eventEffectPrefab, pos.position, pos.rotation);
            eff.animator.runtimeAnimatorController = dash_cloud;
            
        }
    }
}
