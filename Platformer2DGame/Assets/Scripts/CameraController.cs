using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{


    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}
