using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float hDirectionalinput;
    private Collider2D collider;
    private int Score;

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float RunSpeed = 5;
    [SerializeField]
    private float JumpForce = 6;
    [SerializeField]
    private Text ScoreTally;

    

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update the horizontal input
        hDirectionalinput = Input.GetAxis("Horizontal");

        //update Score
        UpdateScoreOnTally(Score);


        // Update the Movement velocity. Do i want to call update every frame????
        UpdateRigidbodyVelocity(hDirectionalinput);

        //in the end we will update the statemachine of animation depending of the velocity of the rigidbody
        UpdateAimationStates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            ++Score;
        }
    }

    private void UpdateScoreOnTally(int Score)
    {
        ScoreTally.text = Score.ToString();
    }

    private void UpdateAimationStates()
    {
        float yVelocity = rigid.velocity.y;
        float xVelocity = rigid.velocity.x;

        if(Mathf.Abs(yVelocity) > Mathf.Epsilon)//either we are falling or jumping
        {
            CurrState = yVelocity>0f ? State.JumpingUp:State.FallingDown;
            
            if(CurrState == State.FallingDown && collider.IsTouchingLayers(groundLayer))
            {
                CurrState = State.Idle;
            }
        }
        else if(Mathf.Abs(xVelocity)>2f)
        {
            CurrState = State.Running;
        }
        else
        {
            CurrState = State.Idle;
        }
        anim.SetInteger("State",(int)CurrState);
    }

    private void UpdateRigidbodyVelocity(float hDirectionalinput)
    {
        if (Input.GetKey(KeyCode.Space) && collider.IsTouchingLayers(groundLayer))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, JumpForce);
        }
        else if (hDirectionalinput != 0f)
        {
            rigid.velocity = new Vector2(hDirectionalinput * RunSpeed, rigid.velocity.y);
            transform.localScale = new Vector2(-hDirectionalinput > 0 ? -1f : 1f, 1f);
        }
    }
}
