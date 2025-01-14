using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Asteroid_FOW : MonoBehaviour
{
    [SerializeField] private Tile fowSemi;
    private GameObject player;
    private Tilemap fowTilemap;
    

        
    void Start()
    {// Start is called before the first frame update
        //get components
        player = GameObject.Find("Player");
        fowTilemap = GameObject.Find("Level/Grid/FOW Tiles").GetComponent<Tilemap>();

    }
        
    void Update()
    {// Update is called once per frame
        Reveal();
    }

    private void Reveal()
    {
        //player position
        int x = Mathf.FloorToInt(player.transform.position.x);
        int y = Mathf.FloorToInt(player.transform.position.y);

        //semi reveal a 5x5 area around the player
        //fully reveal a 3x3 area around the player
        for (int yi = 2; yi >= -2; yi--)
        {//y top to bottom

            for (int xi = -2; xi <= 2; xi++)
            {//x left to right

                Vector3Int tile = new Vector3Int(x + xi, y + yi, 0);

                if (xi == -2 || xi == 2 || yi == -2 || yi == 2)
                {//outside edge of 5x5 to be semi transparent

                    if (y + yi < -1)
                    {//return tile to semi transparent if moved away and at least layer 2 below surface
                        fowTilemap.SetTile(tile, fowSemi);
                    }
                }
                else
                {//inside 3x3 area to be fully transparent around player
                    fowTilemap.SetTile(tile, null);
                }
            }
        }
    }
}
