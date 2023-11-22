using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Asteroid : MonoBehaviour
{
    private Asteroid_Generation generation;
    private Lander lander;
    private Player_Stats playerStats;


    void Start()
    {// Start is called before the first frame update
        
        //get components
        generation = GameObject.Find("Level").GetComponent<Asteroid_Generation>();
        lander = GameObject.Find("Lander").GetComponent<Lander>();
        playerStats = GameObject.Find("Player").GetComponent<Player_Stats>();

        Initialize();
    }

    public void Initialize()
    {//initialize level

        //generate level
        generation.Generate();

        //spawn lander in space
        Spawn();
        
    }

    public void Spawn()
    {//spawn player for first time

        ReSpawn(); //Temp Code
    }

    public void ReSpawn()
    {//respawn player after death

        //reposition player to inside lander
        lander.EnterLander();

        //reset player stats
        playerStats.Add_Health(playerStats.Get_MaxHealth());
        playerStats.Add_Oxygen(playerStats.Get_MaxOxygen());
        playerStats.Add_Energy(playerStats.Get_MaxEnergy());
    }
}
