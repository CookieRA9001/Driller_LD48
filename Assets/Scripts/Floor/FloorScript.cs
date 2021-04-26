using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour{
    public List<GameObject> enemySpawns;
    public List<GameObject> TileSpawnPoints;
    public List<PortalLogic> Portals;
    public DarknessScript Darkness;
    public int floor;
    public GameObject parent;
    public int type;

    public void AssignPortals() {
        
        if (Portals.Count > 0) {
            for (int i = 0; i < Portals.Count; i++) {
                Portals[i].key = floor;
            }
        }
    }
}
