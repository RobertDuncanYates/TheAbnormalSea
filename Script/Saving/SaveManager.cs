using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //this script makes communicating with the SavingScript alot easier
    //For 

    public int Sensitivity;
    public float Volume;
    public List<int> AbnormalCodes;

    void Awake()
    {
        Volume = 0.5f;
        Sensitivity = 80;
        AbnormalCodes = new List<int>();
        LoadGame();//loads data in the beginning
    }
    public void SaveGame()//save game data (call this method when changes have been made)
    {
        SavingScript.SaveTheData(this);
    }
    public void LoadGame() //loading up data previously saved
    {
        try//in case savefile can not be found and load fails
        {
            SaveData data = SavingScript.LoadTheData();
            Volume = data.Volume;
            Sensitivity = data.Sensitivity;
            AbnormalCodes = data.AbnormalCodes;

        }
        catch { }
    }
}
