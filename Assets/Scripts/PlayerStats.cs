using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class PlayerStats : MonoBehaviour {
    private float old_highscore;

    public int Health;
    public int MaxHealth;
    public int DrillPower;
    ///public Animator SpriteAnimator;
    public new GameObject camera;

    public float DefDamageTimer;
    public float DamageTimer = 0;

    public int facingDirection;
    private Rigidbody2D Player;
    private PlayerMovement controller;
    private GameManager gManager;
    public int[] ores = {0, 0, 0, 0, 0};
    public int floor = 0;
    public TextMeshProUGUI[] texts;
    public TextMeshProUGUI floorText;
    private HartCounter hCounter;
    public GameObject TMPOb;
    private TextMeshPro TMP;
    public GameObject DeathMesage;
    private SpriteRenderer spriteRef;
    public Sprite Dameged,Base;

    public GameObject DeathSprite;
    public GameObject FloorIn;

    private Dictionary<int,int> typeToOre = new Dictionary<int, int>{
        {0, -1},
        {1, -1},
        {2, -1},
        {3, 0},
        {4, 4},
        {5, 3},
        {6, 2},
        {7, 69},
        {8, 1},
    };

    private Dictionary<int,int> typeToPower = new Dictionary<int, int>{
        {0, -1},
        {1, -1},
        {2, -1},
        {3, 0},
        {4, 10},
        {5, 6},
        {6, 3},
        {7, 3},
        {8, 1},
    };
    public Dictionary<int, int> typeToAmount = new Dictionary<int, int>{
        {3, 1},
        {4, 1},
        {5, 1},
        {6, 1},
        {7, 1},
        {8, 1},
    };

    public bool isCheckPoint = false;
    public CheckPoint chechPoint = null;

    private void Awake()
    {
        ///deathanim = GameObject.FindGameObjectWithTag("DeathPanel").GetComponent<Animator>();
        Player = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerMovement>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        hCounter = GameObject.FindGameObjectWithTag("heartcounter").GetComponent<HartCounter>();
        spriteRef = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start(){
        LoadDependencies();
    }

    private void LoadDependencies() {
        TMP = TMPOb.GetComponent<TextMeshPro>();
        TMP.text = DrillPower.ToString();
        hCounter.UpdateHearts(Health, MaxHealth);
        old_highscore = SaveSystem.GetOldHighScore();
    }

    private void Update(){
        if (DamageTimer > 0){
            DamageTimer -= Time.deltaTime;
            if (DamageTimer <= 0) {
                spriteRef.sprite = Base;
                DamageTimer = 0;
            }
        }

        if (Health < 0){
            Health = 0;
        }
    }
    public void getDrillPower(int dp) {
        if (TMP != null) {
            DrillPower += dp;
            TMP.text = DrillPower.ToString();
        } else {
            LoadDependencies();
            getDrillPower(dp);
        }
    }

    public void TakeDamage(int Damage) {
        controller.frozen = false;
        Health -= Damage;
        FindObjectOfType<AudioManeger>().Play("PlayerHurt");
        camera.GetComponent<CameraShake>().Shake(0.1f, 0.11f);
        spriteRef.sprite = Dameged;
        controller.SpriteAnimator.Play("Damaged");
        if (controller.isMining) {
            controller.StopMining();
        }

        DamageTimer = DefDamageTimer;
        hCounter.UpdateHearts(Mathf.Max(Health,0), MaxHealth);
        if (Health <= 0){
            FindObjectOfType<AudioManeger>().Play("PlayerDie");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            DeathSprite = Instantiate(DeathSprite, new Vector3(transform.position.x, transform.position.y + 0.1f, 1), Quaternion.identity);
            float new_hs = Mathf.Floor((floor + ores[0] * 2f + ores[1] * 5f + ores[2] * 15f + ores[3] * 50f + ores[4] * 300f) * (float)(floor / 100f + 1f));
            if (new_hs > old_highscore) {
                SaveSystem.saveHighScore(new_hs);
            }
            GameObject dMessage = Instantiate(DeathMesage, new Vector3(camera.transform.position.x, camera.transform.position.y, 0), Quaternion.identity);
            StartButton sb = dMessage.GetComponent<StartButton>();
            sb.isCheckPoint = isCheckPoint;
            ShowScore ss = dMessage.GetComponent<ShowScore>();
            ss.score = new_hs.ToString();
            camera.GetComponent<CameraShake>().Shake(0.3f, 0.1f);
        }
    }

    public void HealthUp(int hp) {
        if (Health < MaxHealth) {
            FindObjectOfType<AudioManeger>().Play("Heal");
            Health += hp;
            if (Health > MaxHealth) Health = MaxHealth;
        }
        hCounter.UpdateHearts(Health, MaxHealth);
    }

    public void UpdateHealth() {
        hCounter.UpdateHearts(Health, MaxHealth);
    }

    public void CollectOre(int type, TileScript tileUnder) {
        if (type == 2) return;

        tileUnder.Mine();
        tileUnder = null;

        DrillPower -= typeToPower[type];
        TMP.text = DrillPower.ToString();
        if (type != 7) {
            if (typeToOre[type] != -1) {
                ores[typeToOre[type]] += typeToAmount[type];
                texts[typeToOre[type]].text = ores[typeToOre[type]].ToString();
            }
        } else {
            HealthUp(typeToAmount[type]);
        }
    }
    public void UpdateOreCounter() {
        for (int i=0; i<5; i++) {
            texts[i].text = ores[i].ToString();
        }
    }

    public bool CanMine(int type) {
        if (DrillPower < typeToPower[type] || type == 2) {
            return false;
        } else return true;
    }

    public void NewFloor() {
        floor += 1;
        UpdateFloor();
    }

    public void UpdateFloor() {
        gManager.floor = floor;
        floorText.text = "Floor: " + floor;
    }
}
