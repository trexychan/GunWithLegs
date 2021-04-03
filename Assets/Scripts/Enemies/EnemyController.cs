using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType maxHealth;
    int currentHealth;
    bool isAlive;
    public bool facingRight = false;
    public Transform vision_origin = null; // location of vision raycast, mainly for ranged enemies
    public Condition currentCondition = Condition.NONE;
    [SerializeField]
    float meleeDamage, rangedAttackDamage;

    private IEnumerator damageCoroutine;

    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start() {
        this.currentHealth = (int)maxHealth;
        this.isAlive = true;
        Debug.Log(this.currentHealth);
    }
    
    public void TurnToFacePlayer(Vector3 pos) {
        if (pos.x > this.gameObject.transform.position.x) {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.facingRight = true;
        } else {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.facingRight = false;
        }
    }
    public virtual void Die() {
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage) {
        DamageFlash();
        this.currentHealth -= this.currentCondition == Condition.WEAK ? damage * 2 : damage;
        Debug.Log(this.currentHealth);
        if (this.currentHealth <= 0) {
            this.isAlive = false;
            Die();
        }
    }

    public void SetCondition(Condition condition)
    {
        if (this.currentCondition != condition) {this.currentCondition = condition;}
        ConditionChange();
    }

    public bool IsAlive() {
        return this.isAlive;
    }

    public bool spottedPlayer() {
        int layermask = 1 << 8;
        Vector3 temp = new Vector3();
        if (gameObject.transform.rotation.y == 180)
        {
            temp = this.vision_origin.right;
        } else
        {
            temp = -(this.vision_origin.right);
        }
        RaycastHit2D hit = Physics2D.Raycast(this.vision_origin.position, temp, 10.0f, layermask);
        

        if (hit)
        {
            return true;
        }
        return false;
    }

    public void ShootRay()
    {
        Debug.Log("SHOT");
        
    }

    public void ShootProjectile()
    {
        Debug.Log("PROJECTILE");
    }

    private void DamageFlash()
    {
        if (damageCoroutine != null) {StopCoroutine(damageCoroutine);}

        damageCoroutine = DoFlash();
        StartCoroutine(damageCoroutine);
    }

    private IEnumerator DoFlash()
    {
        float lerpTime = 0;
        while (lerpTime < 0.1f)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / 0.1f;

            sprite.material.SetFloat("_FlashAmount", 1f - perc);
            yield return null;
        }
        sprite.material.SetFloat("_FlashAmount",0);
    }

    private void ConditionChange()
    {

    }

    
    public float GetMeleeDamage() 
    {
        return meleeDamage;
    }

    public float GetRangedAttackDamage() 
    {
        return rangedAttackDamage;
    }
}

public enum EnemyType
{
    MINOR=2,
    BEEMON=3,
    RATTANK=10,
    CRUSHROOM=6,
    GHOST=1
}

public enum Condition
{
    WEAK,
    POISONED,
    SLOWED,
    NONE
}