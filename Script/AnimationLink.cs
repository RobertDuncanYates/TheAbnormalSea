using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLink : MonoBehaviour//allows animators to effect other animators
{
    public Animator animationcontrol;//stores animator
    public bool intactive;//stores if there is a int variable in animator
    public string intvarname;//stores name of int variable 
    public int intvalue;//stores value of variable 

    public bool boolactive;//stores if there is a bool variable in animator
    public string boolvarname;//stores name of bool variable
    public bool boolvalue;//stores value of variable


    // Update is called once per frame
    void Update()
    {
        if (intactive)//if there is a int variable. 
        {
            animationcontrol.SetInteger(intvarname, intvalue);//set value of variable.
        }
        if (boolactive)//if there is a bool variable.
        {
            animationcontrol.SetBool(boolvarname, boolvalue);//set value of variable.
        }
    }
}
