using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float jetpackSpeed;
    [SerializeField] float jetpackFuel;
    [SerializeField] float velocityLimit;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private GameObject player;
    private Player_Stats playerStats;
    private GameState gameState;

    private bool isGrounded;
   

    
    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        rigidbody = player.GetComponent<Rigidbody2D>();
        animator = GameObject.Find("Player/Sprite").GetComponent<Animator>();
        playerStats = player.GetComponent<Player_Stats>();
        gameState = GameObject.Find("Game Logic").GetComponent<GameState>();
    }

    // Movement /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Aim()
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

    public void MoveLeft()
    {
        if (isGrounded == true)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        //temp code else where for player direction
        //player.transform.localScale = new Vector3(-1, 1, 1);

        if (isGrounded == true)
        {//player running
            player.transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
        }
        else
        {//player flying
            if (rigidbody.velocity.x > -velocityLimit)
            {
                rigidbody.velocity += (Vector2.left * moveSpeed) * Time.deltaTime;
            }
        }
    }

    public void MoveRight()
    {
        if (isGrounded == true)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        //temp code else where for player direction
        //player.transform.localScale = new Vector3(1, 1, 1);

        if (isGrounded == true)
        {//player running
            player.transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
        }
        else
        {//player flying
            if (rigidbody.velocity.x < velocityLimit)
            {
                rigidbody.velocity += (Vector2.right * moveSpeed) * Time.deltaTime;
            }
        }
    }

    public void Idle()
    {
        animator.SetBool("Run", false);
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            rigidbody.velocity += (Vector2.up * jumpSpeed);

            //prevent player from being yeeted into space if jumping on ore (shrugs)
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, jumpSpeed);
        }
    }

    public void JetpackUp()
    {
        if (playerStats.Get_Fuel() > 0)
        {//jetpack on

            //resets downward gravity velocity to prevent stacking gravity while in flight
            //rigidbody.velocity = Vector3.zero;

            //fly
            animator.SetBool("Jetpack", true);
            //player.transform.position += (Vector3.up * jetpackSpeed) * Time.deltaTime;

            if (rigidbody.velocity.y < velocityLimit)
            {
                rigidbody.velocity += (Vector2.up * jetpackSpeed) * Time.deltaTime;
            }
            
            //drain fuel
            playerStats.Remove_Fuel(jetpackFuel * Time.deltaTime);
        }
    }

    public void JetpackDown()
    {
        if (playerStats.Get_Fuel() > 0)
        {//jetpack on

            //resets downward gravity velocity to prevent stacking gravity while in flight
            //rigidbody.velocity = Vector3.zero;

            //fly
            animator.SetBool("Jetpack", true);
            //player.transform.position += (Vector3.up * jetpackSpeed) * Time.deltaTime;

            if (rigidbody.velocity.y > -velocityLimit)
            {
                rigidbody.velocity += (Vector2.down * jetpackSpeed) * Time.deltaTime;
            }

            //drain fuel
            playerStats.Remove_Fuel(jetpackFuel * Time.deltaTime);
        }
    }

    // Utility /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ResetJetpack()
    {
        animator.SetBool("Jetpack", false);
    }

    // Ground Check /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TO DO
    //Do ground check on if feet are touching ground only. Currently touching any ground in any direction is causing jump/fly resets incorrectly
    //Make room for edge case of player is standing on loot

    public bool Get_Grounded()
    {
        return isGrounded;
    }

    private void IsGrounded(bool grounded)
    {
        if (grounded == false)
        {//player not grounded
            animator.SetBool("Jump", true);
            isGrounded = false;
        }
        else
        {//player grounded
            animator.SetBool("Jump", false);
            isGrounded = true;

            //reset velocity
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {//player is grounded
            IsGrounded(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {//player is in air
            IsGrounded(false);
        }
    }
}
