using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController2D controller2D;
    public GameObject pistolSprite;
    public GameObject shotgunSprite;
    public float runSpeed = 40f;
    bool jump = false;
    bool dash = false;
    float horizontalMove = 0f;
    public int playerHealth = 12;
    public string current_gun = "pistol";
    public Transform spawn;
    public Animator death_animcontroller;
    public AnimationClip death_animation;

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //Hardcoded af. change later
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }

        //HARDCODED AF... CHANGE LATER
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (current_gun == "pistol") 
            {
                current_gun = "shotgun";
                shotgunSprite.SetActive(true);
                pistolSprite.SetActive(false);
            } 
            else 
            {
                current_gun = "pistol";
                shotgunSprite.SetActive(false);
                pistolSprite.SetActive(true);
            }
            
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump, dash);
        jump = false;
        dash = false;
    }
    //damage player based on the amount of dmg received
    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Death();
        }
    }
    //handles death
    void Death()
    {
        death_animcontroller.Play("death_scrn_fadein");
        Destroy(gameObject);
    }
    //handles collisions
    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        AmmoPack other = col.GetComponent<AmmoPack>();
        if (enemy != null)
        {
            DamagePlayer(2);
        } else if (other != null)
        {
            playerHealth += other.ammo_count;
        }
        
    }
}
