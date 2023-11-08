using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class Laser : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int energyConsumption;
    [SerializeField] LayerMask fireMask;
    [SerializeField] LayerMask pickupMask;

    private Animator animator;
    private GameObject player;
    private SpriteRenderer laserSprite;
    private Player_Stats playerStats;
    private Player_Inventory playerInventory;
    private Mining mining;
    private float energyTimer;
    private float crackTimer;
    private bool laserRunning;
    


    void Start()
    {// Start is called before the first frame update

        //get components
        animator = GameObject.Find("Player/Laser").GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<Player_Stats>();
        playerInventory = player.GetComponent<Player_Inventory>();
        laserSprite = GameObject.Find("Player/Laser").GetComponent<SpriteRenderer>();
        laserSprite.enabled = false;
        mining = GameObject.Find("Level").GetComponent<Mining>();
    }
        
    void Update()
    {// Update is called once per frame

        PlayerInput();

        if (laserRunning == true)
        {//drain energy

         //convert frames to seconds
            energyTimer += Time.deltaTime;
            if (energyTimer > 1)
            {
                playerStats.Remove_Energy(energyConsumption);
                energyTimer = 0;
            }
        }
        else
        {//reset timer
            energyTimer = 0;
        }
    }

    private void PlayerInput()
    {
        if (Input.GetMouseButton(0))
        {//left mouse click
            Fire(true);
        }
        else if (Input.GetMouseButton(1))
        {//right mouse click
            Suck(true);
        }
        else
        {//animation resets
            Fire(false);
            Suck(false);
        }
    }

    private RaycastHit2D Ray(LayerMask layermask)
    {
        //convert players rotation into a vector2 for raycast
        Vector2 direction;

        //get direction/angle to fire ray in
        if (player.transform.localScale.x == 1)
        {//player facing right
            direction = transform.right;
        }
        else
        {//player facing left
            direction = transform.right * -1;
        }

        //fire ray to detect targetable objects
        RaycastHit2D ray = new RaycastHit2D();
        ray = Physics2D.Raycast(player.transform.position, direction, range, layermask);

        return ray;
    }

    private void Fire(bool fire)
    {//fire laser and check for contacts

        if (fire == true)
        {//fire laser

            //enable mining animation
            laserSprite.enabled = true;
            animator.SetBool("Fire", true);

            //drain energy
            laserRunning = true;

            //fire ray to detect interactable objects
            RaycastHit2D ray = Ray(fireMask);

            if (ray.collider == true)
            {//ray has a hit return

                if (ray.collider.gameObject.CompareTag("Ground"))
                {//hit is minable
                    mining.Mine(ray);      
                }
            }
        }
        else
        {//reset laser
            animator.SetBool("Fire", false);
            laserSprite.enabled = false;
            laserRunning = false;
        }
    }

    private void Suck(bool suck)
    {//suck up drops

        if (suck == true)
        {
            //enable sucking animation
            laserSprite.enabled = true;
            animator.SetBool("Suck", true);

            //drain energy
            laserRunning = true;

            //fire ray to detect interactable objects
            RaycastHit2D ray = Ray(pickupMask);

            if (ray.collider == true)
            {//ray has a hit return

                //convert ray to game object
                GameObject item = ray.collider.gameObject;

                //pickup item
                playerInventory.Pickup(item);
            }
        }
        else
        {//reset laser
            animator.SetBool("Suck", false);
            laserSprite.enabled = false;
            laserRunning = false;
        }
    }

    private void OnDrawGizmosSelected()
    {//draw lasers range in editor

        //get players coordinates
        float x = transform.position.x;
        float y = transform.position.y;

        //used to find cubes center point in gizmo draw
        float offset;
                
        if (range <= 1)
        {//prevents range from being pushed into the player/mob
            offset = 1;
        }
        else
        {//correctly offsets the center point of the cube based on range
            offset = (range / 2);
        }

        //draw box in editor
        Gizmos.DrawWireCube(new Vector2(x + offset, y), new Vector2(range, 0.25f));
    }
}
