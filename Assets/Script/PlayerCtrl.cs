using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody2D rigi2D;

    [Header("Status")]
    [SerializeField] float axisH;
    [SerializeField] float speed;
    [SerializeField] float jump;

    [SerializeField] bool goJump;
    [SerializeField] bool onGround;

    public string idleAimation = "Idle";
    public string moveAimation = "PayerMove";
    public string jumpAimation = "PlayerJump";
    public string claerAimation = "Clear";
    public string overAimation = "Over";

    [SerializeField] string curAnime = "";
    [SerializeField] string oldAnime = "";



    Animator animator;


    public static string gameState = "playing";

    public int score = 0;

    public LayerMask groundLayer; // 착지할 수 있는 레이어


    private void Start()
    {
        axisH = 0.0f;
        speed = 4.0f;
        jump = 9.0f;

        goJump = false;
        onGround = false;


        rigi2D = this.GetComponent<Rigidbody2D>();
        rigi2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        animator = GetComponent<Animator>();
        curAnime = "Idle";
        oldAnime = "Idle";

        gameState = "playing";
    }

    private void Update()
    {
        if(gameState != "playing")
        {
            return;
        }
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {

            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {

            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();

        }


    }

    private void Jump()
    {
        goJump = true;
        onGround = false;
    }

    private void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if (onGround || axisH != 0)
        {
            rigi2D.velocity = new Vector2(axisH * speed, rigi2D.velocity.y);

        }

        if (onGround && goJump)
        {
            Vector2 jumpPw = new Vector2(0, jump);
            rigi2D.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;

        }

        if (onGround)
        {
            if (axisH == 0)
            {
                curAnime = idleAimation;
            }
            else
            {
                curAnime = moveAimation;
            }
        }
        else
        {
            curAnime = jumpAimation;
        }

        if (curAnime != oldAnime)
        {
            oldAnime = curAnime;
            animator.Play(curAnime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            GameOver();

        }
        if (collision.gameObject.tag == "Goal")
        {
            Goal();

        }
        if(collision.gameObject.tag == "Score")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score = item.Value;

            Destroy(collision.gameObject);

        }
    }

    private void GameOver()
    {
        animator.Play(overAimation);
        gameState = "gameOver";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false;
        rigi2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }
    private void Goal()
    {
        animator.Play(claerAimation);
        gameState = "gameClear";
        GameStop();
    }

    public void GameStop()
    {
        rigi2D.velocity = new Vector2(0, 0);
    }
}
