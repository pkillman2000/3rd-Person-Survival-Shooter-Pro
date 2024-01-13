using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* This is a quick and dirty script to allow 
 * the player some target practice.
*/
public class Target : MonoBehaviour
{
    public void SelfDestruct()
    {
        Debug.Log("You got me!");
        Destroy(this.gameObject);
    }
}
