using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteraction : MonoBehaviour
{
    //used to make player talk
    public Interaction MyInteraction;//reference to Interaction control
    public AudioManager Amanager; //reference to an audio manager
    public string TrackName;//stores voice file name
    private bool talk; //if interaction will make npc talk
    private float TalkCounter; //stores how long to talk for

    public Animator TalkAnimation; //reference to talk animation
    public string AnimationName; //stores name of animation

    public Stabable stabbed; //stores script which allows npcs to be stabbed
    private bool gotstabbed; //stores whether npc was stabbed

    // Start is called before the first frame update
    void Start()
    {
        talk = true; //npc will talk if interacted with
        TalkCounter = 0; 
        gotstabbed = false; //npc is not stabbed 
    }

    // Update is called once per frame
    void Update()
    {
        if (!gotstabbed)//if not stabbed
        {
            if (MyInteraction.InteractedWith)//if player interacts with NPC
            {
                MyInteraction.InteractedWith = false;//reset interaction script
                if (talk)//if the npc is going to talk
                {
                    TalkCounter = Amanager.Play(TrackName);//play voice file
                    MyInteraction.TypeText = "Shush"; //reset interaction to shush npc
                }
                else//if the npc is going to hush
                {
                    Amanager.Stop(TrackName);//stop sound file
                    TalkCounter = 0;
                    MyInteraction.TypeText = "Play";//reset interaction to talk with npc
                }
                if (TalkAnimation != null) { TalkAnimation.SetBool(AnimationName, talk); }//set animation if animator is present
                talk = !talk;//change interaction type
            }
            if (TalkCounter > 0)//if talkcounter is higher than 0
            {
                TalkCounter -= Time.deltaTime;//update counter
            }
            else if (!talk)//if currently talking
            {
                TalkCounter = 0;
                if (TalkAnimation != null) { TalkAnimation.SetBool(AnimationName, talk); } //set animation if animator is present
                talk = true;//set talk to true
                MyInteraction.TypeText = "Play";
            }
            gotstabbed = stabbed.Stabbed;//check if player has been stabbed
            if (gotstabbed)//if stabbed
            {
                Amanager.Stop(TrackName);//stop any tracks that might be playing
                if (TalkAnimation != null) { TalkAnimation.SetBool(AnimationName, talk); }//set animation if animator is present
                MyInteraction.Interactable = false;//turn of interactions
            }
        }
    }
}
