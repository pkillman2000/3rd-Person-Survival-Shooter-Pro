using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private int _weaponDamage;
    [SerializeField]
    private Camera _camera;

    private RaycastHit _hit;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse button
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        // See if we hit anything
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit))
        {            
            // Check to see if hit was target
            if (_hit.transform.GetComponent<Health>() != null)
            {
                _hit.transform.GetComponent<Health>().TakeDamage(_weaponDamage);
            }
        }
    }    
}
