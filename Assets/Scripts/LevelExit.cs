using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string destination;
    public Vector3 destination_position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            PlayerData.Instance.SetPlayerPosition(destination_position);
            SceneManager.LoadScene(destination, LoadSceneMode.Single);
        }
    }
}
