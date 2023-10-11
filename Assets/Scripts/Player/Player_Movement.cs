using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float MoveSpeed;

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

        }
        else
        {//ground movement
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
                ResetAnimator();
            }
        }
        
    }

    private void MoveLeft()
    {
        animator.SetBool("Run", true);
        sprite.flipX = true;
        transform.position += (Vector3.left * MoveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        animator.SetBool("Run", true);
        sprite.flipX = false;
        transform.position += (Vector3.right * MoveSpeed) * Time.deltaTime;
    }

    private void Fly()
    {

    }

    private void ResetAnimator()
    {//reset animator to idle

        animator.SetBool("Run", false);
        animator.SetBool("Fly", false);
    }
}
