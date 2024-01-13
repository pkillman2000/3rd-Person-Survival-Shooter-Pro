using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;
    [SerializeField] 
    private float _playerRotationSpeed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityValue;

    private Vector3 _playerGravity;
    public bool _isGrounded = true;
    private float _mouseX;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null)
        {
            Debug.LogWarning("Character Controller is null!");
        }
    }

    void Update()
    {
        // Apply gravity before any movement
        Gravity();
        _isGrounded = _characterController.isGrounded;

        Jump();
        Move();
    }

    // Player does not need to be grounded to move
    // It can change directon and movement in mid-air
    private void Move()
    {
            // Move player with keyboard - forwards/backwards and left/right
            float strafe = Input.GetAxis("Horizontal"); // Left/Right keys
            float curSpeed = Input.GetAxis("Vertical"); // Up/Down keys
            Vector3 _direction = new Vector3(strafe, 0, curSpeed);
            Vector3 velocity = _direction * _playerSpeed;

            /* Rotate player
             * Convert local space to world space - TransformDirection
             * Player moves in the direction it is facing
            */
            _mouseX = Input.GetAxis("Mouse X");
            this.transform.Rotate(0, _mouseX * _playerRotationSpeed, 0);
            velocity = transform.TransformDirection(velocity);

            // Move Player
            _characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _playerGravity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _characterController.Move(_playerGravity * Time.deltaTime);
        }
    }

    private void Gravity()
    {
        _playerGravity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerGravity * Time.deltaTime);
    }
}
