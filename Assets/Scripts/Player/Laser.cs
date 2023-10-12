using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Laser : MonoBehaviour
{
    private Tilemap tilemap;
    private Animator animator;

    void Start()
    {// Start is called before the first frame update

        tilemap = GameObject.Find("Destructible Tiles").GetComponent<Tilemap>();
        animator = transform.Find("Laser").GetComponent<Animator>();
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
            Dig();
        }
        else
        {
            animator.SetBool("Dig", false);
        }
    }

    private void Dig()
    {
        animator.SetBool("Dig", true);
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
}
