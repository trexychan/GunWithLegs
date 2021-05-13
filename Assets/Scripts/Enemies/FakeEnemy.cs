using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour
{
    public GameObject ammoPack;
    public GameObject death_particles;

    // Start is called before the first frame update
    void Start()
    {
        death_particles.SetActive(false);
    }

    void Update()
    {
        death_particles.transform.position = this.gameObject.transform.position;
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

    public void Die()
    {
        death_particles.SetActive(true);
        DropHealth(1);
        gameObject.SetActive(false);
    }
}
