using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class FlyPlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public Rigidbody2D rb;
        public int health = 100;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 moveDirection = new Vector2(horizontal*moveSpeed,vertical*moveSpeed);
            rb.velocity = moveDirection;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                Destroy(this.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("enemy"))
                TakeDamage(50);
        }
    }

}

