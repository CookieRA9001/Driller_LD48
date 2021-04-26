using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public TextMeshPro TMP = null;
    public TextMeshProUGUI TMP_UI = null;
    public string score;
    void Start(){
        if (TMP != null){
            TMP.text = "SCORE: " + score;
        }
        else{
            TMP_UI.text = "SCORE: " + score;
        }

    }
}
