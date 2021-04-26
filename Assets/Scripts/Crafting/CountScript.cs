using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountScript : MonoBehaviour
{
    public TMP_InputField inputField;
    public void AddValue() {
        int value = GetValue();
        inputField.text = (value+1).ToString();
    }

    int GetValue() {
        if (inputField.text != "") {
            return int.Parse(inputField.text);
        } else {
            return 1;
        }
    }

    public void SubtractValue() {
        int value = GetValue();
        int newValue = value-1;
        if (newValue < 1) {
            newValue = 1;
        }
        inputField.text = newValue.ToString();
    }
}
