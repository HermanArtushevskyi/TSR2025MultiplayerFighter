using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Runtime.Gameplay
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        public float _movementSpeed;
        public float _reloadTime;
        public float _jumpForce;
        public Rigidbody2D _rb;
        public Animator _animator;
        public GameObject BulletPrefab;

        [SyncVar] public int health = 3;

        private PlayInputActions _actions;
        private float _lastShootTime;
        private Transform _lookAtObject;
        private bool _shouldRotate;
        
        private void Awake()
        {
            _lastShootTime = 0f;
            if (tag == "Host") _lookAtObject = GameObject.FindWithTag("User").transform;
            else _lookAtObject = GameObject.FindWithTag("Host").transform;
            _actions = new PlayInputActions();
            _actions.Enable();
            _actions.Player.Shoot.performed += OnShoot;
            _actions.Player.Jump.performed += OnJump;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (isOwned == false) return;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            if (isOwned == false) return;
            Vector2 movementVector2 = _actions.Player.Movement.ReadValue<Vector2>();
            Move(movementVector2.x);
            _shouldRotate = transform.position.x - _lookAtObject.transform.position.x > 0;
            if (_shouldRotate) gameObject.transform.rotation = Quaternion.Euler(180f, 0f, 180f);
            else gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void Move(float movementAmount)
        {
            _rb.linearVelocityX = movementAmount * _movementSpeed;
        }

        private void OnShoot(InputAction.CallbackContext ctx)
        {
            if (isOwned == false) return;
            if (_reloadTime > Time.timeSinceLevelLoad - _lastShootTime) return;
            
            CmdShoot();
        }

        [Command]
        private void CmdShoot()
        {
            GameObject projectile = Instantiate(BulletPrefab, gameObject.transform.position, transform.rotation);
            projectile.tag = gameObject.tag;
            if (_shouldRotate) projectile.GetComponent<BulletBehaviour>()._speedMultiplier = -1;
            NetworkServer.Spawn(projectile);
            _lastShootTime = Time.timeSinceLevelLoad;
            RpcOnFire();
        }

        [ClientRpc]
        private void RpcOnFire()
        {
            
        }

        public void GetDamage()
        {
            health--;
        }
    }
}