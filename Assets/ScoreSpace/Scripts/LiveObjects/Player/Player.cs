using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Player : LiveObject
    {
        [SerializeField] private Bit _bit;

        [SerializeField] protected float RotationSpeed = 4;

        [SerializeField] private float _dashSpeed = 10;
        [SerializeField] private float _dashTime = 0.3f;

        [SerializeField] private int _dashCost = 3;
        [SerializeField] private int _currentDash = 3;

        protected float StandardRotationMultiply = 1;

        private PlayerState _state = PlayerState.Moving;
        public PlayerState State => _state;

        private float _horizontal;

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
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.AddForce(transform.up * StandardSpeedMultiply * _dashSpeed * Time.deltaTime, ForceMode2D.Impulse);
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

        private void Rotate(float horizontal)
        {
            Rigidbody.SetRotation(transform.rotation.eulerAngles.z - (horizontal * StandardRotationMultiply * RotationSpeed));
        }
    }

    public enum PlayerState
    {
        Moving,
        Dashing
    }
}
