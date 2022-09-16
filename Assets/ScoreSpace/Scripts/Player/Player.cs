using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Bit _bit;

        [SerializeField] private float _speed = 4;
        [SerializeField] private float _maxVelocity = 4;

        [SerializeField] private float _rotationSpeed = 4;

        [SerializeField] private float _dashSpeed = 10;
        [SerializeField] private float _dashTime = 0.3f;

        [SerializeField] private int _dashCost = 3;
        [SerializeField] private int _currentDash = 3;

        private PlayerState _state = PlayerState.Moving;
        public PlayerState State => _state;

        private Transform _transform;

        private float _standardSpeedMultiply = 30;
        private float _standardRotationMultiply = 1;
        private float _horizontal;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_state != PlayerState.Moving)
                return;

            Move();

            _horizontal = Input.GetAxis("Horizontal");

            if (_horizontal != 0)
                Rotate(_horizontal);
        }

        private void LateUpdate()
        {
            if (_state != PlayerState.Moving)
                return;

            if (Input.GetKeyDown(KeyCode.Space) && _bit.InBit)
            {
                if (_currentDash >= _dashCost)
                    Dash();
                else
                    ReduceDashCooldown();
            }
        }

        private void Dash()
        {
            _rigidbody.AddForce(_transform.up * _standardSpeedMultiply * _dashSpeed * Time.deltaTime, ForceMode2D.Impulse);
            _state = PlayerState.Dashing;

            _currentDash -= _dashCost;

            StartCoroutine(WaitForDash());
        }

        private IEnumerator WaitForDash()
        {
            yield return new WaitForSeconds(_dashTime);

            _state = PlayerState.Moving;
        }

        private void ReduceDashCooldown()
        {
            _currentDash++;
        }

        private void Move()
        {
            _rigidbody.AddForce(_transform.up * _standardSpeedMultiply * _speed * Time.deltaTime);

            if (_rigidbody.velocity.x > _maxVelocity)
                _rigidbody.velocity = new Vector2(_maxVelocity, _rigidbody.velocity.y);

            if (_rigidbody.velocity.y > _maxVelocity)
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _maxVelocity);
        }

        private void Rotate(float horizontal)
        {
            _rigidbody.SetRotation(_transform.rotation.eulerAngles.z - (horizontal * _standardRotationMultiply * _rotationSpeed));
            //_transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.z - (horizontal * _standardRotationMultiply * _rotationSpeed));
        }
    }

    public enum PlayerState
    {
        Moving,
        Dashing,
        Death
    }
}
