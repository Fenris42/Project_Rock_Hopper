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
    [SerializeField] float mineEnergy;
    [SerializeField] float suckEnergy;
    [SerializeField] LayerMask fireMask;
    [SerializeField] LayerMask pickupMask;

    private Animator animator;
    private GameObject player;
    private SpriteRenderer laserSprite;
    private Player_Inventory playerInventory;
    private Asteroid_Mining mining;
    private Player_Stats playerStats;
    


    void Start()
    {// Start is called before the first frame update

        //get components
        animator = GameObject.Find("Player/Laser").GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerInventory = player.GetComponent<Player_Inventory>();
        laserSprite = GameObject.Find("Player/Laser").GetComponent<SpriteRenderer>();
        mining = GameObject.Find("Level").GetComponent<Asteroid_Mining>();
        playerStats = player.GetComponent <Player_Stats>();

        //initialize states
        laserSprite.enabled = false;
    }

    public void Fire(bool fire)
    {//fire laser and check for contacts

        if (fire == true && playerStats.Get_Energy() > 0)
        {//fire laser

            //enable mining animation
            laserSprite.enabled = true;
            laserSprite.color = Color.white;
            animator.SetBool("Fire", true);

            //drain energy
            playerStats.Remove_Energy(mineEnergy * Time.deltaTime);

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
        }
    }

    public void Suck(bool suck)
    {//suck up drops

        if (suck == true && playerStats.Get_Energy() > 0)
        {
            //enable sucking animation
            laserSprite.enabled = true;
            laserSprite.color = Color.green;
            animator.SetBool("Suck", true);

            //drain energy
            playerStats.Remove_Energy(suckEnergy * Time.deltaTime);

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
        }
    }

    //Utility Methods //////////////////////////////////////////////////////////////////////////////////////////////////
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
