using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    Rigidbody2D rb;
    public float despawnTime = 3f;
    private float despawnTimer;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        OnEnable();
    }

    private void OnEnable()
    {
        
        rb.AddForce(transform.up * 500f);
        rb.AddTorque(500f, ForceMode2D.Impulse);
        despawnTimer = despawnTime;
    }

    private void Update()
    {
        if (despawnTimer >= 0)
        {
            despawnTimer -= Time.deltaTime;
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
