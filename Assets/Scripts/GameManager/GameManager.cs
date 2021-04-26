using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] FloorPrefabs;
    public GameObject[] ShopFloorPrefabs;
    public GameObject[] EnemySpawnerPrefabs;
    public GameObject EndRoomPrefab;
    public int[] EnemySpawnChances = {30, 40, 70, 100};
    public List<GroundScript> Floors;
    public Vector3 spawnPos;
    public int floor;
    public int lastSpawnedFloor;
    public int[] oreChances = {666, 950, 995, 999, 1000};
    public int[] oreChancesMax = {75, 150, 300, 500, 1000};
    public float[] oreChancesRate = {0.75f, 0.5f, 0.15f, 0.05f, 1000};
    public float oreChancesMultiplier = 1f;
    private bool first = true;
    public float DarknessMin = 0.2f;
    public float DarknessDiv = 500f;
    public int shopFloor = 10;
    private PlayerStats pStats;
    public bool killAll = false;

    private void Start()
    {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (PlayerPrefs.GetInt("Saved") == 1 && gameObject.GetComponent<SavingScript>().SavingEnabled) {
            gameObject.GetComponent<SavingScript>().Load();
        } else {
            floor = pStats.floor;
            SpawnFloor(new Vector3(spawnPos.x, spawnPos.y, spawnPos.z));
            SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-6, spawnPos.z));
            SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-12, spawnPos.z));
            SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-18, spawnPos.z));
        }
    }
    public void AddFloor(GroundScript Floor) {
        Floors.Add(Floor);
        if (Floors.Count > 10 ) {
            Floors[0].DeleteFloor();
            Floors.RemoveAt(0);
        }
    }

    public void SpawnFloor(Vector3 pos) {
        SpawnFloor(pos, -1);
    }

    public void DeleteFloors() {
        for (int i = Floors.Count-1; i >= 0; i--) {
            Floors[i].DeleteFloor();
            Floors.RemoveAt(i);
        }
        Floors.Clear();
    }

    public void SpawnCustomFromNew(int type, int customFloor) {
        DeleteFloors();
        floor = customFloor;
        first = true;
        SpawnFloor(new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), type);
        SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-6, spawnPos.z));
        SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-12, spawnPos.z));
        SpawnFloor(new Vector3(spawnPos.x, spawnPos.y-18, spawnPos.z));
        pStats.gameObject.GetComponent<PlayerMovement>().currentKnockback = 0;
        pStats.transform.position = spawnPos;
    }

    public void SpawnFloor(Vector3 pos, int type) {
        if (Floors.Count <= 0) {
            lastSpawnedFloor = floor;
        } else lastSpawnedFloor = Floors[Floors.Count-1].floor + 1;

        GameObject Floor;
        int rand = 0;
        if (lastSpawnedFloor == 0) {
            Floor = Instantiate(FloorPrefabs[0], pos, Quaternion.identity);
        } else if (lastSpawnedFloor == 1000 && type == -1) {
            Floor = Instantiate(EndRoomPrefab, pos, Quaternion.identity);
        } else if (lastSpawnedFloor % shopFloor == 0 && type == -1) {
            rand = Random.Range(0, ShopFloorPrefabs.Length);
            Floor = Instantiate(ShopFloorPrefabs[rand], pos, Quaternion.identity);
        } else if (type == -1) {
            rand = Random.Range(0, FloorPrefabs.Length);
            Floor = Instantiate(FloorPrefabs[rand], pos, Quaternion.identity);
        } else {
            rand = type;
            Floor = Instantiate(FloorPrefabs[rand], pos, Quaternion.identity);
        }
        FloorScript fScript = Floor.GetComponentInChildren<FloorScript>();
        GroundScript gScript = Floor.GetComponentInChildren<GroundScript>();
        gScript.floor = lastSpawnedFloor;
        fScript.floor = lastSpawnedFloor;
        fScript.type = rand;
        fScript.AssignPortals();
        AddFloor(gScript);
        List<GameObject> enemySpawns = fScript.enemySpawns;
        List<GameObject> enemySpawnsLeft = enemySpawns;
        int rnum = Random.Range(0, (int)Mathf.Round(enemySpawns.Count*(lastSpawnedFloor/50f) + 1));
        if (rnum > enemySpawns.Count) {
            rnum = enemySpawns.Count;
        }
        if (!first) {
            for (int i = 0; i < rnum; i++) {
                int rnum2 = Random.Range(0,enemySpawnsLeft.Count);
                int rnum3 = Random.Range(0, EnemySpawnChances[EnemySpawnChances.Length-1]);
                int enemyType = 0;

                for (int ii = EnemySpawnChances.Length - 2; ii >= 0; ii--) {
                    if (rnum3 >= EnemySpawnChances[ii] && rnum3 < EnemySpawnChances[ii+1]) {
                        enemyType = ii + 1;
                    } else if (rnum3 < EnemySpawnChances[0]) {
                        enemyType = 0;
                    }
                }

                GameObject Enemy = Instantiate(EnemySpawnerPrefabs[enemyType], enemySpawnsLeft[rnum2].transform.position, Quaternion.identity);
                gScript.Enemies.Add(Enemy);
                if (killAll) Enemy.GetComponent<EnemySpriteGroup>().canDie = true;
                Enemy.GetComponent<EnemyStats>().Damage = 1 + (int)Mathf.Floor(lastSpawnedFloor/200f);
                enemySpawnsLeft.RemoveAt(rnum2);
            }
        } else {
            first = false;
        }
        
        fScript.Darkness.sRenderer.color = new Color(0, 0, 0, Mathf.Min(lastSpawnedFloor/DarknessDiv + DarknessMin, 1));

    }
}
