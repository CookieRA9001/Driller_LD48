using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Type;
    // 1 - frendly
    // 2 - moves, but can die
    // 3 - moves, but can't die
    // 4 - stike
    public bool facingRight = false;
    public bool canDie = true;
    public bool Grounded = false;
    float framTime = 1f;
    int fram = 1;
    public float runspeed = 10f;
    private Sprite Sprite1, Sprite2, SpriteD;
    private Rigidbody2D MeMyselfAndI;
    private SpriteRenderer MyFace;
    private BoxCollider2D MyBox;
    private EnemySpriteGroup MyType;
    public float DamageTimer = 0;
    private LayerMask Ground;
    public LayerMask WallnGround;
    public bool ignoreGround = false;
    private DamageTrigger DT;
    public int Damage = 1;
    private GameObject Feet;
    private GameObject Nose;

    void Start(){
        MeMyselfAndI = GetComponent<Rigidbody2D>();
        MyFace = GetComponent<SpriteRenderer>();
        MyBox = GetComponent<BoxCollider2D>();
        MyType = GetComponent<EnemySpriteGroup>();
        Ground = LayerMask.NameToLayer("Ground"); //!
        Feet = new GameObject("Feet");
        Nose = new GameObject("Nose");
        DT = gameObject.AddComponent<DamageTrigger>();
        DT.Damage = Damage;
        Feet.transform.position = new Vector3(transform.position.x, transform.position.y - 0.55f, transform.position.z);
        Nose.transform.position = new Vector3(transform.position.x - 0.6f, transform.position.y, transform.position.z);
        Feet.transform.parent = gameObject.transform;
        Nose.transform.parent = gameObject.transform;

        runspeed = MyType.speed;
        canDie = MyType.canDie;
        Sprite1 = MyType.fram1;
        if (MyType.isFram2) Sprite2 = MyType.fram2;
        else Sprite2 = MyType.fram1;
        SpriteD = MyType.dameged;

        MyFace.sprite = Sprite1;
    }
    void Update(){
        if (DamageTimer > 0){
            MyFace.sprite = SpriteD;
            DamageTimer -= Time.deltaTime;
            if (DamageTimer <= 0.9f){
                MeMyselfAndI.velocity = new Vector3(0, 0, 0);
            }
            if (DamageTimer <= 0){
                Destroy(gameObject);
                
            }
        }
        else {
            //animation
            if (framTime > 0){
                framTime -= Time.deltaTime;
            }
            else {
                framTime = 1f;
                if (fram == 1) { MyFace.sprite = Sprite1; fram = 2; }
                else { MyFace.sprite = Sprite2; fram = 1; }
            }

            if (facingRight){
                MeMyselfAndI.velocity = new Vector3(runspeed, MeMyselfAndI.velocity.y);
            }
            else{
                MeMyselfAndI.velocity = new Vector3(-runspeed, MeMyselfAndI.velocity.y);
            }
        }
    }
    private void FixedUpdate()
    {
        if (Grounded && !ignoreGround) {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(Feet.transform.position, new Vector2(0.1f, 0.05f), Ground);
            //Debug.Log(Ground);
            Grounded = false;
            for (int i = 0; i < colliders.Length; i++){
                if (colliders[i].gameObject != gameObject && colliders[i].tag == "Ground"){
                    Grounded = true;
                }
            }
            if (!Grounded){
                Flip();
            }
        }
        else if (!ignoreGround) {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(Feet.transform.position, new Vector2(0.1f, 0.05f), Ground);
            //Debug.Log(Ground);
            Grounded = false;
            for (int i = 0; i < colliders.Length; i++){
                if (colliders[i].gameObject != gameObject && colliders[i].tag == "Ground") {
                    Grounded = true;
                }
            }
        }
        Collider2D[] collidersWall = Physics2D.OverlapBoxAll(Nose.transform.position, new Vector2(0.1f, 0.1f), WallnGround);
        for (int i = 0; i < collidersWall.Length; i++) {
            if (collidersWall[i].tag == "Wall" || collidersWall[i].tag == "Ground") {
                if (Type == 8) Destroy(gameObject);
                else if (Type == 6) if(GetComponent<EnemyAbil_Hunter>()!=null) GetComponent<EnemyAbil_Hunter>().TurnOff();
                else if (Type == 9) if(GetComponent<EnemyAbil_Demon>() != null) GetComponent<EnemyAbil_Demon>().TurnOff();
                Flip();
                break;
            }
        }
        
    }
    public void Flip(){
        facingRight = !facingRight;
        Vector3 i = transform.localScale;
        i.x *= -1;
        transform.localScale = i;
    }
    public void KillSelf(bool playerFacingRight){
        if (DamageTimer == 0){
            FindObjectOfType<AudioManeger>().Play("EnemyDie");
            Vector3 targetVelocity;
            if (!playerFacingRight){
                targetVelocity = new Vector2(-20, 0);
            }
            else{
                targetVelocity = new Vector2(20, 0);
            }

            MeMyselfAndI.velocity = targetVelocity;
            DamageTimer = 1f;
        }
    }
}
