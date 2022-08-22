using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Q8
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float smoothness;
        Vector3 _velocity;

        void Start()
        {
            if (target)
            {
                offset = transform.position - target.position;
            }
        }

        void Update()
        {
            if (target)
            {
                // transform.position = target.position + offset;
                transform.position = Vector3.SmoothDamp(
                    transform.position, 
                    target.position + offset, 
                    ref _velocity,
                    smoothness
                );
            }
        }
    }
}
