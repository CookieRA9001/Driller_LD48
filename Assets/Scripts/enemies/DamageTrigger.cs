using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private PlayerStats player;
    private PlayerMovement controller;

    public int Damage = 1;
    public float horizontal = 20;
    public float vertical = 1;
    public EnemyStats enemyStats;
    public bool onStay = true;
    //public LayerMask Electronics;

    private void Start(){
        enemyStats = GetComponent<EnemyStats>();
        if (GameObject.FindGameObjectWithTag("Player") == null) this.enabled=false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        DoDamage(collider);
    }

    private void OnTriggerStay2D(Collider2D collider){
        if (onStay) DoDamage(collider);
    }

    void DoDamage(Collider2D collider) {
        if (collider.CompareTag("Player") && player.DamageTimer <= 0  && enemyStats.DamageTimer==0){
            if (enemyStats.Type != 1 && 
                ((controller.facingRight == enemyStats.facingRight && (
                (enemyStats.facingRight && player.transform.position.x > transform.position.x) ||
                (!enemyStats.facingRight && player.transform.position.x < transform.position.x)
                )) 
                || !enemyStats.canDie)){
                player.TakeDamage(Damage);
                if (transform.position.x - player.transform.position.x > 0){
                    controller.KnockBackRight(horizontal);
                }
                else{
                    controller.KnockBackLeft(horizontal);
                }
                controller.KnockBackDown(vertical);
                if(enemyStats.Type == 8) Destroy(gameObject);
            }
            else if(enemyStats.canDie) {
                enemyStats.KillSelf(controller.facingRight);
            }
        }
    }
}
