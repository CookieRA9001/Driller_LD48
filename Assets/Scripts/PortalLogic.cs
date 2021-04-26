using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour{
    private GameObject player;
    private PlayerMovement controller;
    private PortalLogic myFriend = null;
    private SpriteRenderer myFace;
    public int key;
    public bool active=false;
    public float timeOut = 0f;
    public Sprite sp1, sp2;
    float framTime = 0.5f;
    int fram = 0;
    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        myFace = GetComponent<SpriteRenderer>();
        PortalLogic[] allPortal = FindObjectsOfType<PortalLogic>();
        foreach(PortalLogic portal in allPortal) {
            if (portal.key == key && portal!=this){
                myFriend = portal;
            }
        }
        if(myFriend!=null) active = true;
    }
    void Update(){
        framTime -= Time.deltaTime;
        if(framTime <= 0){
            fram = 1 - fram;
            framTime = 0.5f;
            if(fram==1) myFace.sprite = sp2;
            else myFace.sprite = sp1;
        }
        if (timeOut > 0){
            timeOut -= Time.deltaTime;
            if (timeOut <= 0){
                active = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (active) {
            if (myFriend.active)
            if(collider.gameObject.CompareTag("Player"))
            {
                active = false;
                myFriend.active = false;
                timeOut = 0.3f;
                myFriend.timeOut = 0.3f;
                player.transform.position = new Vector3(myFriend.transform.position.x, myFriend.transform.position.y, myFriend.transform.position.x);
            }
        }
    }
}
