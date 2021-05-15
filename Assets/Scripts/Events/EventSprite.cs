using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float lifetime = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Start()
    {
        StartCoroutine(DestroyAfterTimeExpire());
    }

    private IEnumerator DestroyAfterTimeExpire()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

}
