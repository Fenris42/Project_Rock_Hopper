using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Player_Movement player_movement;
    private Player_Stats player_stats;
    private Lander lander;
    private Laser laser;

    private bool controlsEnabled;
    private bool playerControlsEnabled;
    private bool landerControlsEnabled;
    
    void Start()
    {// Start is called before the first frame update

        //get components
        player_movement = GameObject.Find("Player").GetComponent<Player_Movement>();
        player_stats = GameObject.Find("Player").GetComponent <Player_Stats>();
        lander = GameObject.Find("Lander").GetComponent<Lander>();
        laser = GameObject.Find("Player").GetComponent<Laser>();

    }

    
    void Update()
    {// Update is called once per frame

        PlayerInput();
    }

    private void PlayerInput()
    {
        if (controlsEnabled == true)
        {//game is running

            //Player Controls/////////////////////////////////////////////////////////
            if (playerControlsEnabled == true)
            {
                //point player in direction of mouse
                player_movement.Aim();

                //Movement///////////////////
                if (Input.GetKey(KeyCode.A))
                {//left
                    player_movement.MoveLeft();
                }
                else if (Input.GetKey(KeyCode.D))
                {//right
                    player_movement.MoveRight();
                }
                else
                {//return to idle
                    player_movement.Idle();
                }
                
                if (Input.GetKeyDown(KeyCode.Space))
                {//Jump
                    player_movement.Jump();
                }

                if (Input.GetKey(KeyCode.W))
                {//fly up
                    player_movement.JetpackUp();
                }
                else if (Input.GetKey(KeyCode.S))
                {//fly down
                    player_movement.JetpackDown();
                }
                else
                {//reset jetpack
                    player_movement.ResetJetpack();
                }



                //Laser///////////////////////////
                if (Input.GetMouseButton(0))
                {//left mouse click
                    laser.Fire(true);
                }
                else if (Input.GetMouseButton(1))
                {//right mouse click
                    laser.Suck(true);
                }
                else
                {//resets
                    laser.Fire(false);
                    laser.Suck(false);
                }
            }



            //Lander Controls/////////////////////////////////////////////////////////
            if (landerControlsEnabled == true)
            {

            }



            //Interact/////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Enter/Exit Lander
                if (lander.Get_PlayerOnBoard() == false && lander.Get_PlayerInRange() == true)
                {//player is not onboard and in range of door - > enter lander
                    lander.EnterLander();
                }
                else if (lander.Get_PlayerOnBoard() == true)
                {//player is inside lander - > exit lander
                    lander.ExitLander();
                }

            }



            //Menus/////////////////////////////////////////////////////////
        }

    }

    public void Set_PlayerControlsEnabled(bool value)
    {
        playerControlsEnabled = value;
    }

    public void Set_LanderControlsEnabled(bool value)
    {
        landerControlsEnabled = value;
    }

    public void Set_ControlsEnabled(bool value)
    {
        controlsEnabled = value;
    }
}
