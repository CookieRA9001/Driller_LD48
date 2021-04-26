using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour{
    public FloorScript parent;
    public float offset;
    public GameObject[] orePrefabs;
    public GameObject normalTilePrefab;
    private List<GameObject> TileSpawnPoints;
    public List<TileScript> TilesLeft;
    public int baseSpecialTileCount = 4;
    public int minSpecialTileCount = 2;
    public int[] oreChances;
    public int[] oreChancesMax;
    public float[] oreChancesRate;
    public float oreChancesMultiplier;
    public List<GameObject> Enemies;
    bool spawned = false;
    GameManager gManager;
    public int floor;

    private void Start(){
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        oreChancesMax = gManager.oreChancesMax;
        oreChancesRate = gManager.oreChancesRate;
        oreChancesMultiplier = gManager.oreChancesMultiplier;
        TileSpawnPoints = parent.TileSpawnPoints;
        CreateGround();
    }

    private void CreateGround() {
        for (int i = 0; i < TileSpawnPoints.Count; i++) {
            GameObject Tile = Instantiate(normalTilePrefab, TileSpawnPoints[i].transform.position, Quaternion.identity);
            Tile.transform.parent = parent.transform;
            TileScript tScript = Tile.GetComponent<TileScript>();
            TilesLeft.Add(tScript);
        }

        int rnum1 = (int)Random.Range(minSpecialTileCount, baseSpecialTileCount + (Mathf.Round(floor / Mathf.Max(26-oreChancesMultiplier,1))));
        if (rnum1 > TileSpawnPoints.Count-1) {    
            rnum1 = TileSpawnPoints.Count-1;
        }

        for (int i = 0; i < gManager.oreChances.Length - 1; i++) {
            oreChances[i] = gManager.oreChances[i] - ((int)Mathf.Round(floor / (oreChancesMultiplier/oreChancesRate[i])))*3;
            if (oreChances[i] < oreChancesMax[i]) {
                oreChances[i] = oreChancesMax[i];
            }
        }

        for (int i = 0; i < rnum1; i++) {
            int rnum = Random.Range(0, TilesLeft.Count);
            Vector3 pos = TilesLeft[rnum].transform.position;
            Destroy(TilesLeft[rnum].gameObject);
            TilesLeft.RemoveAt(rnum);

            int rnumOre = Random.Range(0, oreChances[oreChances.Length-1]);
            int oreType = 0;

            for (int ii = oreChances.Length - 2; ii >= 0; ii--) {
                if (rnumOre > oreChances[ii] && rnumOre < oreChances[ii+1]) {
                    oreType = ii + 1;
                } else if (rnumOre < oreChances[0]) {
                    oreType = 0;
                }
            }

            GameObject Tile = Instantiate(orePrefabs[oreType], pos, Quaternion.identity);
            Tile.transform.parent = parent.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!spawned && other.CompareTag("Player")) {
            spawned = true;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y-offset, transform.position.z);
            gManager.SpawnFloor(pos);
        }
    }

    public void DeleteFloor() {
        for (int i = Enemies.Count - 1; i >= 0; i--) {
            Destroy(Enemies[i]);
            Enemies.RemoveAt(i);
        }
        Destroy(parent.parent.gameObject);
    }
}
