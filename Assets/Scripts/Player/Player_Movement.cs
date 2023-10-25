using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float flySpeed;

    private Animator animator;
    private Rigidbody2D rigidbody;


    
    void Start()
    {// Start is called before the first frame update

        animator = transform.Find("Sprite").GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {// Update is called once per frame

        PlayerInput();
    }

    // Movement /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {//jetpack movement
            Fly();
        }

        if (Input.GetKey(KeyCode.A))
        {//left
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {//right
            MoveRight();
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void MoveLeft()
    {
        animator.SetBool("Run", true);
        transform.localScale = new Vector3(-1, 1, 1);
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        animator.SetBool("Run", true);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
    }

    private void Fly()
    {
        //resets downward gravity velocity to prevent stacking gravity while in flight
        rigidbody.velocity = Vector3.zero;

        //fly
        animator.SetBool("Fly", true);
        transform.position += (Vector3.up * flySpeed) * Time.deltaTime;
    }

    // Collision Detection /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {//player is grounded
            animator.SetBool("Fly", false);
        }
    }

}
