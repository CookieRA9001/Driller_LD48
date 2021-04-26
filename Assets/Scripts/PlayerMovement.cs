using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;
using System;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float mSmoothing = .1f;
    public bool airControl;
    public float runspeed = 20f;
    public PlayerStats playerStats;
    float horizontalMove = 0f;

    public Animator SpriteAnimator;
    public GameObject Sprite;

    public Rigidbody2D PlayerRigidBody;

    public LayerMask Ground;

    public Transform groundCheck;

    const float groundedRadius = .2f; 
	public bool Grounded;            
    private Rigidbody2D Player;
	public bool facingRight = true;
    private Vector3 Velocity = Vector3.zero;

    public float currentKnockback;
    private float knockBackTimer;
    public UnityEvent OnLandEvent;
    public bool frozen;
    private TileScript tileUnder;
    public bool isMining;
    //Vector2 movement;

    private void Awake()
	{
        Player = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null) OnLandEvent = new UnityEvent();
	}

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;

        knockBackTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Down") && !isMining && tileUnder != null) {
            if (playerStats.CanMine(tileUnder.Type)) {
                SpriteAnimator.Play("Mining");
            }
        }
    }

    private void FixedUpdate()
	{
		bool wasGrounded = Grounded;

		Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(0.2f, 0.05f), Ground);
        if (colliders.Length <= 0) Grounded = false;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                TileScript tScript = colliders[i].gameObject.GetComponent<TileScript>();
                if (tScript) tileUnder = tScript;
				Grounded = true;
				if (!wasGrounded)
                    OnLandEvent.Invoke();
			}
		}

        if (!frozen) Move(horizontalMove * Time.fixedDeltaTime, false, false);
        ///SpriteAnimator.SetFloat("movementX", PlayerRigidBody.velocity.x);
        ///SpriteAnimator.SetFloat("movementY", PlayerRigidBody.velocity.y);
        ///SpriteAnimator.SetBool("lookingRight", facingRight);
        ///SpriteAnimator.SetBool("Grounded", Grounded);


        if (facingRight == false)
            playerStats.facingDirection = -1;
        else
            playerStats.facingDirection = 1;
    }


	public void Move(float move, bool crouch, bool jump){

		if (Grounded || airControl && !frozen){
            float direction = move;

            
            if (move == 0){
                if (!facingRight)
                    direction = -1;
                else
                    direction = 1;
            }

            if (knockBackTimer > 0){
                Vector3 targetVelocity = new Vector2((move * 10f) + currentKnockback, Player.velocity.y);
                Player.velocity = targetVelocity;
            } else{
                Vector3 targetVelocity = new Vector2(move * 10f, Player.velocity.y);

                Player.velocity = Vector3.SmoothDamp(Player.velocity, targetVelocity, ref Velocity, mSmoothing);
            }

			if (move > 0 && !facingRight)
                Flip();
			else if (move < 0 && facingRight)
				Flip();
		}

	}

	private void Flip(){
        if (!frozen) {
            facingRight = !facingRight;

		    Vector3 i = Sprite.transform.localScale;
		    i.x *= -1;
		    Sprite.transform.localScale = i;
        }
    }

    public void KnockBackDown(float knockback){
        Player.velocity = Vector2.up * knockback;
    }

    public void KnockBackRight(float knockback)
    {
        currentKnockback = -knockback;
        knockBackTimer = 0.1f;
    }

    public void KnockBackLeft(float knockback)
    {
        currentKnockback = knockback;
        knockBackTimer = 0.1f;
    }

    public void StartMining() {
        isMining = true;
        frozen = true;
        Player.velocity = new Vector3(0, 0, 0);
        Player.position = new Vector3(Mathf.Ceil(Player.position.x) - 0.5f, Player.position.y);
        FindObjectOfType<AudioManeger>().Play("Digging");
    }

    public void StopMining() {
        isMining = false;
        frozen = false;
    }

    public void EndMining() {
        playerStats.CollectOre(tileUnder.Type, tileUnder);
        FindObjectOfType<AudioManeger>().Play("Dig");
        isMining = false;
    }

    public void UnfreezePlayer() {
        frozen = false;
    }
}
