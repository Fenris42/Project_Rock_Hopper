using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat_Bar : MonoBehaviour
{
    private Slider fillSlider;
    private Slider changeSlider;
    private float current;
    private float max;



    void Start()
    {// Start is called before the first frame update
        fillSlider = GameObject.Find(gameObject.name + "/Border/Fill Slider").GetComponent<Slider>();
        changeSlider = GameObject.Find(gameObject.name + "/Border/Change Slider").GetComponent<Slider>();
    }

    void Update()
    {// Update is called once per frame

    }

    public void Initialize(float amount)
    {//initialize bars current value
        current = amount;
        max = amount;
    }

    public void Add(float amount)
    {//increase stat bar and play animation

        //add amount from bar
        current += amount;

        //keep values in range
        Sanitize();

        //apply updates to secondary bar
        UpdateChangeBar();

        //delay updating the main bar for 1 sec
        Invoke("UpdateFillBar", 0.5f);

    }

    public void Remove(float amount)
    {//reduce stat bar and play animation

        //remove amount from bar
        current -= amount;

        //keep values in range
        Sanitize();

        //apply updates to main bar
        UpdateFillBar();

        //delay updating the secondary bar for 1 sec
        Invoke("UpdateChangeBar", 0.5f);
    }

    private void Sanitize()
    {//ensure values stay in range

        if (current < 0)
        {
            current = 0;
        }
        if (current > max)
        {
            current = max;
        }
    }

    private void UpdateFillBar()
    {//set slider to percentage of current and max values

        //get bar fill ratio
        float percentage = current / max;

        //scale health bar fill
        fillSlider.value = percentage;

    }

    private void UpdateChangeBar()
    {//set slider to percentage of current and max values

        //get bar fill ratio
        float percentage = current / max;

        //scale health bar fill
        changeSlider.value = percentage;

    }
}