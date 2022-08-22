using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q5
{
    public class Asteroid : MonoBehaviour
    {
        Rigidbody2D _rigidbody;
        float randomSpeed;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            randomSpeed = Random.Range(0.5f, 3f);
        }

        void Update()
        {
            _rigidbody.velocity = Vector2.left * randomSpeed;
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
