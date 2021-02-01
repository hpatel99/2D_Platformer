using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rigid.velocity = new Vector2(-5,rigid.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector2(5, rigid.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            anim.SetBool("running", true);
        }
        else 
        {
            anim.SetBool("running", false);
        }
    }
}
