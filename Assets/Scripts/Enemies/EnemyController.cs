using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public EnemyType maxHealth;
    public Rigidbody2D rb;
    public int ammoDropCount;
    public Transform firept;
    public GameObject heavyprojectile;
    public GameObject lightprojectile;
    public GameObject ammoPack;
    public GameObject enemyDeathExplosion;
    public GameObject player;
    public Animator anim;

    [HideInInspector]
    public int currentHealth;

    
    bool isAlive;
    public bool canAct = true;
    public bool isAwake = true;
    public bool facingRight = false;
    public Transform vision_origin = null; // location of vision raycast, mainly for ranged enemies
    public Condition currentCondition = Condition.NONE;
    [SerializeField]
    float meleeDamage, rangedAttackDamage;
    public float minChaseDistance = 10; 

    private IEnumerator damageCoroutine;

    SpriteRenderer sprite;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }
    void Start() {
        this.currentHealth = (int)maxHealth;
        this.isAlive = true;
        if (ammoDropCount == 0)
        {
            ammoDropCount = (int)maxHealth;
        }
        
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

    public void StopMoving()
    {
        if (rb)
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    public bool spotPlayerByDistance(Vector2 playerPos) {
        float distance = Vector2.Distance(this.gameObject.transform.position, playerPos);
        if (distance < minChaseDistance) {
            return true;
        }
        return false;
    }


    public virtual void Die() {
        this.gameObject.SetActive(false);
        Instantiate(enemyDeathExplosion, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        DropHealth(ammoDropCount);
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

    public void DropHealth(int number)
    {
        for (int i = 0; i < number; i++) {
            float xValue = Random.Range(-4f, 4f);
            float yValue = Random.Range(8f,10f);
            // float torqueValue = Random.Range(-10f, 10f);
            GameObject ammo = Instantiate(ammoPack, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            ammo.GetComponent<Rigidbody2D>().velocity += new Vector2(xValue, yValue);
            // ammo.GetComponent<Rigidbody2D>().AddTorque(torqueValue);
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

    public bool checkIsGrounded()
    {
        Vector2 startingPoint1 = new Vector2(boxCollider.bounds.center.x - boxCollider.bounds.extents.x , boxCollider.bounds.center.y - boxCollider.bounds.extents.y);
        Vector2 startingPoint2 = new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x, boxCollider.bounds.center.y - boxCollider.bounds.extents.y);
        RaycastHit2D hit1 = Physics2D.Raycast(startingPoint1, Vector2.down, 0.05f, 1 << 9);
        Debug.DrawRay(startingPoint1, Vector2.down, Color.blue, 0.05f);
        RaycastHit2D hit2 = Physics2D.Raycast(startingPoint2, Vector2.down, 0.05f, 1 << 9);
        Debug.DrawRay(startingPoint2, Vector2.down, Color.blue, 0.05f);
        if (hit1 || hit2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShootRay()
    {
        Debug.Log("SHOT");
        
    }

    public void ShootHeavyProjectile()
    {
        Debug.Log("PROJECTILE");
        Instantiate(heavyprojectile,firept.position,gameObject.transform.rotation);
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
        while (lerpTime < 0.15f)
        {
            lerpTime += Time.deltaTime;
            // float perc = lerpTime / 0.3f;

            sprite.material.SetFloat("_FlashAmount", 1f);
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

    public void EnemyPause(float delay)
    {
        canAct = false;
        StartCoroutine(PauseDelay(delay));
    }

    private IEnumerator PauseDelay(float d)
    {
        yield return new WaitForSeconds(d);
        canAct = true;
    }
}

public enum EnemyType
{
    MINOR=1,
    BEEMON=3,
    RATTANK=10,
    CRUSHROOM=6,
    GHOST=4,
    MAJORINCONVENIENCE=40
}

public enum Condition
{
    WEAK,
    POISONED,
    SLOWED,
    NONE
}