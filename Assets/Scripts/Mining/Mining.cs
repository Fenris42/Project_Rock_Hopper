using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    //serialized fields
    [SerializeField] private float digTime;
    [SerializeField] Tile crack_1;
    [SerializeField] Tile crack_2;
    [SerializeField] Tile crack_3;
    [SerializeField] Tile crack_4;
    [SerializeField] Tile crack_5;
    [SerializeField] Tile crack_6;
    [SerializeField] Tile crack_7;
    [SerializeField] Tile crack_8;
    [SerializeField] Tile crack_9;
    [SerializeField] Tile crack_10;

    //private variables
    private Tilemap tilemap;
    private Tilemap crackTilemap;
    private float timer;
    private List<Vector3Int> updateQueue = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Destructible Tiles").GetComponent<Tilemap>();
        crackTilemap = GameObject.Find("Crack Tiles").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        //convert frame rate to seconds
        timer += Time.deltaTime;
        if (timer >= (digTime / 10))
        {
            UpdateTiles();
            timer = 0;
        }
    }

    public void Mine(RaycastHit2D ray)
    {
        //get coords of ray contact and translate into tile coordinates
        Vector3 hitPos = Vector3.zero;
        hitPos.x = ray.point.x - 0.01f * ray.normal.x;
        hitPos.y = ray.point.y - 0.01f * ray.normal.y;
        Vector3Int tilePos = tilemap.WorldToCell(hitPos);

        //search update array to check if tile already set for update
        if (updateQueue.Contains(tilePos) == false)
        {
            updateQueue.Add(tilePos);
        }
    }

    private void UpdateTiles()
    {
        if (updateQueue.Count > 0)
        {
            foreach (Vector3Int tile in updateQueue)
            {
                var crackInfo = crackTilemap.GetTile(tile);
                var tileInfo = tilemap.GetTile(tile);

                if (tileInfo == null)
                {
                    crackTilemap.SetTile(tile, null);
                }
                else if (crackInfo == null)
                {
                    crackTilemap.SetTile(tile, crack_1);
                }
                else if (crackInfo.name == "Crack_1")
                {
                    crackTilemap.SetTile(tile, crack_2);
                }
                else if (crackInfo.name == "Crack_2")
                {
                    crackTilemap.SetTile(tile, crack_3);
                }
                else if (crackInfo.name == "Crack_3")
                {
                    crackTilemap.SetTile(tile, crack_4);
                }
                else if (crackInfo.name == "Crack_4")
                {
                    crackTilemap.SetTile(tile, crack_5);
                }
                else if (crackInfo.name == "Crack_5")
                {
                    crackTilemap.SetTile(tile, crack_6);
                }
                else if (crackInfo.name == "Crack_6")
                {
                    crackTilemap.SetTile(tile, crack_7);
                }
                else if (crackInfo.name == "Crack_7")
                {
                    crackTilemap.SetTile(tile, crack_8);
                }
                else if (crackInfo.name == "Crack_8")
                {
                    crackTilemap.SetTile(tile, crack_9);
                }
                else if (crackInfo.name == "Crack_9")
                {
                    crackTilemap.SetTile(tile, crack_10);
                }
                else if (crackInfo.name == "Crack_10")
                {
                    DropItem(tile);
                }
            }
        }

        //reset update queue
        updateQueue.Clear();
    }

    private void DropItem(Vector3Int tile)
    {
        //get tile info
        var tileinfo = tilemap.GetTile(tile);

        //set instatiation position to be in center of tile
        Vector3 spawnPos = tile;
        spawnPos.x += 0.5f;
        spawnPos.y += 0.5f;

        switch (tileinfo.name)
        {
            case "Regolith":
                Instantiate(Resources.Load("Rock"), spawnPos, Quaternion.identity);
                break;
            case "Ice_Ore":
                Instantiate(Resources.Load("Ice"), spawnPos, Quaternion.identity);
                break;
        }

        //set tile to empty
        crackTilemap.SetTile(tile, null);
        tilemap.SetTile(tile, null);
    }
}
