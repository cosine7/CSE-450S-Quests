using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q5
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody2D _rigidbody;
        Transform target;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float acceleration = GameController.instance.missileSpeed / 2f;
            float maxSpeed = GameController.instance.missileSpeed;

            ChooseNearestTarget();
            if (target != null)
            {
                Vector2 directionToTarget = target.position - transform.position;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                _rigidbody.MoveRotation(angle);
            }
            _rigidbody.AddForce(transform.right * acceleration);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }

        void ChooseNearestTarget()
        {
            float closestDistance = 9999f;
            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

            foreach (var asteroid in asteroids)
            {
                if (asteroid.transform.position.x > transform.position.x)
                {
                    Vector2 directionToTarget = asteroid.transform.position - transform.position;

                    if (directionToTarget.sqrMagnitude < closestDistance)
                    {
                        closestDistance = directionToTarget.sqrMagnitude;
                        target = asteroid.transform;
                    }
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Asteroid>())
            {
                Destroy(other.gameObject);
                Destroy(gameObject);

                GameObject explosion = Instantiate(
                    GameController.instance.explosionPrefab,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(explosion, 0.25f);
                GameController.instance.EarnPoints(10);
            }
        }
    }
}
