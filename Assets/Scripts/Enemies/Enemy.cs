using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character<Enemy, EnemyState, EnemyStateInput>
{
    protected override void SetInitialState()
    {
        throw new System.NotImplementedException();
    }
}

public abstract class EnemyState : CharacterState<Enemy, EnemyState, EnemyStateInput>
{

}

public class EnemyStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public Player stateMachine;
}
