using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float accelerationTimeAirborne;
    public float accelerationTimeGrounded;
    private float velocityXSmoothing;
    public float moveAfterLaunchTime;
    private float moveAfterLaunchTimer;
    [HideInInspector]
    public Vector2 moveInput;

    //variables for variable jump height
    public float maxJumpVelocity;
    public float minJumpVelocity;

    //the player's rigidbody
    private Rigidbody2D rb;

    //Everything for being grounded
    [HideInInspector]
    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    public PlayerControls playerControls;
    public float dashTime;
    public float dashSpeed;

    public List<GunBase> gunList;
    public Player playerManager;
    [SerializeField]
    public Transform firePoint;
    public GameObject pistolHitEffect;
    public AudioClip pistolFireSound;
    public AudioSource audioSource;
    int currentGun;
    public bool canDoubleJump = false, hasJumpedOnce = false, hasDoubleJumped = false;

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
        currentGun = 0;
        gunList = new List<GunBase>();
        gunList.Add(new Pistol(this, firePoint, pistolHitEffect, pistolFireSound, GetComponent<LineRenderer>(), null));
    }

    public void Update() {
        
    }

    public float CalculatePlayerVelocity(float RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float accelerationTimeGrounded, float accelerationTimeAirborne, bool isGrounded)
    {
        float targetVelocityx = input.x * moveSpeed;
        return Mathf.SmoothDamp(RBvelocity, targetVelocityx, ref velocityXSmoothing, isGrounded ? accelerationTimeGrounded : accelerationTimeAirborne);
    }

    //if you jump it changes your y velocity to the maxJumpVelocity
    public void Jump()
    {
        if (!isGrounded && canDoubleJump && hasJumpedOnce) {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity * 1.2f);
            hasJumpedOnce = false;
            hasDoubleJumped = true;
            Debug.Log("second jump");
        } else if (isGrounded && !hasJumpedOnce) {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);
            hasDoubleJumped = false;
            Debug.Log("first jump");
        }
    }

    public bool canJump()
    {
        if (isGrounded) {
            return true;
        } else if (!isGrounded && canDoubleJump && !hasDoubleJumped) {
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
        return gunList[currentGun].canDash();
    }

    public void switchGun(bool right) {
        if (right) {
            if (currentGun + 1 == gunList.Count) {
                currentGun = 0;
            } else {
                currentGun++;
            }
        } else {
            if (currentGun - 1 == -1) {
                currentGun = gunList.Count - 1;
            } else {
                currentGun--;
            }
        }
    }

    public void Shoot() {
       Vector2 shootDir = clampTo8Directions(playerControls.InGame.Move.ReadValue<Vector2>());
       //use the abstract gun class to shoot
       gunList[currentGun].Shoot();
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Platform"))
    //     {
    //         transform.parent = collision.transform;
    //     }
    // }

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

}
