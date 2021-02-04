using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Running,
    JumpingUp,
    FallingDown
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private State CurrState;
    private float RunSpeed = 5;
    private float hDirectionalinput;
    private Collider2D collider;
    [SerializeField]
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update the horizontal input
        hDirectionalinput = Input.GetAxis("Horizontal");

        // Update the Movement velocity. Do i want to call update every frame????
        UpdateRigidbodyVelocity(hDirectionalinput);

       

        //in the end we will update the statemachine of animation depending of the velocity of the rigidbody
        UpdateAimationStates();
    }

    private void UpdateAimationStates()
    {
        if(CurrState ==State.JumpingUp)
        {

        }
        else if(Mathf.Abs(rigid.velocity.x)>2f)
        {
            CurrState = State.Running;
        }
        else
        {
            CurrState = State.Idle;
        }

    }

    private void UpdateRigidbodyVelocity(float hDirectionalinput)
    {
        if (Input.GetKey(KeyCode.Space) && collider.IsTouchingLayers(groundLayer))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 3f);
        }
        else if (hDirectionalinput != 0f)
        {
            rigid.velocity = new Vector2(hDirectionalinput * RunSpeed, rigid.velocity.y);
            transform.localScale = new Vector2(-hDirectionalinput > 0 ? -1f : 1f, 1f);
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }
    }
}
