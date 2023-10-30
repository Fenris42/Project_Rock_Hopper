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
    private GameObject player;


    
    void Start()
    {// Start is called before the first frame update

        animator = transform.Find("Sprite").GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
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
            Jetpack(true);
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
        animator.SetBool("Run", true);
        //player.transform.localScale = new Vector3(-1, 1, 1);
        player.transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        animator.SetBool("Run", true);
        //player.transform.localScale = new Vector3(1, 1, 1);
        player.transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
    }

    private void Jetpack(bool on)
    {
        if (on == true)
        {//jetpack on

         //resets downward gravity velocity to prevent stacking gravity while in flight
            rigidbody.velocity = Vector3.zero;

            //fly
            animator.SetBool("Fly", true);
            player.transform.position += (Vector3.up * flySpeed) * Time.deltaTime;
        }
        else
        {//jetpack off
            animator.SetBool("Fly", false);
        }
        
    }

    // Collision Detection /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {//player is grounded
            Jetpack(false);
        }
    }

}
