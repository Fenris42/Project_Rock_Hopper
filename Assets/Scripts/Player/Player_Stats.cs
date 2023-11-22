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
    [SerializeField] private float oxygenRegen;
    [SerializeField] private float oxygenDrain;
    [SerializeField] private float noOxygenHealthDrain;
    [Header("Energy")]
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    private Stat_Bar healthBar;
    private Stat_Bar oxygenBar;
    private Stat_Bar energyBar;
    private Image healthWarning;
    private Image oxygenWarning;
    private Image energyWarning;

    private float health;
    private float oxygen;
    private float energy;

    private GameState gameState;
    private bool eva;
    private float timer;
    private bool warning;


    void Start()
    {// Start is called before the first frame update

        //get components
        healthBar = GameObject.Find("UI/Canvas/HUD/Health Bar").GetComponent<Stat_Bar>();
        oxygenBar = GameObject.Find("UI/Canvas/HUD/Oxygen Bar").GetComponent<Stat_Bar>();
        energyBar = GameObject.Find("UI/Canvas/HUD/Energy Bar").GetComponent<Stat_Bar>();
        healthWarning = GameObject.Find("UI/Canvas/HUD/Health Bar/Warning").GetComponent<Image>();
        oxygenWarning = GameObject.Find("UI/Canvas/HUD/Oxygen Bar/Warning").GetComponent<Image>();
        energyWarning = GameObject.Find("UI/Canvas/HUD/Energy Bar/Warning").GetComponent<Image>();
        gameState = GameObject.Find("Game State").GetComponent<GameState>();

        //initialize stats and bars
        health = maxHealth;
        oxygen = maxOxygen;
        energy = maxEnergy;
        healthBar.Initialize(maxHealth);
        oxygenBar.Initialize(maxOxygen);
        energyBar.Initialize(maxEnergy);
        healthWarning.enabled = false;
        oxygenWarning.enabled = false;
        energyWarning.enabled = false;
                
        //initialize states
        eva = false;
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

        if (eva == true)
        {//player is on EVA

            Remove_Oxygen(oxygenDrain);

            if (oxygen == 0)
            {//damage player if out of oxygen
                Remove_Health(noOxygenHealthDrain);
            }

            if (health == 0)
            {//delay die slightly for healthbar to catch up
                Invoke("Die", 1f);
            }
        }
        else
        {//player is in ship

            //health regen
            if (health < maxHealth)
            {
                Add_Health(healthRegen);
            }
            
            //oxygen regen
            if (oxygen < maxOxygen)
            {
                Add_Oxygen(oxygenRegen);
            }
            
            //energy regen
            if (energy < maxEnergy)
            {
                Add_Energy(energyRegen);
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

        //health
        if (((health / maxHealth) * 100) <= warningThreshold)
        {
            healthWarning.enabled = warning;
        }
        else
        {
            healthWarning.enabled = false;
        }

        //oxygen
        if (((oxygen / maxOxygen) * 100) <= warningThreshold)
        {
            oxygenWarning.enabled = warning;
        }
        else
        {
            oxygenWarning.enabled = false;
        }

        //energy
        if (((energy / maxEnergy) * 100) <= warningThreshold)
        {
            energyWarning.enabled = warning;
        }
        else
        {
            energyWarning.enabled = false;
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

    public void Set_EVA(bool value)
    {//for stat drain/regen toggling
        eva = value;
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

    public bool Get_EVA()
    {
        return eva;
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
