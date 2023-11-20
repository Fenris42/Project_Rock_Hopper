using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Check : MonoBehaviour
{
    private bool playerInRange;

    void Start()
    {// Start is called before the first frame update
        playerInRange = false;
    }

    public bool PlayerInRange()
    {//returns if player is inside trigger box
        return playerInRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
