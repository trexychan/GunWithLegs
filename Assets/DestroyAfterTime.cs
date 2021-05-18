using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifetimeTimer());
    }

    // Update is called once per frame
    IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
