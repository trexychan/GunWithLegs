using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType maxHealth;
    int currentHealth;

    void Start() {
        this.currentHealth = (int)maxHealth;
        Debug.Log(this.currentHealth);
    }
    
    public void TurnToFacePlayer(Transform pos) {
        if (pos.transform.position.x > this.gameObject.transform.position.x) {
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
            Die();
        }
    }
}

public enum EnemyType
{
    MINOR=2,
    BEEMON=3,
    RATTANK=10
}
