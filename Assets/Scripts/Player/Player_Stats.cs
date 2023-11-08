using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int healthRegen;
    [Header("Oxygen")]
    [SerializeField] private int maxOxygen;
    [SerializeField] private int oxygenRegen;
    [SerializeField] private int oxygenDrain;
    [Header("Energy")]
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRegen;
    [SerializeField] private int flyEnergy;
    [SerializeField] private int mineEnergy;
    [SerializeField] private int suckEnergy;

    private int health;
    private int oxygen;
    private int energy;
    private Stat_Bar healthBar;
    private Stat_Bar oxygenBar;
    private Stat_Bar energyBar;
    private float timer;
    private Player_Movement playerMovement;
    private Laser laser;
    private GameObject player;
    private bool isOutside;


    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<Player_Movement>();
        laser = player.GetComponent <Laser>();
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
            
            //drain energy
            if (playerMovement.IsFlying() == true)
            {//flying
                Remove_Energy(flyEnergy);
            }
            if (laser.IsMining() == true)
            {//mining
                Remove_Energy(mineEnergy);
            }
            if (laser.IsSucking() == true)
            {//sucking
                Remove_Energy(suckEnergy);
            }

            //reset timer
            timer = 0;
        }
    }

    public void Add_Health(int amount)
    {//add health and update stat bar
        health += amount;
        Sanitize();
        healthBar.Add(amount);
    }

    public void Remove_Health(int amount)
    {//remove health and update HUD stat bar
        health -= amount;
        Sanitize();
        healthBar.Remove(amount);
    }

    public void Add_Oxygen(int amount)
    {//add oxygen and update HUD stat bar
        oxygen += amount;
        Sanitize();
        oxygenBar.Add(amount);
    }

    public void Remove_Oxygen(int amount)
    {//remove oxygen and update HUD stat bar
        oxygen -= amount;
        Sanitize();
        oxygenBar.Remove(amount);
    }

    public void Add_Energy(int amount)
    {//add energy and update HUD stat bar
        energy += amount;
        Sanitize();
        energyBar.Add(amount);
    }

    public void Remove_Energy(int amount)
    {//remove energy and update HUD stat bar
        energy -= amount;
        Sanitize();
        energyBar.Remove(amount);
    }

    public int Get_Energy()
    {
        return energy;
    }

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

    public void IsOutside(bool value)
    {//for oxygene drain togglling
        isOutside = value;
    }
}
