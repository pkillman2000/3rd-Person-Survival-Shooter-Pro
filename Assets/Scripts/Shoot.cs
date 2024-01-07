using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private RaycastHit _hit;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        // See if we hit anything
        if (Physics.Raycast(transform.position, transform.forward, out _hit))
        {
            Debug.Log("Target Name: " + _hit.transform.name);
            
            // Check to see if hit was target
            if (_hit.transform.GetComponent<Target>() != null)
            {
                Debug.Log("Target Present");
                _hit.transform.GetComponent<Target>().SelfDestruct();
            }
        }
    }
}
