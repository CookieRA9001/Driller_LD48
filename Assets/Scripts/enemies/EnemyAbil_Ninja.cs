using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbil_Ninja : MonoBehaviour{
    private EnemyStats Me;
    private EnemySpriteGroup MyType;
    //private DamageTrigger MyPain;
    private GameObject player;
    void Start(){
        Me = GetComponent<EnemyStats>();
        MyType = GetComponent<EnemySpriteGroup>();
        //MyPain = GetComponent<DamageTrigger>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){
        if (Me.gameObject == null || Me == null || player == null) return;
        Vector2 tp = transform.position;
        if (Me.facingRight && player.transform.position.x > tp.x && player.transform.position.y >= tp.y-0.5f && player.transform.position.y <= tp.y+0.5f){
            Me.runspeed = MyType.speed * 6;
        }
        else if (!Me.facingRight && player.transform.position.x < tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f)
        {
            Me.runspeed = MyType.speed * 6;
        }
    }
}
