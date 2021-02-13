using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironDmg : MonoBehaviour
{
    //public Player player_info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            //player_info.transform.position = player_info.spawn.position;
        }
    }
}
