using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] int maxMass;

    private int mass;
    private TMP_Text massText;
    //Rock
    private int rock;
    private TMP_Text rockText;
    //Ice
    private int ice;
    private TMP_Text iceText;
    //Iron
    private int iron;
    private TMP_Text ironText;
    //Copper
    private int copper;
    private TMP_Text copperText;
    //Gold
    private int gold;
    private TMP_Text goldText;
    //Titanium
    private int titanium;
    private TMP_Text titaniumText;

    


    void Start()
    {// Start is called before the first frame update

        //get components
        massText = GameObject.Find("HUD/Canvas/Inventory/Background/Mass").GetComponent<TMP_Text>();
        rockText = GameObject.Find("HUD/Canvas/Inventory/Background/Rock/Count").GetComponent<TMP_Text>();
        iceText = GameObject.Find("HUD/Canvas/Inventory/Background/Ice/Count").GetComponent<TMP_Text>();
        ironText = GameObject.Find("HUD/Canvas/Inventory/Background/Iron/Count").GetComponent<TMP_Text>();
        copperText = GameObject.Find("HUD/Canvas/Inventory/Background/Copper/Count").GetComponent<TMP_Text>();
        goldText = GameObject.Find("HUD/Canvas/Inventory/Background/Gold/Count").GetComponent<TMP_Text>();
        titaniumText = GameObject.Find("HUD/Canvas/Inventory/Background/Titanium/Count").GetComponent<TMP_Text>();

        //initialize fields
        mass = 0;
        ice = 0;
        rock = 0;
        iron = 0;
        copper = 0;
        gold = 0;
        titanium = 0;
        UpdateHUD();

    }
        
    void Update()
    {// Update is called once per frame

    }

    public void Pickup(GameObject item)
    {//pickup item

        if (mass < maxMass)
        {
            //add item to inventory
            switch (item.tag)
            {
                case "Rock":
                    rock += 1;
                    break;
                case "Ice":
                    ice += 1;
                    break;
                case "Iron":
                    iron += 1;
                    break;
                case "Copper":
                    copper += 1;
                    break;
                case "Gold":
                    gold += 1;
                    break;
                case "Titanium":
                    titanium += 1;
                    break;
            }

            UpdateHUD();

            //destroy item
            Destroy(item);
        }
    }

    private void UpdateHUD()
    {
        //mass
        mass = rock + ice + iron + copper + gold + titanium;
        massText.text = mass + " / " + maxMass + " KG";

        //counters
        rockText.text = rock.ToString();
        iceText.text = ice.ToString();
        ironText.text = iron.ToString();
        copperText.text = copper.ToString();
        goldText.text = gold.ToString();
        titaniumText.text = titanium.ToString();

    }
}
