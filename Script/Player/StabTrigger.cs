using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabTrigger : MonoBehaviour
{
    // Used to stab crewmembers. Trigger appears on sword when swinging
    private void OnTriggerEnter(Collider other)//if object enters trigger
    {
        Debug.Log(other.name);
        try//try find Stabable script
        {
            Stabable stab = other.gameObject.GetComponent<Stabable>();
            stab.Stabbed = true; //set stabbed to true
        }
        catch { }
        

    }


}
