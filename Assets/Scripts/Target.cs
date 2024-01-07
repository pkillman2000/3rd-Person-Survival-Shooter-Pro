using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public void SelfDestruct()
    {
        Debug.Log("You got me!");
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
