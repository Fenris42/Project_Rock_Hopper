using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    private Camera camera;
    private GameObject player;
    private bool enabled;
    
    void Start()
    {// Start is called before the first frame update

        camera = Camera.main;
        player = GameObject.Find("Player");
        enabled = true;
    }

    void Update()
    {// Update is called once per frame

        if (enabled == true)
        {
            FollowPlayer();
        }
    }

    public void Enabled(bool value)
    {//toggle follow cam on/off
        enabled = value;
    }

    private void FollowPlayer()
    {//Move camera to players coords

        //player coords
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = camera.transform.position.z;

        //set camera
        camera.transform.position = new Vector3(x, y , z);
    }
}
