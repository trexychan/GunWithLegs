using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 3f, currentPlayerHealth = 9f, maxPlayerHealth = 9f;
    public Image[] healthbullets;
    public Sprite loadedshell;
    public Sprite emptyshell;
    public Sprite[] gunicons;
    public Image[] gunHUDSlots;
    public float accelerationTimeAirborne;
    public float accelerationTimeGrounded;
    private float velocityXSmoothing;

    public Vector2 launchVelocity;
    public float moveAfterLaunchTime;
    [HideInInspector]
    public Vector2 moveInput;

    //variables for variable jump height
    public float maxJumpVelocity;
    public float minJumpVelocity;

    //the player's rigidbody
    public Rigidbody2D rb;

    //the player's box collider
    public BoxCollider2D boxCollider;

    //Everything for being grounded
    [HideInInspector]
    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    public PlayerControls playerControls;
    public float dashTime;
    public float dashSpeed;
    public float dashCooldownTime;
    private float dashCooldownTimer;
    public bool canFire = true;
    private bool isImmuneToDamage = false;
    public float invincibilityCooldownTime;
    private float invincibilityCooldownTimer;

    public List<GunBase> gunList;
    public Player playerManager;
    [SerializeField]
    //public Transform firePoint;
    public Transform firePoint, DPLeftFirePoint;
    public Transform ejectPt;
    public GameObject ejected_shell;
    public GameObject[] hitEffects = new GameObject[5];
    public GameObject switchEffect;
    public GameObject[] bulletObjs = new GameObject[5];
    public List<RuntimeAnimatorController> gunAnimControllers = new List<RuntimeAnimatorController>();
    public AudioClip[] gunSounds;
    public AudioSource audioSource;
    int currentGun;
    public bool canDoubleJump = false, hasJumpedOnce = false, hasDoubleJumped = false;
    private bool takingDamage = false;
    public LineRenderer dualPistolsLeftFirePoint;
    private SpriteRenderer spriteRenderer;

    public bool damaged;


    private void Awake() {
        playerControls = new PlayerControls();
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetPlayerHealthBar(currentPlayerHealth);
        currentGun = 0;
        gunList = new List<GunBase>();
        //gunList.Add(new DualPistols(this, firePoint, DPLeftFirePoint, hitEffects[0], gunSounds[0], GetComponent<LineRenderer>(), dualPistolsLeftFirePoint, null));
        // gunList.Add(new TVGun(this, firePoint, hitEffects[0],bulletObjs[1], gunSounds[0], gunAnimControllers[0]));
        gunList.Add(new Pistol(this, firePoint, hitEffects[0], gunSounds[0], GetComponent<LineRenderer>(), gunAnimControllers[0], ejected_shell, ejectPt, gunicons[0]));
        Debug.Log(gunicons[0]);
        SetPlayerCurrentGun(currentGun);
        //gunList.Add(new Shotgun(this, firePoint, hitEffects[0], gunSounds[1], GetComponent<LineRenderer>(), gunAnimControllers[1]));
        //gunList.Add(new RPG(this, firePoint, hitEffects[0], bulletObjs[1], gunSounds[1], gunAnimControllers[2]));
        foreach (RuntimeAnimatorController anim in gunAnimControllers) {
            Debug.Log(anim);
        }
    }

    public void Update() {
        updateDashCooldown();
        updateInvincibilityCooldown();
    }

    public void DamagePlayer(float damage)
    {
        if (!isImmuneToDamage) {
            currentPlayerHealth -= damage;
            currentPlayerHealth = Mathf.Ceil(currentPlayerHealth);
            SetPlayerHealthBar(currentPlayerHealth); 
        }
    }

    public bool tookDamage() {
        return damaged;
    }

    public void setDamaged(bool set) {
        damaged = set;
    }

    public void IncreaseMaxHealth(float maxIncrease)
    {
        maxPlayerHealth += maxIncrease;
        currentPlayerHealth = maxPlayerHealth;
        SetPlayerHealthBar(currentPlayerHealth);
    }

    private void SetPlayerHealthBar(float currentHealth)
    {
        if (currentHealth > maxPlayerHealth) {currentHealth = maxPlayerHealth;}

        for (int i = 0; i < healthbullets.Length; i++)
        {
            if (i < currentHealth) {healthbullets[i].sprite = loadedshell;}
            else {healthbullets[i].sprite = emptyshell;}
            if (i < maxPlayerHealth)
            {
                healthbullets[i].enabled = true;
            } else
            {
                healthbullets[i].enabled = false;
            }
        }
    }

    private void SetPlayerCurrentGun(int current)
    {
        gunHUDSlots[1].sprite = gunList[current].icon;
        
        gunHUDSlots[2].sprite = gunList[(currentGun + 1) % gunList.Count].icon;
        if (currentGun - 1 < 0)
        {
            gunHUDSlots[0].sprite = gunList[gunList.Count - 1].icon;
        } else
        {
            gunHUDSlots[0].sprite = gunList[currentGun - 1].icon;
        }
    }

    public float CalculatePlayerVelocity(float RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float accelerationTimeGrounded, float accelerationTimeAirborne, bool isGrounded)
    {
        float targetVelocityx = input.x * moveSpeed;
        return Mathf.SmoothDamp(RBvelocity, targetVelocityx, ref velocityXSmoothing, isGrounded ? accelerationTimeGrounded : accelerationTimeAirborne);
    }

    public void launchPlayer(Vector2 velocity) {
        rb.velocity = velocity;
    }

    //if you jump it changes your y velocity to the maxJumpVelocity
    public void Jump()
    {
        if (!isGrounded && canDoubleJump && hasJumpedOnce && canJump()) {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity * 1.2f);
            hasJumpedOnce = false;
            hasDoubleJumped = true;
        } else if (isGrounded && !hasJumpedOnce) {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);
            hasDoubleJumped = false;
            hasJumpedOnce = true;
        }
    }

    public bool canJump()
    {
        if (isGrounded && gunList[currentGun].canDash()) {
            hasJumpedOnce = false;
            return true;
        } else if (!isGrounded && canDoubleJump && !hasDoubleJumped && gunList[currentGun].canDash()) {
            return true;
        }
        return false;
    }

    public void JumpRelease()
    {
        if (rb.velocity.y > minJumpVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, minJumpVelocity);
        }
    }

    public void WallJump(Vector2 jumpVelocity)
    {
        rb.velocity = jumpVelocity;
    }


    public void HandleMovement()
    {
        moveInput = playerControls.InGame.Move.ReadValue<Vector2>();
        float xVelocity = CalculatePlayerVelocity(rb.velocity.x, moveInput, moveSpeed, velocityXSmoothing, accelerationTimeGrounded, accelerationTimeAirborne, isGrounded);
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void HandleLerpMovement()
    {
        moveInput = playerControls.InGame.Move.ReadValue<Vector2>();
        rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(moveInput.x * moveSpeed, rb.velocity.y)), 1f * Time.deltaTime);
    }

    private Vector2 clampTo8Directions(Vector2 vectorToClamp) {
        if (vectorToClamp.x > 0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //right
            return new Vector2(1,0);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y < -0.1f)
        {
            //down right
            return new Vector2(1,-1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y < -0.1f)
        {
            //down
            return new Vector2(0,-1);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y < -0.1f)
        {
            //down left
            return new Vector2(-1,-1).normalized;
        }
        if (vectorToClamp.x < -0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //left
            return new Vector2(-1,0);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y > 0.1f)
        {
            //up left
            return new Vector2(-1,1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y > 0.1f)
        {
            //up
            return new Vector2(0,1);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y > 0.1f)
        {
            //up right
            return new Vector2(1,1).normalized;
        }

        return Vector2.zero;
    }

    public bool canDash() {
        return gunList[currentGun].canDash() && dashCooldownTimer <= 0;
    }

    public void startDashCooldown()
    {
        dashCooldownTimer = dashCooldownTime;
    }

    public bool checkIfGrounded() {

        Vector2 startingPoint1 = new Vector2(boxCollider.bounds.center.x - boxCollider.bounds.extents.x , boxCollider.bounds.center.y - boxCollider.bounds.extents.y);
        Vector2 startingPoint2 = new Vector2(boxCollider.bounds.center.x + boxCollider.bounds.extents.x, boxCollider.bounds.center.y - boxCollider.bounds.extents.y);
        RaycastHit2D hit1 = Physics2D.Raycast(startingPoint1, Vector2.down, 0.2f, whatIsGround);
        Debug.DrawRay(startingPoint1, Vector2.down, Color.blue, 0.2f);
        RaycastHit2D hit2 = Physics2D.Raycast(startingPoint2, Vector2.down, 0.2f, whatIsGround);
        Debug.DrawRay(startingPoint2, Vector2.down, Color.blue, 0.2f);
        if (hit1 || hit2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void updateDashCooldown()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void updateInvincibilityCooldown() {
        if (invincibilityCooldownTimer >= 0 && isImmuneToDamage) {
            invincibilityCooldownTimer -= Time.deltaTime;
            if (spriteRenderer.enabled) {
                spriteRenderer.enabled = false;
            } else {
                spriteRenderer.enabled = true;
            }
        } else {
            SetPlayerImmunity(false);
        }
    }

    public void switchGun(int right) {
        if (right > 0) {
            if (currentGun + 1 == gunList.Count) {
                currentGun = 0;
            } else {
                currentGun++;
            }
        } else if (right < 0) {
            if (currentGun - 1 == -1) {
                currentGun = gunList.Count - 1;
            } else {
                currentGun--;
            }
        }
        if (gunList.Count > 1) {Instantiate(switchEffect,gameObject.transform.position,Quaternion.identity);}
        Debug.Log(currentGun);
        SetPlayerCurrentGun(currentGun);
        playerManager.GetStateInput().anim.runtimeAnimatorController = gunList[currentGun].animController;
        //Debug.Log(currentGun + " " + gunAnimControllers[currentGun]);
    }

    public void Shoot() {
        Vector2 shootDir = clampTo8Directions(playerControls.InGame.Move.ReadValue<Vector2>());
        //use the abstract gun class to shoot
        if (canFire) {
            canFire = false;
            gunList[currentGun].Shoot();
            StartCoroutine(delayNextShot());
        }

        //sample code to fire camera shake
        CamController.Instance.Shake(2, 0.1f);
    }

    public IEnumerator delayNextShot()
    {
        yield return new WaitForSeconds(gunList[currentGun].fireRate);
        canFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Platform"))
        // {
        //     transform.parent = collision.transform;
        // }

        if (collision.gameObject.layer == 11 && !damaged) // if the collision is with an enemy 
        { 
            Debug.Log("ran into enemy");
            if (isImmuneToDamage == false) {
                if (collision.gameObject.CompareTag("Enemy Projectile"))
                {
                    float enemyRangedDamage = collision.gameObject.GetComponent<EnemyProjectile>().GetProjectileDamage();
                    DecreasePlayerCurrentHealth(enemyRangedDamage);
                } else {    
                    float enemyMeleeDamage = collision.gameObject.GetComponent<EnemyController>().GetMeleeDamage();
                    DecreasePlayerCurrentHealth(enemyMeleeDamage);
                }
                setDamaged(true);
            }
        }
            
        // } else if (collision.gameObject.CompareTag("Enemy Projectile") && !takingDamage) // if collision is with an enemy ranged attack
        // { 
        //     Debug.Log("ran into enemy ranged attack");
        //     takingDamage = true;

        //     float enemyRangedDamage = collision.gameObject.GetComponent<EnemyProjectile>().GetProjectileDamage();
        //     DecreasePlayerCurrentHealth(enemyRangedDamage);
            
        //     StartCoroutine(RangedAttackPushPlayer());
            
        //     // do other stuff if player collides with enemy ranged attack
        // }
    }

    private IEnumerator EnemyPushPlayer() 
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        
        int num = Random.Range(0,2); // randomly push player to the left or to the right
        if (num == 0)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2000);
        else
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2000);
        
        yield return new WaitForSeconds(0.05f);
        GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(1f); // temporary immunity from damage for player
        takingDamage = false;
    }

    private IEnumerator RangedAttackPushPlayer() 
    {
        // push player after getting hit by enemy ranged attack
        yield return null;
    }

    public void DecreasePlayerCurrentHealth(float amount)
    {
        currentPlayerHealth -= amount;
        currentPlayerHealth = Mathf.Ceil(currentPlayerHealth);
        SetPlayerHealthBar(currentPlayerHealth);
        Debug.Log("Player health: " + currentPlayerHealth);
        if (currentPlayerHealth <= 0f) {
            Debug.Log("player died :(");
            // kill player
        }
    }

    public void IncreasePlayerCurrentHealth(float amount)
    {
        if (currentPlayerHealth + amount >= maxPlayerHealth) {
            currentPlayerHealth = maxPlayerHealth;
        } else {
            currentPlayerHealth += amount;
        }
        SetPlayerHealthBar(currentPlayerHealth);
        Debug.Log("Player health: " + currentPlayerHealth);
    }

    public void SetPlayerImmunity(bool immunity) {
        this.isImmuneToDamage = immunity;
        if (immunity == false) {
            spriteRenderer.enabled = true;
            invincibilityCooldownTimer = invincibilityCooldownTime;
        }
    }

    public void IncreasePlayerMaxHealth(float amount)
    {
        maxPlayerHealth += amount;
        currentPlayerHealth = maxPlayerHealth;
        SetPlayerHealthBar(currentPlayerHealth);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    public void showBulletTrail(LineRenderer renderer) {
        StartCoroutine(displayBulletTrail(renderer));
    }

    public void playSound(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private IEnumerator displayBulletTrail(LineRenderer bulletTrail) {
        bulletTrail.enabled = true;
        yield return new WaitForSeconds(0.02f);
        bulletTrail.enabled = false;
    }

    public void setDoubleJump(bool canPlayerDoubleJump) {
        canDoubleJump = canPlayerDoubleJump;
    }

    public void addGun(int gunType) {
        switch(gunType) {
            case (int)GunPickup.GunType.RPG:
                gunList.Add(new RPG(this, firePoint, hitEffects[0], bulletObjs[1], gunSounds[1], gunAnimControllers[2], gunicons[2]));
            break;
            case (int)GunPickup.GunType.Shotgun:
                gunList.Add(new Shotgun(this, firePoint, hitEffects[0], gunSounds[1], GetComponent<LineRenderer>(), gunAnimControllers[1], gunicons[1]));
            break;
            case (int)GunPickup.GunType.DualPistols:
                gunList.Add(new DualPistols(this, firePoint, DPLeftFirePoint, hitEffects[0], gunSounds[0], GetComponent<LineRenderer>(), dualPistolsLeftFirePoint, gunAnimControllers[3], gunicons[3]));
            break;

        }
        SetPlayerCurrentGun(currentGun);
    }

}
