using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbil_FireBall : MonoBehaviour{
    private float lifeTime = 3;
    public bool GoRight;
    private Rigidbody2D rb2;
    private EnemyStats es;
    void Start(){
        es = GetComponent<EnemyStats>();
        rb2 = GetComponent<Rigidbody2D>();
    }
    void Update(){
        if (lifeTime>0) {
            lifeTime -= Time.deltaTime;
            if (lifeTime <=0) {
                Destroy(gameObject);
            }
        }
        if (GoRight){
            es.facingRight = true;
        }
        else{
            es.facingRight = false;
        }
    }
}
