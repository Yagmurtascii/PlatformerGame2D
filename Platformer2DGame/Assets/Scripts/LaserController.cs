using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{

    public class LaserController : MonoBehaviour
    {
        public float speed;
        public int damages;
        public Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            rb.velocity = (transform.right + new Vector3(10, 0, 0)) * speed;
            Destroy(this.gameObject, 1f);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
                enemy.TakeDamage(damages);

            Destroy(this.gameObject);
        }

    }
}