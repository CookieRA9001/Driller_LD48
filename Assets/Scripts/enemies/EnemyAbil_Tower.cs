using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbil_Tower : MonoBehaviour{
    private EnemyStats Me;
    private EnemySpriteGroup MyType;
    private GameObject player;
    public GameObject FireBall;
    private float timer = 3f;
    void Start(){
        Me = GetComponent<EnemyStats>();
        MyType = GetComponent<EnemySpriteGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){
        if (Me.gameObject == null || Me == null || player == null) return;
        if (timer>0) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                GameObject GO = Instantiate(FireBall, transform.position, Quaternion.identity);
                EnemyAbil_FireBall fb = GO.GetComponent<EnemyAbil_FireBall>();
                fb.GoRight = Me.facingRight;
                timer = 3f;
            }
        }
        Vector2 tp = transform.position;
        if (Me.facingRight && player.transform.position.x < tp.x && player.transform.position.y >= tp.y-0.5f && player.transform.position.y <= tp.y+0.5f){
            Me.Flip();
        }
        else if (!Me.facingRight && player.transform.position.x > tp.x && player.transform.position.y >= tp.y - 0.5f && player.transform.position.y <= tp.y + 0.5f){
            Me.Flip();
        }
    }
}
