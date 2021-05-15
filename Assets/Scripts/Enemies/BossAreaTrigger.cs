using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public EnemyController boss;
    private CameraHandler cameraHandler;
    // class for handling the inital boss sequence, including spawning the boss, locking off the exit, and starting the music (if there is any)
    public List<GameObject> arenaDoors;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandler = GameObject.FindGameObjectWithTag("CameraHandler").GetComponent<CameraHandler>();
        if (cameraHandler == null)
        {
            Debug.Log("You need a CameraHandler in the scene to be able to switch between the main assembled camera and the boss room camera!");
        }
        foreach (Transform child in transform)
        {
            arenaDoors.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.IsAlive())
        {
            foreach (GameObject door in arenaDoors)
            {
                door.GetComponent<Collider2D>().isTrigger = true;
                door.GetComponent<Animator>().SetBool("isClosed", false);
                door.gameObject.layer = 14;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraHandler.SwitchCamera("Boss");
            foreach (GameObject door in arenaDoors)
            {
                door.GetComponent<Collider2D>().isTrigger = false;
                door.GetComponent<Animator>().SetBool("isClosed", true);
                door.gameObject.layer = 9;
                boss.isAwake = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraHandler.SwitchCamera("Assembled");
        }
        
    }
}
