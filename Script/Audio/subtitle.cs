
using UnityEngine;
[System.Serializable]
public class subtitle 
{
    public bool SubActive; //stores whether subtitle is active 
    public string nameofsource; //name of subtitle source
    public string thesubtitle; //the subtitle
    public float timer; //how long subtitle is on screen

    public string ReturnLine()//returns full subtitle line
    {
        string line = thesubtitle;
        if(nameofsource != "")
        {
            line = nameofsource + " : " + line;
        }
        return (line);
    }
}
