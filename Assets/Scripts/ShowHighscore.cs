using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHighscore : MonoBehaviour{
    public TextMeshPro TMP = null;
    public TextMeshProUGUI TMP_UI = null;
    void Start(){
        float hs;
        hs = SaveSystem.GetOldHighScore();
        if (TMP != null){
            TMP.text = "HIGH SCORE: " + hs.ToString();
        }
        else { 
            TMP_UI.text = "HIGH SCORE: " + hs.ToString();
        }
        
    }
}
