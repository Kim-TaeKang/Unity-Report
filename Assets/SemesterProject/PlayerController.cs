using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 velocity;
    public float moveSpeed = 5.0f;
    public float jumpPower = 10.0f;
    private bool isGround = false;

    Animator animator;
    new Rigidbody2D rigidbody;


    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // 플레이어의 이동 처리
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 플레이어의 이동 방향 설정
        velocity = new Vector2(horizontal * moveSpeed, rigidbody.velocity.y);


        if (isGround == false)
            if (velocity.y < -0.5)
                animator.SetBool("isFall", true);

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (velocity.x != 0 && isGround)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetTrigger("Jump");
        }
    }
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y > 0.5)
        {
            isGround = true;
            Debug.Log("isGround=true");
            animator.SetBool("isFall",false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        Debug.Log("isGround=false");
     
    }
}