using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SaveData
{
    //This is the data that will be saved in the savefile
    //For Game

    //For Options Unused
    public float Volume;
    public int Sensitivity;

    //gamedata
    public List<int> AbnormalCodes;//stores abnormal the player has seen

    public SaveData(SaveManager savemanager)
    {
        Volume = savemanager.Volume;
        Sensitivity = savemanager.Sensitivity;
        AbnormalCodes = savemanager.AbnormalCodes;
    }
}
