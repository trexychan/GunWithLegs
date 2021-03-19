using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType maxHealth;
    int currentHealth;
    bool isAlive;
    public Transform vision_origin = null; // location of vision raycast, mainly for ranged enemies

    void Start() {
        this.currentHealth = (int)maxHealth;
        this.isAlive = true;
        Debug.Log(this.currentHealth);
    }
    
    public void TurnToFacePlayer(Vector3 pos) {
        if (pos.x > this.gameObject.transform.position.x) {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public virtual void Die() {
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage) {
        this.currentHealth -= damage;
        Debug.Log(this.currentHealth);
        if (this.currentHealth <= 0) {
            this.isAlive = false;
            Die();
        }
    }

    public bool IsAlive() {
        return this.isAlive;
    }

    public bool spottedPlayer() {
        int layermask = 1 << 8;
        Vector3 temp = new Vector3();
        temp = -(this.vision_origin.right);
        RaycastHit2D hit = Physics2D.Raycast(this.vision_origin.position, temp, 10.0f, layermask);
        

        if (hit)
        {
            Debug.Log(hit.collider.name);
            return true;
        }
        return false;
    }

    public void ShootRay()
    {
        Debug.Log("SHOT");
        
    }
}

public enum EnemyType
{
    MINOR=2,
    BEEMON=3,
    RATTANK=10
}
