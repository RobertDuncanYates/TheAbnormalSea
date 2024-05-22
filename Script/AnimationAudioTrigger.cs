using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioTrigger : MonoBehaviour //allows animations to trigger audio managers
{
    public AudioManager audiom;//stores  AudioManager
    public bool TriggerAudio; //stores whether to trigger audio
    private bool PTriggered = false; //variable for detecting changes in TriggerAudio
    public string AudioName;//stores audio track name

    // Update is called once per frame
    void Update()
    {
        if (TriggerAudio && !PTriggered)//if audio is triggered then play audio
        {
            audiom.Play(AudioName);
        }
        if (TriggerAudio != PTriggered) { PTriggered = TriggerAudio; }//update everytime TriggerAudio value changes
    }
}
