using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float flySpeed;

    private Animator animator;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
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
        sprite.flipX = true;
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        animator.SetBool("Run", true);
        sprite.flipX = false;
        transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
    }

    private void Fly()
    {
        animator.SetBool("Fly", true);
        transform.position += (Vector3.up * flySpeed) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {//player is grounded
            animator.SetBool("Fly", false);
        }
    }

}
