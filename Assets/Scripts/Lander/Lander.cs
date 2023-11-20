using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    private GameObject player;
    private Player_Stats playerStats;
    private Range_Check doorRange;
    private SpriteRenderer playerSprite;
    private GameObject playerSpawn;
    private BoxCollider2D floor;

    private bool playerOnBoard;

    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<Player_Stats>();
        playerSprite = GameObject.Find("Player/Sprite").GetComponent<SpriteRenderer>();
        doorRange = GameObject.Find("Lander/Colliders/Door").GetComponent<Range_Check>();
        playerSpawn = GameObject.Find("Lander/Player Spawn");
        floor = GameObject.Find("Lander/Colliders/Floor").GetComponent <BoxCollider2D>();

        //initialize variables
        Initialize();
    }
        
    void Update()
    {// Update is called once per frame
        PlayerInput();
    }

    private void Initialize()
    {//initialize players state to be inside of lander

        playerOnBoard = true;
        player.transform.position = playerSpawn.transform.position;
        playerSprite.enabled = false;
        playerStats.Set_EVA(false);
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {//Enter/Exit lander
            Boarding();
        }
    }

    private void Boarding()
    {//Enter/Exit lander

        if (playerOnBoard == false && doorRange.PlayerInRange() == true)
        {//player is not onboard and in range of door - > enter lander

            //player is now onboard lander
            playerOnBoard = true;

            //toggle stat regen on
            playerStats.Set_EVA(false);

            //disable player sprite
            playerSprite.enabled = false;

            //enable landers floor
            floor.enabled = true;

            //move player to landers spawn position
            player.transform.position = playerSpawn.transform.position;
        }
        else if (playerOnBoard == true)
        {//player is inside lander

            //player is now outside lander
            playerOnBoard = false;

            //toggle stat regen off
            playerStats.Set_EVA(true);

            //enable player sprite
            playerSprite.enabled = true;

            //disable landers floor
            floor.enabled = false;
        }
    }
}
