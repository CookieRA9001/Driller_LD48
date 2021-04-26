using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Item{
    public GameObject CheckPointObjectRaf;
    public override void Craft(int count) {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        Vector3 tp = pStats.transform.position;
        GameObject CPoint = Instantiate(CheckPointObjectRaf,tp, Quaternion.identity);
        CheckPoint cp = CPoint.GetComponent<CheckPoint>();
        int type = pStats.FloorIn.GetComponent<FloorParentScript>().floorScript.type;
        if(pStats.chechPoint != null) Destroy(pStats.chechPoint.gameObject);
        cp.CreateCheckPoint(pStats.gameObject, type);
        pStats.chechPoint = cp;
    }
}
