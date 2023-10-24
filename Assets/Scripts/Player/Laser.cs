using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Laser : MonoBehaviour
{
    [SerializeField] float range;
    private Tilemap tilemap;
    private Animator animator;
    private GameObject player;

    void Start()
    {// Start is called before the first frame update

        tilemap = GameObject.Find("Destructible Tiles").GetComponent<Tilemap>();
        animator = transform.Find("Laser").GetComponent<Animator>();
        player = GameObject.Find("Player");

    }
        
    void Update()
    {// Update is called once per frame

        PlayerInput();
        DebugDig();


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
        }
    }

    private void Fire()
    {
        animator.SetBool("Dig", true);

        //player coords
        float px = player.transform.position.x;
        float py = player.transform.position.y;

        //coords for box overlap centerpoint
        float offset;

        if (range <= 1)
        {
            offset = 1;
        }
        else
        {
            offset = (range / 2) + 0.5f;
        }

        //get all targets in range of laser
        Collider2D[] targets = { };

        if (player.transform.localScale.x == 1)
        {//facing right

            targets = Physics2D.OverlapBoxAll(new Vector2(px + offset, py), new Vector2(range, 0.1f), 0);

        }
        else if (player.transform.localScale.x == -1)
        {//facing left
            targets = Physics2D.OverlapBoxAll(new Vector2(px - offset, py), new Vector2(range, 0.1f), 0);
        }

        
        foreach (Collider2D target in targets)
        {
            if (target.tag == "Ground")
            {//target is minable
                Vector3Int tile = tilemap.WorldToCell(target.transform.position);
                tilemap.SetTile(tile, null);
            }
        }
    }

    private void DebugDig()
    {
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

        //left
        Gizmos.DrawWireCube(new Vector2(x - offset, y), new Vector2(range, 0.1f));

        //right
        Gizmos.DrawWireCube(new Vector2(x + offset, y), new Vector2(range, 0.1f));
    }
}
