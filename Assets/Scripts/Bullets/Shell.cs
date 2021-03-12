using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    Rigidbody2D rb;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 500f);
        rb.AddTorque(500f,ForceMode2D.Impulse);
        StartCoroutine(despawnTimer());
    }

    public IEnumerator despawnTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
