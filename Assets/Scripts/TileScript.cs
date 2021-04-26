using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public int Type;

    public void Mine() {
        Destroy(gameObject);
    }
}
