using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }
    [SerializeField]
    private EnemyState _currentState;
    
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _gravityValue;
    private Vector3 _enemyGravity;
    private bool _isGrounded;
    private Vector3 _directonToPlayer;
    
    [SerializeField]
    private int _weaponDamage;
    [SerializeField]
    private float _attackFrequency;

    private CharacterController _characterController;
    private Player _player;
    private Health _playerHealth;
    private Health _myHealth;
    public bool _playerIsAlive;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if(_characterController == null )
        {
            Debug.LogWarning("Character Controller is null");
        }

        _player = FindObjectOfType<Player>();
        if(_player == null )
        {
            Debug.LogWarning("Player is null");
        }
        else
        {
            _playerHealth = _player.GetComponent<Health>();
        }

        _myHealth = GetComponent<Health>();
        if( _myHealth == null ) 
        {
            Debug.LogWarning("My Health is null");
        }

        _isGrounded = _characterController.isGrounded;
        _currentState = EnemyState.Chasing;
    }

    void Update()
    {
        if(_myHealth.GetLifeStatus()) // I'm still alive
        {
            Gravity();
            _isGrounded = _characterController.isGrounded;

            // Must be grounded to move and in Chasing state
            if (_isGrounded && _currentState == EnemyState.Chasing)
            {
                MoveToTarget();
                _playerIsAlive = _playerHealth.GetLifeStatus();
            }
        }
        else // I'm dead
        {
            _currentState = EnemyState.Idle;
        }
    }

    private void MoveToTarget()
    {
        // Find direction to player
        _directonToPlayer = _player.transform.position - this.transform.position;
        _directonToPlayer.y = 0;

        // Rotate towards player - Slerp makes it smooth
        if(_directonToPlayer != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_directonToPlayer, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        // Move towards player
        _characterController.Move(_directonToPlayer.normalized * _movementSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        _enemyGravity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_enemyGravity * Time.deltaTime);
    }

    IEnumerator AttackTarget()
    {
        // Must be in Attacking state and Player must be alive
        while (_currentState == EnemyState.Attacking && _playerIsAlive)
        {
            _playerHealth.TakeDamage(_weaponDamage);
            _playerIsAlive = _playerHealth.GetLifeStatus();

            // Check if I just killed Player - if so, exit loop
            if (!_playerIsAlive)
            {
                Debug.Log("I defeated you!");
                _currentState = EnemyState.Idle;
                yield return null;
            }

            yield return new WaitForSeconds(_attackFrequency);
        }
    }

    // Start Attacking state
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            _playerIsAlive = _playerHealth.GetLifeStatus();
            _currentState = EnemyState.Attacking;
            StartCoroutine(AttackTarget());
        }
    }

    // Start Chasing state
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            _currentState = EnemyState.Chasing;
            Debug.Log("Trigger Exit");
        }
    }
}
