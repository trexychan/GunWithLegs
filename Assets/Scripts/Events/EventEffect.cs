using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEffect : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    

}
