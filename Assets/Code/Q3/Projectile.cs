using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q3
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * 10f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Target>())
            {
                SoundManager.instance.PlaySoundHit();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                SoundManager.instance.PlaySoundMiss();
            }
            Destroy(gameObject);
        }
    }
}
