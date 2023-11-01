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
    private int ore;

    private TMP_Text massText;
    private TMP_Text oreText;


    void Start()
    {// Start is called before the first frame update

        //get components
        massText = GameObject.Find("HUD/Canvas/Inventory/Background/Mass").GetComponent<TMP_Text>();
        oreText = GameObject.Find("HUD/Canvas/Inventory/Background/Ore/Count").GetComponent<TMP_Text>();

        //initialize fields
        mass = 0;
        ore = 0;
        UpdateHUD();

    }
        
    void Update()
    {// Update is called once per frame

    }

    public void AddItem(GameObject item)
    {
        if (item.CompareTag("Ore"))
        {
            ore += 1;
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        //mass
        mass = ore;
        massText.text = mass + " / " + maxMass + " KG";

        //ore
        oreText.text = ore.ToString();
    }
}
