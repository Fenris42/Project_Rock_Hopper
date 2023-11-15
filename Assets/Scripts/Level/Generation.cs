using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [Header("Dimensions")]
    [SerializeField] private int spaceHeight;
    [SerializeField] private int groundWidth;
    [SerializeField] private int groundHeight;
    [Header("Background")]
    [SerializeField] private Tile space;
    [SerializeField] private Tile background;
    [Header("Ground")]
    [SerializeField] private Tile regolith;
    [SerializeField] private Tile bedrock;
    [Header("Ice")]
    [SerializeField] private Tile iceOre;
    [SerializeField] private int iceUpperSpawnLayer;
    [SerializeField] private int iceLowerSpawnLayer;
    [SerializeField] private int iceOrePercent;

    //tilemaps
    private Tilemap backgroundTilemap;
    private Tilemap groundTilemap;
    private Tilemap oreTilemap;
    private Tilemap fowTilemap;
    

    
    void Start()
    {// Start is called before the first frame update

        //get components
        backgroundTilemap = GameObject.Find("Level/Grid/Background Tiles").GetComponent<Tilemap>();
        groundTilemap = GameObject.Find("Level/Grid/Ground Tiles").GetComponent<Tilemap>();
        oreTilemap = GameObject.Find("Level/Grid/Ore Tiles").GetComponent<Tilemap>();
        fowTilemap = GameObject.Find("Level/Grid/FOW Tiles").GetComponent<Tilemap>();

        //generate level
        Generate();
    }

    void Update()
    {// Update is called once per frame

    }

    private void Generate()
    {//generate level

        Vector3Int tile = new Vector3Int(0, 0, 0);
        int random = 0;

        //space tile generation
        for (int y = (spaceHeight - 1); y >= 0; y--)
        {//y top to bottom

            for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                backgroundTilemap.SetTile(tile, space);
            }
        }

        //background generation
        for (int y = -1; y >= (-groundHeight); y--)
        {//y top to bottom

            for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                backgroundTilemap.SetTile(tile, background);
            }
        }

        //regolith generation
        for (int y = -1; y >= (-groundHeight); y--)
        {//y top to bottom

            for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                groundTilemap.SetTile(tile, regolith);
            }
        }

        //bedrock generation
        for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
        {//x left to right

            //set tile
            int y = -(groundHeight + 1);
            tile = new Vector3Int(x, y, 0);
            groundTilemap.SetTile(tile, bedrock);
        }

        //ice ore generation
        for (int y = -iceUpperSpawnLayer; y >= (-iceLowerSpawnLayer); y--)
        {//y top to bottom

            for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
            {//x left to right

                random = Random.Range(1, 101);
                if (random <= iceOrePercent)
                {
                    //set tile
                    tile = new Vector3Int(x, y, 0);
                    oreTilemap.SetTile(tile, iceOre);
                }
            }
        }
    }
}
