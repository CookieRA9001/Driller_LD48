using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void ExitGame() {
        SavingScript g = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SavingScript>();
        g.Save();
        g.WriteSave();
        Application.Quit();
    }
}
