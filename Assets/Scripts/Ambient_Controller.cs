using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambient_Controller : MonoBehaviour
{
    public Transform ambience_change;
    public AudioSource ambient_src;
    public AudioClip ambient1;
    public AudioClip ambient2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Player player = other.gameObject.GetComponentInParent<Player>();
        // if (player != null)
        // {
        //     if (ambient_src.clip != ambient2)
        //     {
        //         ambient_src.clip = ambient2;
        //         ambient_src.Play();
        //     }
            
        // }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Player player = other.gameObject.GetComponentInParent<Player>();
        // if (player != null)
        // {
        //     if (ambient_src.clip == ambient2)
        //     {
        //         ambient_src.clip = ambient1;
        //         ambient_src.Play();
        //     }
        // }
    }
}
