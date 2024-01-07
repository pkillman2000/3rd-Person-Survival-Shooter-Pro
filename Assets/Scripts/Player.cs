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
        // Move player down if applicable
        Gravity();
        _isGrounded = _characterController.isGrounded;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        // Move player
        Move();
    }

    private void Move()
    {
        float strafe = Input.GetAxis("Horizontal");
        float curSpeed = Input.GetAxis("Vertical");
        Vector3 _direction = new Vector3(strafe, 0, curSpeed);
        Vector3 velocity = _direction * _playerSpeed;
        _mouseX = Input.GetAxis("Mouse X");

        // Rotate player
        this.transform.Rotate(0, _mouseX * _playerRotationSpeed, 0);

        // Convert local space to world space
        // It moves the direction the player is facing on not 
        // world space Z
        velocity = transform.TransformDirection(velocity);
        // Move Player
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _playerGravity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _characterController.Move(_playerGravity * Time.deltaTime);
        }
    }

    // Acts as gravity
    private void Gravity()
    {
        _playerGravity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerGravity * Time.deltaTime);
    }
}
