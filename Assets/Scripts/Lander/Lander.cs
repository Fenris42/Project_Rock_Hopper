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
    }

    public void EnterLander()
    {
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

    public void ExitLander()
    {
        //player is now outside lander
        playerOnBoard = false;

        //toggle stat regen off
        playerStats.Set_EVA(true);

        //enable player sprite
        playerSprite.enabled = true;

        //disable landers floor
        floor.enabled = false;
    }

    public bool Get_PlayerOnBoard()
    {
        return playerOnBoard;
    }

    public bool Get_PlayerInRange()
    {
        return doorRange.PlayerInRange();
    }
}
