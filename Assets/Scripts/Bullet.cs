using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int shotgun_base_DMG = 1;
    public int pistol_base_DMG = 1;
    //public Player player;

    public float damage_mult = 1.0f;

    Random rand_ang = new Random();
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        // if (enemy != null)
        // {
        //     switch (player.current_gun)
        //     {
        //         case "pistol":
        //             enemy.TakeDamage((int)(pistol_base_DMG*damage_mult));
        //             break;
        //         case "shotgun":
        //             enemy.TakeDamage((int)(shotgun_base_DMG*damage_mult));
        //             break;
        //     }
            
        // }
        // if (col.name != "player")
        // {
        //     Destroy(gameObject);
        // }
        
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

}
