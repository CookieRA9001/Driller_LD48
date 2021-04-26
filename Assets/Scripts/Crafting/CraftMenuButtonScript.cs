using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenuButtonScript : MonoBehaviour
{
    public void Unfreeze() {
        Time.timeScale = 1;
    }

    public void Freeze() {
        Time.timeScale = 0;
    }
}
