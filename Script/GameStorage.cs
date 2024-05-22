using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStorage : MonoBehaviour
{
    private bool firstload = true;
    public static GameStorage instance;
    //storing varibles
    public int Opening;//stores opening
    public int Journey;//stores play progress
    public List<int> AbnormalCodes;//store abnormal code history
    public int totalabnormals;//stores total amount of abnormals in game
    public GameObject me;//stores the gamestorage
    void Awake()
    {
        DontDestroyOnLoad(gameObject); //makes it carry to the next scene
        if (firstload == true)//Only run at the fist scene loaded
        {
            if (instance == null)//if there is no DiffultyStorage
            {
                instance = this; //sets this as the main DiffultyStorage
                firstload = false; //no longer first load
            }
            else
            {
                Destroy(gameObject);//destroy if DiffultyStorage excists already
                return;
            }
        }
    }
    
}
