using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer
{


    public class PlayerController : MonoBehaviour
    {
        [Header("UI Manager")]
        public UIManager uIManager;

        [Header("Speed")]
        public float moveSpeed;
        public float jumpSpeed;
        private float extraMoveSpeed;
        public Rigidbody2D rb;

        public float horizontal;


        [Header("Layers")]
        public LayerMask groundLayer;
        public LayerMask gameOverLayer;
        public LayerMask IncreaseSpeedLayer;
        public Transform groundCheck;


        [Header("Bool Expression")]
        private bool isGrounded;
        private bool isGameOver;
        private bool isIncreaseSpeed = false;


        [Header("Animations")]
        public Animator animator;
        private SpriteRenderer spriteRenderer;

        [Header("Coins and Keys")]
        private int coin;
        private int key = 0;

        void Start()
        {
            coin = PlayerPrefs.GetInt("coin"); //Take the saved coin value
            uIManager.CoinUI(coin); //Send the coin value to coinText;
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapPoint(groundCheck.position, groundLayer);
            isGameOver = Physics2D.OverlapPoint(groundCheck.position, gameOverLayer);

            horizontal = Input.GetAxis("Horizontal"); // Move controller (A - D - Right Arrow - Left Arrow )

            //Change player face

            if (horizontal <= 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;


            //Animations
            if (Mathf.Abs(horizontal) > 0)
                animator.SetBool("isWalking", true);
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
            }

            //Jump Controller
            if (Input.GetKey(KeyCode.Space) && isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true);
            }


            //ExtraSpeed
            if (isIncreaseSpeed == true)
                extraMoveSpeed = 2f;
            else
                extraMoveSpeed = 1f;

            //Apply Vector2
            Vector2 moveDirection = new Vector2(horizontal * moveSpeed * extraMoveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = moveDirection;

            //GameOver Controller
            if (isGameOver == true)
                DecreaseCoins();


        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PickUp"))
            {
                var item = collision.GetComponent<ItemsControl>(); // Access the ItemsControl
                if (item.Type == Items.Coin)
                {
                    coin += item.Amount;
                    uIManager.CoinUI(coin);
                    PlayerPrefs.SetInt("coin", coin); // Save coins
                }
                Destroy(item.gameObject);
            }

            if (collision.tag == "Key")
            {
                key++;
                uIManager.KeyUI(key);
                Destroy(collision.gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.collider.CompareTag("isSpeed"))
                isIncreaseSpeed = true;

            if (collision.collider.CompareTag("keyBox"))
            {
                if (key == 1)
                {
                    Destroy(collision.gameObject);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load next scene
                    uIManager.CoinUI(PlayerPrefs.GetInt("coin"));
                }
                else
                    DecreaseCoins();
            }

        }

        private void DecreaseCoins() // If isGameOver is true or key is 0, total coins decrease by the programming.
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            coin = PlayerPrefs.GetInt("coin");
            coin -= 10;
            if (coin <= 0)
                PlayerPrefs.SetInt("coin", 0);
            else
                PlayerPrefs.SetInt("coin", coin);
            uIManager.CoinUI(coin);
        }
    }

}