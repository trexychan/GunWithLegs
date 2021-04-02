using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushroomManager : MonoBehaviour
{
    public GameObject player;
    bool doneWaiting = false;
    float originalY;

    void Start() {
        originalY = transform.position.y;
    }

    public void StartWaitOnGround() {
        StartCoroutine(WaitOnGround());
    }

    IEnumerator WaitOnGround() {
        yield return new WaitForSeconds(1.0f);
        doneWaiting = true;
    }

    public bool IsDoneWaiting() {
        return doneWaiting;
    }

    public void SetWaitStatus(bool newStatus) {
        doneWaiting = newStatus;
    }

    public float GetOriginalY() {
        return originalY;
    }

    public void AddForceAtAngle(float force, float angle)
    {  
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;
        
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(xcomponent, ycomponent));
    }
}
