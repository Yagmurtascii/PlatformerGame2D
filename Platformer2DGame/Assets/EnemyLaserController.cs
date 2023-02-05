using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class EnemyLaserController : MonoBehaviour
    {
        public float speed;
        public int damages;
        public Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        { 
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            FlyPlayerController player = collision.collider.GetComponent<FlyPlayerController>();
            if (player != null)
                player.TakeDamage(damages);
            Destroy(this.gameObject);
        }


    }
}
