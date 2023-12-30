using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _gravityValue;

    private Vector3 _playerVelocity;
    public bool _isGrounded = true;
    private Vector3 _velocity;

    private CharacterController _characterController;

    // input system horz, vert
    // controller.move(WASAD * speed * time.deltatime)

    // Jump
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
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        MovePlayer();
    }

    private void Jump()
    {
        if(_isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }

    private void MovePlayer()
    {
        // Acts as gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);

        // Update _isGrounded
        _isGrounded = _characterController.isGrounded;

        // Player cannot change direction while airborne but will have intertia
        if (_isGrounded)
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        _characterController.Move(_velocity * _playerSpeed * Time.deltaTime);
    }

}
