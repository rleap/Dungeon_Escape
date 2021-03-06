﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    Rigidbody2D rigid;
    SpriteRenderer playerSprite;
    SpriteRenderer swordArcSprite;

    bool isGrounded = false;
    [Header("Collections")]
    [SerializeField] public int diamonds;

    [Header("Settings")]
    [SerializeField] public int playerHealth;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float jumpforce = 5.0f;
    [SerializeField] LayerMask groundLayer;
   
    PlayerAnimation playerAnimation;

    public int Health { get; set; }

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        Health = playerHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        Attack();
	}

    void Move()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");

        isGrounded = IsGrounded();


        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        // Jump
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && isGrounded)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpforce);
            playerAnimation.Jump(true); // Trigger jump animation
        }
        else if (isGrounded)
        {
            playerAnimation.Jump(false);
        }

        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);
        playerAnimation.Move(move);  // Transition to run animation
    }

    void Attack()
    {
        isGrounded = IsGrounded();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded)
        {
            playerAnimation.Attack();
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.75f, groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        return hit.collider != null ? true : false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            playerSprite.flipX = false;
            swordArcSprite.flipY = false;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipY = true;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }

    }

    public void Damage()
    {
        //Debug.Log("Player::Damage()");

        //Debug.Log("Player Health: " + Health);

        if (Health < 1)
        {
            return;
        }

        //playerAnimation.Hit();

        Health--;

        UIManager.Instance.UpdateLives(Health);

        if (Health < 1)
        {
            //Debug.Log("Player Dead");
            playerAnimation.Death();

            StartCoroutine(AfterDeath());
        }
    }

    private IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(3.0f);

        // Return to main menu
        SceneManager.LoadScene("Main_Menu");
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
