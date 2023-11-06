using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    //serialied fields
    [SerializeField] int maxMass;

    //private variables
    private int mass;
    private TMP_Text massText;

    private int rock;
    private TMP_Text rockText;

    private int ice;
    private TMP_Text iceText;

    


    void Start()
    {// Start is called before the first frame update

        //get components
        massText = GameObject.Find("HUD/Canvas/Inventory/Background/Mass").GetComponent<TMP_Text>();
        rockText = GameObject.Find("HUD/Canvas/Inventory/Background/Rock/Count").GetComponent<TMP_Text>();
        iceText = GameObject.Find("HUD/Canvas/Inventory/Background/Ice/Count").GetComponent<TMP_Text>();

        //initialize fields
        mass = 0;
        ice = 0;
        rock = 0;
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
                case "Ice_Ore":
                    ice += 1;
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
        mass = rock + ice;
        massText.text = mass + " / " + maxMass + " KG";

        //counters
        rockText.text = rock.ToString();
        iceText.text = ice.ToString();
    }
}
