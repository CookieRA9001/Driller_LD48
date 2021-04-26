using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSave : MonoBehaviour
{
    public void Clear() {
        PlayerPrefs.DeleteAll();
    }
}
