using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public int shotgunPellets = 4;

    //public Player player;
    public PlayerSoundManager soundManager;
    public Transform pistolFirePt;
    public Transform shotgunFirePt;
    public GameObject impact_effect;
    public LineRenderer bullet_trail;
    // ammo damage stats
    public int shotgun_base_DMG = 1;
    public int pistol_base_DMG = 1;
    public float damage_mult = 1.0f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // switch (player.current_gun)
        // {
        //     case "pistol":
        //         player.DamagePlayer(1);
        //         StartCoroutine(FirePistol());
        //         soundManager.PlayPistolFire();
        //         break;
        //     case "shotgun":
        //         player.DamagePlayer(2);
        //         FireShotgun();
        //         soundManager.PlayShotgunFire();
        //         break;
        // }
    }
    
    IEnumerator FirePistol()
    {
        Vector3 temp = pistolFirePt.right;
        temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfo = Physics2D.Raycast(pistolFirePt.position, temp);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);
        Debug.Log(hitInfo.collider.gameObject.layer);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)(pistol_base_DMG*damage_mult));
            }
            bullet_trail.SetPosition(0,pistolFirePt.position);
            bullet_trail.SetPosition(1,hitInfo.point);

            GameObject new_hit = (GameObject)Instantiate(impact_effect, hitInfo.point, Quaternion.identity);
            Destroy(new_hit, 0.267f);

        } else 
        {
            bullet_trail.SetPosition(0,pistolFirePt.position);
            bullet_trail.SetPosition(1,pistolFirePt.position + temp*100);
        }

        bullet_trail.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        bullet_trail.gameObject.SetActive(false);


    }

    void FireShotgun()
    {
        for (int i=0; i<shotgunPellets; i++)
        {
            Vector3 temp = pistolFirePt.right;
            temp.y += Random.Range(-0.3f,0.3f);
            RaycastHit2D hitInfo = Physics2D.Raycast(shotgunFirePt.position, temp);
            if (hitInfo)
            {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                AmmoCrate crate = hitInfo.transform.GetComponent<AmmoCrate>();
                if (enemy != null && crate == null)
                {
                    enemy.TakeDamage((int)(shotgun_base_DMG*damage_mult));
                } else if (enemy == null && crate != null)
                {
                    crate.DamageCrate((int)(shotgun_base_DMG*damage_mult));
                }
                DrawLine(shotgunFirePt.position, hitInfo.point, Color.white,0.05f);

                GameObject new_hit = (GameObject)Instantiate(impact_effect, hitInfo.point, Quaternion.identity);
                Destroy(new_hit, 0.267f);

            } else 
            {
                DrawLine(shotgunFirePt.position, shotgunFirePt.position + temp*100, Color.white,0.05f);
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.01f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
