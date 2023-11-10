using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float healthRegen;
    [Header("Oxygen")]
    [SerializeField] private int maxOxygen;
    [SerializeField] private float oxygenRegen;
    [SerializeField] private float oxygenDrain;
    [Header("Energy")]
    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRegen;

    private Stat_Bar healthBar;
    private Stat_Bar oxygenBar;
    private Stat_Bar energyBar;

    private float health;
    private float oxygen;
    private float energy;
    private bool isOutside;

    private float timer;


    void Start()
    {// Start is called before the first frame update

        //get components
        healthBar = GameObject.Find("HUD/Canvas/Health Bar").GetComponent<Stat_Bar>();
        oxygenBar = GameObject.Find("HUD/Canvas/Oxygen Bar").GetComponent<Stat_Bar>();
        energyBar = GameObject.Find("HUD/Canvas/Energy Bar").GetComponent<Stat_Bar>();

        //initialize stats and bars
        health = maxHealth;
        oxygen = maxOxygen;
        energy = maxEnergy;
        healthBar.Initialize(maxHealth);
        oxygenBar.Initialize(maxOxygen);
        energyBar.Initialize(maxEnergy);
                
        //initialize states
        isOutside = true;
    }
        
    void Update()
    {// Update is called once per frame

        //convert frames to seconds
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (isOutside == true)
            {//drain oxygen
                Remove_Oxygen(oxygenDrain);
            }
            else
            {//regen stats
                Add_Health(healthRegen);
                Add_Oxygen(oxygenRegen);
                Add_Energy(energyRegen);
            }

            //reset timer
            timer = 0;
        }
    }

    //Set Methods /////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Add_Health(float amount)
    {//add health and update stat bar
        health += amount;
        Sanitize();
        healthBar.Add(amount);
    }

    public void Remove_Health(float amount)
    {//remove health and update HUD stat bar
        health -= amount;
        Sanitize();
        healthBar.Remove(amount);
    }

    public void Add_Oxygen(float amount)
    {//add oxygen and update HUD stat bar
        oxygen += amount;
        Sanitize();
        oxygenBar.Add(amount);
    }

    public void Remove_Oxygen(float amount)
    {//remove oxygen and update HUD stat bar
        oxygen -= amount;
        Sanitize();
        oxygenBar.Remove(amount);
    }

    public void Add_Energy(float amount)
    {//add energy and update HUD stat bar
        energy += amount;
        Sanitize();
        energyBar.Add(amount);
    }

    public void Remove_Energy(float amount)
    {//remove energy and update HUD stat bar
        energy -= amount;
        Sanitize();
        energyBar.Remove(amount);
    }

    public void IsOutside(bool value)
    {//for oxygen drain toggling
        isOutside = value;
    }

    //Get Methods /////////////////////////////////////////////////////////////////////////////////////////////////////
    public float Get_Health() 
    { 
        return health; 
    }

    public float Get_Oxygen()
    {
        return oxygen;
    }

    public float Get_Energy()
    {
        return energy;
    }

    //Utility Methods /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Sanitize()
    {//ensure values stay in range

        //health
        if (health < 0)
        {
            health = 0;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //oxygen
        if (oxygen < 0)
        {
            oxygen = 0;
        }
        if (oxygen > maxOxygen)
        {
            oxygen = maxOxygen;
        }

        //energy
        if (energy < 0)
        {
            energy = 0;
        }
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
