using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float flySpeed;
    [SerializeField] float jumpSpeed;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private GameObject player;
    private bool isFlying;
    private bool isJumping;
    private Player_Stats playerStats;

    
    void Start()
    {// Start is called before the first frame update

        player = GameObject.Find("Player");
        rigidbody = player.GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player/Sprite").GetComponent<Animator>();
        playerStats = player.GetComponent<Player_Stats>();
    }
        
    void Update()
    {// Update is called once per frame

        PlayerInput();

    }

    // Movement /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayerInput()
    {
        Aim();

        if (Input.GetKey(KeyCode.Space))
        {//jetpack movement
            Jetpack(true, false);
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

    private void Aim()
    {//point laser in direction of mouse

        //player location
        Vector3 playerPos = player.transform.position;

        //mouse location
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //right angle triangle calculation
        float adjacent = mousePos.x - playerPos.x;
        float opposite = mousePos.y - playerPos.y;
        float angle = Mathf.Atan(opposite / adjacent) * (180 / Mathf.PI);

        //rotate player (temp)
        player.transform.eulerAngles = new Vector3(0, 0, angle);

        //flip player to face direction of mouse
        if (mousePos.x >= playerPos.x)
        {//mouse to the right of player
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mousePos.x < playerPos.x)
        {//mouse to the left of player
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void MoveLeft()
    {
        if (isFlying == false && isJumping == false)
        {
            animator.SetBool("Run", true);
        }
        
        //player.transform.localScale = new Vector3(-1, 1, 1);
        player.transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        if (isFlying == false && isJumping == false)
        {
            animator.SetBool("Run", true);
        }

        //player.transform.localScale = new Vector3(1, 1, 1);
        player.transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
    }

    private void Jetpack(bool on, bool gravity)
    {
        if (on == true && playerStats.Get_Energy() > 0)
        {//jetpack on

            if (gravity == false)
            {//resets downward gravity velocity to prevent stacking gravity while in flight
                rigidbody.velocity = Vector3.zero;
            }
            
            //fly
            animator.SetBool("Fly", true);
            player.transform.position += (Vector3.up * flySpeed) * Time.deltaTime;
            isFlying = true;
        }
        else if (on == true && playerStats.Get_Energy() == 0 && isJumping == false)
        {//jump if jetpack out of energy
            Jump(true);
        }
        else
        {//reset jetpack
            animator.SetBool("Fly", false);
            isFlying = false;
        }
        
    }

    private void Jump(bool on)
    {
        if (on == true)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            isJumping = true;
            rigidbody.velocity += (Vector2.up * jumpSpeed);
        }
        else
        {//reset jump
            animator.SetBool("Jump", false);
            isJumping = false;
        }
    }

    // Utility /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool IsFlying()
    {
        return isFlying;
    }

    // Collision Detection /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {//player is grounded
            Jetpack(false, true);
            Jump(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {//player is in air
            Jetpack(true, true);
            Jump(true);
        }
    }

}
