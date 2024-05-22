using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabable : MonoBehaviour
{
    //sword will detect any objects with this script
    public bool Stabbed; //whether npc has been stabbed
    private bool animationplayed;//whether animation has been played
    public Animator deathanimation; //reference to death animation
    public string animationname;//animation name

    public bool Abnormal; //whether this npc is abnormal
    private MainGameControl gamecontrol; //link to main control script
    // Start is called before the first frame update
    void Start()
    {
        gamecontrol = FindObjectOfType<MainGameControl>();//find main game control script
        animationplayed =false;
        Stabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Stabbed && !animationplayed && deathanimation != null) //if stabbed and animation not played
        {
            deathanimation.SetInteger(animationname, 1);//play death animation
            animationplayed = true;//set animation played to true
            if (Abnormal){ gamecontrol.AbnormalPresence = false; }//if abnormal set AbnormalPresence to false
            else { gamecontrol.crewkilled = true; }//if not abnormal set crewkilled to true
        }
    }
}
