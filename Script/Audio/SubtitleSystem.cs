using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleSystem : MonoBehaviour
{
    public TextMeshProUGUI SubtitleText;//reference to subtitle text
    public GameObject subtitlebox;//reference to subtitle box
    private List<subtitle> subtitles = new List<subtitle>();//list of subtitles
    private bool nosubtitles; //if subtitles are on screen


    // Start is called before the first frame update
    void Start()
    {
        nosubtitles=false;
        UpdateSubtitles();

    }
    public void AddSubtitle(subtitle newsubtitle,float LengthAudio)//adds subtitles
    {
        LengthAudio += 2;//subtitle will last length of audio + 2 seconds
        if(LengthAudio < 3) { LengthAudio = 3; }; //Subtitles will not be on for less than 3 seconds
        newsubtitle.timer = LengthAudio;//set subtitles timer
        for(int i = 0; i < subtitles.Count; i++)//loop through subtitle list
        {
            if (subtitles[i].nameofsource == newsubtitle.nameofsource)//if subtitle from the same source is present remove it
            {
                subtitles.RemoveAt(i);
            }
        }
        subtitles.Add(newsubtitle);//add subtitle to list
        nosubtitles = false;
        UpdateSubtitles();//update subtitles

    }
    private void UpdateSubtitles()//update subtitle text
    {
        string TheSubtitles= "";//subtitle text
        if (!nosubtitles)//if subtitles active 
        {
            for (int i = 0; i < subtitles.Count; i++)//loop through subtitles
            {
                TheSubtitles += subtitles[i].nameofsource + ": " + subtitles[i].thesubtitle + "\n";//add subtitle to text
            }
        }
        SubtitleText.text = TheSubtitles; //set subtitle text
        subtitlebox.SetActive(TheSubtitles != "");//If text is blank, hide subtitle box
    }
    // Update is called once per frame
    void Update()
    {
        
        if(subtitles.Count > 0)//if subtitles present
        {
            nosubtitles = false;
            bool updatesub = false;//stores whether to update subtitles
            for (int i = 0; i < subtitles.Count; i++)//loop through subtitles
            {
                if (subtitles[i].timer > 0)//update subtitle timers
                {
                    subtitles[i].timer -= Time.deltaTime;
                }
                else { //if timers gets to 0
                    subtitles.RemoveAt(i);//remove subtitle
                    i--;//set i back 1
                    updatesub = true;//update subtitles
                }
            }
            if (updatesub) { UpdateSubtitles(); }
        }
        else if(!nosubtitles)//if no subtitles
        {
            nosubtitles = true;//variable used to make this run once
            UpdateSubtitles();//update subtitles
        }
    }
}
