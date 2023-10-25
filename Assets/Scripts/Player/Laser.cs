using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Laser : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] LayerMask laserTargets;

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
        //DebugDig();


    }

    private void PlayerInput()
    {
        if (Input.GetMouseButton(0))
        {//left mouse click
            Fire();
        }
        else
        {
            animator.SetBool("Dig", false);
            laserSprite.enabled = false;
        }
    }

    private void Fire()
    {//fire laser and check for contacts

        laserSprite.enabled = true;
        animator.SetBool("Dig", true);

        RaycastHit2D ray = new RaycastHit2D();

        if (player.transform.localScale.x == 1)
        {//facing right
            ray = Physics2D.Raycast(transform.position, Vector2.right, range, laserTargets);
        }
        else if (player.transform.localScale.x == -1)
        {//facing left
            ray = Physics2D.Raycast(transform.position, Vector2.left, range, laserTargets);
        }

        if (ray.collider == true)
        {//ray has a hit return

            if (ray.collider.gameObject.CompareTag("Ground"))
            {//hit is minable

                //get coords of ray contact and translate into tile coordinates
                Vector3 hitpos = Vector3.zero;
                hitpos.x = ray.point.x - 0.01f * ray.normal.x;
                hitpos.y = ray.point.y - 0.01f * ray.normal.y;
                Vector3Int tile = tilemap.WorldToCell(hitpos);

                //set tile to empty
                tilemap.SetTile(tile, null);
            }
        }
    }

    private void DebugDig()
    {//deletes tile on click (temporary code)

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3Int tile = tilemap.WorldToCell(mousePos);
            tilemap.SetTile(tile, null);
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
            offset = (range / 2) + 0.5f;
        }

        //right
        Gizmos.DrawWireCube(new Vector2(x + offset, y), new Vector2(range, 0.25f));
    }
    

}
