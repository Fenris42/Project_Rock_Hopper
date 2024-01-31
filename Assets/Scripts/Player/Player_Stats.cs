using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    [Header("Warnings")]
    [SerializeField] private int warningThreshold;
    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [Header("Oxygen")]
    [SerializeField] private float maxOxygen;
    [SerializeField] private float oxygenDrain;
    [SerializeField] private float noOxygenHealthDrain;
    [Header("Energy")]
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyDrain;
    [SerializeField] private float noEnergyHealthDrain;
    [Header("Fuel")]
    [SerializeField] private float maxFuel;

    //stat bars
    //health
    private Stat_Bar healthBar;
    private Image healthWarning;
    private float health;

    //oxygen
    private Stat_Bar oxygenBar;
    private Image oxygenWarning;
    private float oxygen;

    //energy
    private Stat_Bar energyBar;
    private Image energyWarning;
    private float energy;

    //fuel
    private Stat_Bar fuelBar;
    private Image fuelWarning;
    private float fuel;

    //buff bar
    private GameObject noOxygenDebuff;
    private GameObject freezingDebuff;
    
    private GameState gameState;
    private float timer;
    private bool warning;
    private bool statDrain;



    void Start()
    {// Start is called before the first frame update

        //get components
        healthBar = GameObject.Find("UI/Canvas/HUD/Stat Bars/Health Bar").GetComponent<Stat_Bar>();
        oxygenBar = GameObject.Find("UI/Canvas/HUD/Stat Bars/Oxygen Bar").GetComponent<Stat_Bar>();
        energyBar = GameObject.Find("UI/Canvas/HUD/Stat Bars/Energy Bar").GetComponent<Stat_Bar>();
        fuelBar = GameObject.Find("UI/Canvas/HUD/Stat Bars/Fuel Bar").GetComponent<Stat_Bar>();
        healthWarning = GameObject.Find("UI/Canvas/HUD/Stat Bars/Health Bar/Warning/Sprite").GetComponent<Image>();
        oxygenWarning = GameObject.Find("UI/Canvas/HUD/Stat Bars/Oxygen Bar/Warning/Sprite").GetComponent<Image>();
        energyWarning = GameObject.Find("UI/Canvas/HUD/Stat Bars/Energy Bar/Warning/Sprite").GetComponent<Image>();
        fuelWarning = GameObject.Find("UI/Canvas/HUD/Stat Bars/Fuel Bar/Warning/Sprite").GetComponent<Image>();
        gameState = GameObject.Find("Game Logic").GetComponent<GameState>();
        noOxygenDebuff = GameObject.Find("UI/Canvas/HUD/Stat Bars/Buff Bar/No O2");
        freezingDebuff = GameObject.Find("UI/Canvas/HUD/Stat Bars/Buff Bar/Freezing");

        //initialize stats
        health = maxHealth;
        oxygen = maxOxygen;
        energy = maxEnergy;
        fuel = maxFuel;

        //initialize stat bars
        healthBar.Initialize(maxHealth);
        oxygenBar.Initialize(maxOxygen);
        energyBar.Initialize(maxEnergy);
        fuelBar.Initialize(maxFuel);

        //initialize warnings
        healthWarning.enabled = false;
        oxygenWarning.enabled = false;
        energyWarning.enabled = false;
        fuelWarning.enabled = false;
        
        //initialize buff bar
        noOxygenDebuff.SetActive(false);
        freezingDebuff.SetActive(false);
    }
        
    void Update()
    {// Update is called once per frame

        //convert frames to seconds
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            //update stats
            Stats();

            //check stats for warnings
            Warnings();
                        
            //reset timer
            timer = 0;
        }
    }

    // Methods ////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Stats()
    {//manages stat drains and regens

        if (statDrain == true)
        {
            //drain stats from life support
            Remove_Oxygen(oxygenDrain);
            Remove_Energy(energyDrain);

            if (oxygen == 0)
            {//damage player if out of oxygen (life support)
                Remove_Health(noOxygenHealthDrain);
            }

            if (energy == 0)
            {//damage player if out of energy (life support)
                Remove_Health(noEnergyHealthDrain);
            }

            if (health == 0)
            {//delay die slightly for healthbar to catch up
                Invoke("Die", 1f);
            }
        }
        else
        {
            //health regen
            if (health < maxHealth)
            {
                Add_Health(healthRegen);
            }
        }
    }

    private void Warnings()
    {
        //toggle for flashing warnings
        switch (warning)
        {
            case true:
                warning = false;
                break;
            case false:
                warning = true;
                break;
        }

        //health//////////////////////////////////////////////
        if (((health / maxHealth) * 100) <= warningThreshold)
        {
            healthWarning.enabled = warning;
        }
        else
        {
            healthWarning.enabled = false;
        }

        //oxygen//////////////////////////////////////////////
        //warning
        if (((oxygen / maxOxygen) * 100) <= warningThreshold)
        {
            oxygenWarning.enabled = warning;
        }
        else
        {
            oxygenWarning.enabled = false;
        }

        //no 02 debuff
        if (oxygen == 0)
        {
            noOxygenDebuff.SetActive(true);
        }
        else
        {
            noOxygenDebuff.SetActive(false);
        }

        //energy//////////////////////////////////////////////
        //warning
        if (((energy / maxEnergy) * 100) <= warningThreshold)
        {
            energyWarning.enabled = warning;
        }
        else
        {
            energyWarning.enabled = false;
        }

        //freezing debuff
        if (energy == 0)
        {
            freezingDebuff.SetActive(true);
        }
        else
        {
            freezingDebuff.SetActive(false);
        }

        //fuel////////////////////////////////////////////
        //warning
        if (((fuel / maxFuel) * 100) <= warningThreshold)
        {
            fuelWarning.enabled = warning;
        }
        else
        {
            fuelWarning.enabled = false;
        }
    }

    private void Die()
    {//player is out of health, display game over screen
        gameState.GameOver();
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

    public void Add_Fuel(float amount)
    {//add energy and update HUD stat bar
        fuel += amount;
        Sanitize();
        fuelBar.Add(amount);
    }

    public void Remove_Fuel(float amount)
    {//remove energy and update HUD stat bar
        fuel -= amount;
        Sanitize();
        fuelBar.Remove(amount);
    }

    public void Set_StatDrain(bool value)
    {
        statDrain = value;
    }

    //Get Methods /////////////////////////////////////////////////////////////////////////////////////////////////////
    public float Get_Health() 
    { 
        return health; 
    }

    public float Get_MaxHealth()
    {
        return maxHealth;
    }

    public float Get_Oxygen()
    {
        return oxygen;
    }

    public float Get_Fuel()
    {
        return fuel;
    }

    public float Get_MaxOxygen()
    {
        return maxOxygen;
    }

    public float Get_Energy()
    {
        return energy;
    }

    public float Get_MaxEnergy()
    {
        return maxEnergy;
    }

    public float Get_MaxFuel()
    {
        return maxFuel;
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

        //fuel
        if (fuel < 0)
        {
            fuel = 0;
        }
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }
}
