using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    [SerializeField]
    private int _currentHealth;

    public bool _isAlive;


    void Start()
    {
        _currentHealth = _maxHealth;
        _isAlive = true;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth < _minHealth)
        {
            _isAlive = false;
            Die();
        }   
    }

    // Is character still alive?
    public bool GetLifeStatus()
    {
        return _isAlive;
    }

    public void Die()
    {
        this.transform.Rotate(0, 0, 90);
        GetComponent<MeshRenderer>().material.color = Color.black;

        /* TODO
         * Add some type of death VFX
         * Add some type of death SFX
         * 
         * Maybe check to see if this is attached to Player.  
         * If it is, respawn instead of destroying the Player.
         * Player has Main.Camera attached and destroying Player 
         * causes problems.
         * 
         * If an enemy, destroy object?
        */

    }
}
