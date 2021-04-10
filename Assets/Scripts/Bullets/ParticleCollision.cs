using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem particle;
    public List<ParticleCollisionEvent> collisionEvents;
    public GameObject particleHitPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents);
        GameObject hit = Instantiate(particleHitPrefab, collisionEvents[0].intersection, Quaternion.identity);

        
    }
}
