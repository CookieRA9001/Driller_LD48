using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLogic : MonoBehaviour
{
    public GameObject Upgrade;
    // Start is called before the first frame update
    public int[] ShopRarityArray = { 
    };
    public Sprite[] ShopItemSprites = {
    };
    public Sprite[] RecoseSprites = { };
    public bool Fliped = false;
    void Start(){
        int floor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().floor;
        if (Fliped) Flip();
        Upgrade = new GameObject();
        SpriteRenderer sr = Upgrade.AddComponent<SpriteRenderer>();
        int rand = Mathf.RoundToInt(Random.value*Mathf.Min(800+floor,1000));
        int index=0;
        for(int i = 0; rand > ShopRarityArray[i]; i++){
            index = i;
        }
        sr.sprite = ShopItemSprites[index];
        BoxCollider2D bc2d = Upgrade.AddComponent<BoxCollider2D>();
        BoxCollider2D bc2d_trig = Upgrade.AddComponent<BoxCollider2D>();
        Rigidbody2D rb2d = Upgrade.AddComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        UpgadeLogic ul = Upgrade.AddComponent<UpgadeLogic>();
        bc2d_trig.isTrigger = true;
        ul.key = index;
        ul.price = 1;
        int r = Mathf.Max(Mathf.FloorToInt(Random.value*Mathf.Min(floor*5, 1000) /250) - Mathf.FloorToInt(Random.value*1.9f -1),0);
        switch (r) {
            case 0: { ul.price = (index+1)*3 + Mathf.FloorToInt(floor/10) ; break; }
            case 1: { ul.price = (index+1)*3 + Mathf.FloorToInt(floor / 20); break; }
            case 2: { ul.price = (index+1)*2 + Mathf.FloorToInt(floor / 60); break; }
            case 3: { ul.price = (index+1) + Mathf.FloorToInt(floor / 150); break; }
            case 4: { ul.price = (index+1) + Mathf.FloorToInt(floor / 300); break; }
            default: { r = 4; ul.price = 999; break; }
        }
        ul.recorce = r;
        ul.s = RecoseSprites[r];
        Upgrade.transform.position = new Vector3(transform.position.x + (-2 * transform.localScale.x), transform.position.y-0.5f, transform.position.z);
        Upgrade.transform.parent = transform;
        ul.Setup();
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void Flip()
    {
        Vector3 i = transform.localScale;
        i.x *= -1;
        transform.localScale = i;
    }
}
