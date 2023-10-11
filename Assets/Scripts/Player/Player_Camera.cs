using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    private Camera camera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //player cooords
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = camera.transform.position.z;

        //set camera
        camera.transform.position = new Vector3(x, y , z);

    }
}
