using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimtion : MonoBehaviour
{
    public Sprite[] ani;
    public float framrate;
    private float time;
    private int ani_index =0;
    private SpriteRenderer sr;
    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = ani[ani_index];
        time = framrate;
    }
    void Update(){
        if (time > 0){
            time -= Time.deltaTime;
            if (time <= 0) {
                ani_index++;
                if (ani_index < ani.Length) {
                    time = framrate;
                    sr.sprite = ani[ani_index];
                }
            }
        }
    }
}
