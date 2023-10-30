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
    [SerializeField] LayerMask fireMask;
    [SerializeField] LayerMask pickupMask;

    private Tilemap tilemap;
    private Animator animator;
    private GameObject player;
    private SpriteRenderer laserSprite;



    void Start()
    {// Start is called before the first frame update

        tilemap = GameObject.Find("Destructible Tiles").GetComponent<Tilemap>();
        animator = GameObject.Find("Player/Laser").GetComponent<Animator>();
        player = GameObject.Find("Player");
        laserSprite = GameObject.Find("Player/Laser").GetComponent<SpriteRenderer>();
        laserSprite.enabled = false;
    }
        
    void Update()
    {// Update is called once per frame

        PlayerInput();
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

            //fire ray to detect interactable objects
            RaycastHit2D ray = Ray(fireMask);

            if (ray.collider == true)
            {//ray has a hit return

                if (ray.collider.gameObject.CompareTag("Ground"))
                {//hit is minable
                    Mine(ray);      
                }
            }
        }
        else
        {//reset laser animation
            animator.SetBool("Fire", false);
            laserSprite.enabled = false;
        }
    }

    private void Mine(RaycastHit2D ray)
    {
        //get coords of ray contact and translate into tile coordinates
        Vector3 hitpos = Vector3.zero;
        hitpos.x = ray.point.x - 0.01f * ray.normal.x;
        hitpos.y = ray.point.y - 0.01f * ray.normal.y;
        Vector3Int tile = tilemap.WorldToCell(hitpos);

        //get tile info
        var tileinfo = tilemap.GetTile(tile);

        if (tileinfo.name == "Regolith")
        {//mine quick and drop nothing
            tilemap.SetTile(tile, null);
        }
        else if (tileinfo.name == "Ore")
        {//drop ore

            //get ore prefab
            Instantiate(Resources.Load("Ore"), hitpos, Quaternion.identity);
            tilemap.SetTile(tile, null);
        }
    }

    private void Suck(bool suck)
    {//suck up drops

        if (suck == true)
        {
            //enable sucking animation
            laserSprite.enabled = true;
            animator.SetBool("Suck", true);

            //fire ray to detect interactable objects
            RaycastHit2D ray = Ray(pickupMask);

            if (ray.collider == true)
            {//ray has a hit return

                //convert ray to game object
                GameObject item = ray.collider.gameObject;

                //pickup item
                Pickup(item);
            }
        }
        else
        {//reset animation
            animator.SetBool("Suck", false);
            laserSprite.enabled = false;
        }
    }

    private void Pickup(GameObject item)
    {//pickup item

        if (item.CompareTag("Ore"))
        {//Add resource

            //delete drop game object
            Destroy(item);
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
