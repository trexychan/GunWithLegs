using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    public float disappear_time, reappear_time;
    private bool solid;
    // Start is called before the first frame update
    void Start()
    {
        solid = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!solid)
        {
            StartCoroutine(ReappearPlatform());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisappearPlatform());
        }
    }

    private IEnumerator DisappearPlatform()
    {
        yield return new WaitForSeconds(disappear_time);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        solid = false;
    }

    private IEnumerator ReappearPlatform()
    {
        yield return new WaitForSeconds(reappear_time);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        solid = true;
    }
}
