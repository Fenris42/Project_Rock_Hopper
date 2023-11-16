using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [Header("Dimensions")]
    [SerializeField] private int spaceHeight;
    [SerializeField] private int groundHeight;
    [SerializeField] private int groundWidth;
    [SerializeField] private int bufferLayers; //extra tiles around regular spawning the obscure cameras view of the void
    [Header("Background")]
    [SerializeField] private Tile space;
    [SerializeField] private Tile background;
    [Header("FOW")]
    [SerializeField] private Tile fowFull;
    [SerializeField] private Tile fowSemi;
    [SerializeField] private int fowUpperSpawnLayer;
    [Header("Ground")]
    [SerializeField] private Tile regolith;
    [SerializeField] private Tile bedrock;
    [Header("Ice")]
    [SerializeField] private Tile iceOre;
    [SerializeField] private int iceUpperSpawnLayer;
    [SerializeField] private int iceLowerSpawnLayer;
    [SerializeField] private int iceOrePercent;
    [Header("Iron")]
    [SerializeField] private Tile ironOre;
    [SerializeField] private int ironUpperSpawnLayer;
    [SerializeField] private int ironLowerSpawnLayer;
    [SerializeField] private int ironOrePercent;
    [Header("Copper")]
    [SerializeField] private Tile copperOre;
    [SerializeField] private int copperUpperSpawnLayer;
    [SerializeField] private int copperLowerSpawnLayer;
    [SerializeField] private int copperOrePercent;
    [Header("Gold")]
    [SerializeField] private Tile goldOre;
    [SerializeField] private int goldUpperSpawnLayer;
    [SerializeField] private int goldLowerSpawnLayer;
    [SerializeField] private int goldOrePercent;
    [Header("Titanium")]
    [SerializeField] private Tile titaniumOre;
    [SerializeField] private int titaniumUpperSpawnLayer;
    [SerializeField] private int titaniumLowerSpawnLayer;
    [SerializeField] private int titaniumOrePercent;

    //tilemaps
    private Tilemap backgroundTilemap;
    private Tilemap groundTilemap;
    private Tilemap oreTilemap;
    private Tilemap fowTilemap;

    //boundaries
    private GameObject topBoundary;
    private GameObject bottomBoundary;
    private GameObject leftBoundary;
    private GameObject rightBoundary;


    
    void Start()
    {// Start is called before the first frame update

        //get components
        backgroundTilemap = GameObject.Find("Level/Grid/Background Tiles").GetComponent<Tilemap>();
        groundTilemap = GameObject.Find("Level/Grid/Ground Tiles").GetComponent<Tilemap>();
        oreTilemap = GameObject.Find("Level/Grid/Ore Tiles").GetComponent<Tilemap>();
        fowTilemap = GameObject.Find("Level/Grid/FOW Tiles").GetComponent<Tilemap>();
        topBoundary = GameObject.Find("Level/Boundary/Top");
        bottomBoundary = GameObject.Find("Level/Boundary/Bottom");
        leftBoundary = GameObject.Find("Level/Boundary/Left");
        rightBoundary = GameObject.Find("Level/Boundary/Right");

        //generate level
        Generate();
    }

    /*
    void Update()
    {// Update is called once per frame

    }
    */

    private void Generate()
    {//generate level

        Vector3Int tile = new Vector3Int(0, 0, 0);


        //space generation
        for (int y = ((spaceHeight - 1) + bufferLayers); y >= 0; y--)
        {//y top to bottom

            for (int x = ((-groundWidth / 2) - bufferLayers); x < ((groundWidth / 2) + bufferLayers); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                backgroundTilemap.SetTile(tile, space);
            }
        }


        //ground generation
        for (int y = -1; y >= (-groundHeight); y--)
        {//y top to bottom

            for (int x = ((-groundWidth / 2) - bufferLayers); x < ((groundWidth / 2) + bufferLayers); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                backgroundTilemap.SetTile(tile, background);
                groundTilemap.SetTile(tile, regolith);
            }
        }


        //bedrock generation
        for (int y = (-groundHeight - 1); y >= (-groundHeight - bufferLayers); y--)
        {//y top to bottom

            for (int x = ((-groundWidth / 2) - bufferLayers); x < ((groundWidth / 2) + bufferLayers); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                groundTilemap.SetTile(tile, bedrock);
            }
        }


        //Ore Generation
        //ice ore
        GenerateOre(iceUpperSpawnLayer, iceLowerSpawnLayer, iceOrePercent, iceOre);

        //iron ore
        GenerateOre(ironUpperSpawnLayer, ironLowerSpawnLayer, ironOrePercent, ironOre);

        //copper ore
        GenerateOre(copperUpperSpawnLayer, copperLowerSpawnLayer, copperOrePercent, copperOre);

        //gold ore
        GenerateOre(goldUpperSpawnLayer, goldLowerSpawnLayer, goldOrePercent, goldOre);

        //titanium ore
        GenerateOre(titaniumUpperSpawnLayer, titaniumLowerSpawnLayer, titaniumOrePercent, titaniumOre);


        //FOW Semi generation (top layer only)
        for (int x = ((-groundWidth / 2) - bufferLayers); x < ((groundWidth / 2) + bufferLayers); x++)
        {//x left to right

            //set tile
            tile = new Vector3Int(x, -fowUpperSpawnLayer, 0);
            fowTilemap.SetTile(tile, fowSemi);
        }

        //FOW Full generation
        for (int y = (-fowUpperSpawnLayer - 1); y >= (-groundHeight - bufferLayers); y--)
        {//y top to bottom

            for (int x = ((-groundWidth / 2) - bufferLayers); x < ((groundWidth / 2) + bufferLayers); x++)
            {//x left to right

                //set tile
                tile = new Vector3Int(x, y, 0);
                fowTilemap.SetTile(tile, fowFull);
            }
        }


        //boundary generation
        topBoundary.transform.position = new Vector3(0, spaceHeight + 0.05f, 0);
        topBoundary.GetComponent<BoxCollider2D>().size = new Vector2(groundWidth, 0.1f);

        bottomBoundary.transform.position = new Vector3(0, (-groundHeight - 0.05f), 0);
        bottomBoundary.GetComponent<BoxCollider2D>().size = new Vector2(groundWidth, 0.1f);

        //find center of the maps height for left/right boundaries
        int mapHeight = spaceHeight + groundHeight;
        int heightCenter = 0;

        if (groundHeight == spaceHeight)
        {
            heightCenter = 0;
        }
        else if (groundHeight > spaceHeight)
        {
            heightCenter = -((mapHeight / 2) - spaceHeight);
        }
        else if (spaceHeight > groundHeight)
        {
            heightCenter = ((mapHeight / 2) - groundHeight);
        }

        leftBoundary.transform.position = new Vector3(((-groundWidth / 2) - 0.05f), heightCenter, 0);
        leftBoundary.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, mapHeight);

        rightBoundary.transform.position = new Vector3(((groundWidth / 2) + 0.05f), heightCenter, 0);
        rightBoundary.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, mapHeight);

    }

    private void GenerateOre(int upperSpawnLayer, int lowerSpawnLayer, int orePercent, Tile oreTile)
    {
        Vector3Int tile = new Vector3Int(0, 0, 0);
        int random = 0;

        for (int y = -upperSpawnLayer; y >= (-lowerSpawnLayer); y--)
        {//y top to bottom

            for (int x = (-groundWidth / 2); x < (groundWidth / 2); x++)
            {//x left to right

                //random # 1-100 (Unity is dumb and lower is inclusive and max is exclusive because reasons)
                random = Random.Range(1, 101);

                if (random <= orePercent)
                {//generate ore if random number is in range of percent chance of spawning

                    //set tile
                    tile = new Vector3Int(x, y, 0);
                    oreTilemap.SetTile(tile, oreTile);
                }
            }
        }
    }

    public int GetGroundWidth()
    {
        return groundWidth;
    }
}