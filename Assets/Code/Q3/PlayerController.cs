using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Q3
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        SpriteRenderer sprite;
        Animator animator;
        public int jumpsLeft;
        public int score;
        public TMP_Text scoreUI;
        public bool isPaused;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            score = PlayerPrefs.GetInt("Score");
            _rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
            animator.speed = _rigidbody2D.velocity.magnitude > 0 ? _rigidbody2D.velocity.magnitude / 3f : 1f;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {
                return;
            }
            scoreUI.text = score.ToString();

            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 12f * Time.deltaTime, ForceMode2D.Impulse);
                sprite.flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                jumpsLeft--;
                _rigidbody2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            }
            animator.SetInteger("JumpsLeft", jumpsLeft);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;
            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;
            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuController.instance.Show();
            }
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.9f);
                // Debug.DrawRay(transform.position, -transform.up * 0.9f);

                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        jumpsLeft = 2;
                    }
                }
            }
        }
    }
}
