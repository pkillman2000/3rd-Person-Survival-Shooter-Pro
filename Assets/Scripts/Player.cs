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

    private Vector3 _playerVelocity;
    private bool _isGrounded = true;

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
        // Update _isGrounded
        _isGrounded = _characterController.isGrounded;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        Move();
    }

    private void Move()
    {
        _mouseX = Input.GetAxis("Mouse X");
        // Acts as gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);        

        this.transform.Rotate(0, _mouseX * _playerRotationSpeed, 0);

        // Move player
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = _playerSpeed * Input.GetAxis("Vertical");
        _characterController.SimpleMove(forward * curSpeed);
    }
    private void Jump()
    {

        if (_isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
