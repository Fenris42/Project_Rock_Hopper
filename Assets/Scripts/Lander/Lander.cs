using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    private GameObject player;
    private Player_Stats playerStats;
    private Range_Check doorRange;
    private SpriteRenderer playerSprite;
    private SpriteRenderer laserSprite;
    private GameObject playerSpawn;
    private BoxCollider2D floor;
    private Controls controls;
    private bool playerOnBoard;

    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<Player_Stats>();
        playerSprite = GameObject.Find("Player/Sprite").GetComponent<SpriteRenderer>();
        laserSprite = GameObject.Find("Player/Laser").GetComponent<SpriteRenderer>();
        doorRange = GameObject.Find("Lander/Colliders/Door").GetComponent<Range_Check>();
        playerSpawn = GameObject.Find("Lander/Player Spawn");
        floor = GameObject.Find("Lander/Colliders/Floor").GetComponent <BoxCollider2D>();
        controls = GameObject.Find("Game Logic").GetComponent<Controls>();

    }

    public void EnterLander()
    {
        //player is now onboard lander
        playerOnBoard = true;

        //disable stat drain and enable regen
        playerStats.Set_StatDrain(false);

        //disable player controls
        controls.Set_PlayerControlsEnabled(false);

        //enable lander controls
        controls.Set_LanderControlsEnabled(true);

        //disable player sprite
        playerSprite.enabled = false;
        laserSprite.enabled = false;

        //enable landers floor
        floor.enabled = true;

        //move player to landers spawn position
        player.transform.position = playerSpawn.transform.position;
    }

    public void ExitLander()
    {
        //player is now outside lander
        playerOnBoard = false;

        //enable stat drain and disable regen
        playerStats.Set_StatDrain(true);

        //enable player controls
        controls.Set_PlayerControlsEnabled(true);

        //disable lander controls
        controls.Set_LanderControlsEnabled(false);

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
