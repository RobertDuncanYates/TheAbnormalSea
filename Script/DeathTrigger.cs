using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour//trigger that will kill player. Placed in the sea
{
    public void OnTriggerEnter(Collider other)//if an object enters the trigger
    {
        if(other.tag == "Player")//if object is player
        {
            MainGameControl gamecontrol = FindObjectOfType<MainGameControl>();//find game control
            gamecontrol.NextScene(true);//move scene along. Death = true
        }
        
    }
}
