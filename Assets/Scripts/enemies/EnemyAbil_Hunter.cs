using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbil_Hunter : MonoBehaviour{
    private EnemyStats Me;
    private EnemySpriteGroup MyType;
    //private DamageTrigger MyPain;
    private GameObject player;
    float time = 0.5f;
    void Start(){
        Me = GetComponent<EnemyStats>();
        MyType = GetComponent<EnemySpriteGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){
        if (Me.gameObject == null || Me == null || player == null) return;
        if (time > 0) {
            time -= Time.deltaTime;
            if (time <= 0) {
                time = 0.5f;
                Vector2 tp = transform.position;
                if (Me.facingRight && player.transform.position.x < tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f && Me.Grounded)
                    Me.Flip();
                else if (!Me.facingRight && Me.Grounded && player.transform.position.x > tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f)
                    Me.Flip();
            }
        }
    }
    public void TurnOff() {
        if (Me.gameObject == null || Me == null || player == null) return;
        Vector2 tp = transform.position;
        if (Me.facingRight && player.transform.position.x < tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f && Me.Grounded){
            time = 0;
        }
        else if (!Me.facingRight && Me.Grounded && player.transform.position.x > tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f){
            time = 0;
        }
    }
}
