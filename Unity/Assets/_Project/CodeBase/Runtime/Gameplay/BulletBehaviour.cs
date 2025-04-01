using System;
using Mirror;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay
{
    public class BulletBehaviour : NetworkBehaviour
    {
        public float _speed;
        public Rigidbody2D _rb;
        public float _speedMultiplier = 1;

        private void FixedUpdate()
        {
            _rb.linearVelocityX = _speed * _speedMultiplier;
        }

        [Server]
        private void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        [ServerCallback]
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("trigger");
            if (other.gameObject.tag == this.gameObject.tag) return;

            if (other.TryGetComponent<PlayerBehaviour>(out var player))
            {
                player.GetDamage();
            }
            
            DestroySelf();
        }
        
        [ServerCallback]
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == this.gameObject.tag) return;

            if (other.gameObject.TryGetComponent<PlayerBehaviour>(out var player))
            {
                player.GetDamage();
            }
            
            DestroySelf();
        }
    }
}