using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

namespace Q8
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D _rigidbody;
        Animator _animator;
        SpriteRenderer _spriteRenderer;
        public KeyCode keyUp;
        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public float moveSpeed;
        public Direction facingDirection;
        public Transform[] attackZones;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            if (Input.GetKey(keyUp))
            {
                _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyDown))
            {
                _rigidbody.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyLeft))
            {
                _rigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyRight))
            {
                _rigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            }
        }

        void Update()
        {
            float movementSpeed = _rigidbody.velocity.magnitude;
            _animator.SetFloat("speed", movementSpeed);
            if (movementSpeed > 0.1f)
            {
                _animator.SetFloat("movementX", _rigidbody.velocity.x);
                _animator.SetFloat("movementY", _rigidbody.velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("attack");
                int facingDirectionIndex = (int) facingDirection;
                Transform attackZone = attackZones[facingDirectionIndex];
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 0.1f);
                foreach(Collider2D hit in hits)
                {
                    Breakable breakableObject = hit.GetComponent<Breakable>();
                    if (breakableObject)
                    {
                        breakableObject.Break();
                    }
                }
            }
        }

        void LateUpdate()
        {
            if (string.Equals(_spriteRenderer.sprite.name, "zelda1_8"))
            {
                facingDirection = Direction.Up;
            }
            else if (string.Equals(_spriteRenderer.sprite.name, "zelda1_4"))
            {
                facingDirection = Direction.Down;
            }
            else if (string.Equals(_spriteRenderer.sprite.name, "zelda1_10"))
            {
                facingDirection = Direction.Left;
            }
            else if (string.Equals(_spriteRenderer.sprite.name, "zelda1_6"))
            {
                facingDirection = Direction.Right;
            }
        }
    }
}
