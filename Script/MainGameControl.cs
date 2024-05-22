using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameControl : MonoBehaviour
{
    //Controls Main Game Features
    public GameStorage storage; //stores reference to games storage (A script used for storing info between scenes)
    public SaveManager saves; //stores reference to Save manager (Script used for interacting with Savefiles)
    public Animator opening; //Reference to the waking up animation.
    public Animator eyes;//reference to the players eyes transition animation
    public Character[] characters; //stores list of crew members on the ship and all there Abnormal states
    public Interaction SleepInteraction; //store bed interactor. (Used to tell when the player interacts with bed)

    public bool AbnormalPresence;//stores whether there is an abnormal on the ship
    public bool crewkilled;//store whether the player killed an innocent crew member

    private float Timer;//used to add a delay before the scene moves on. So the players eyes can close
    private bool Nextscene;//used to mark when the scene is moving on

    public Animator maploc;//the animator that controls the map of the players progress


    private int AbnormalCode;//store a code relating the current abnormal in the scene. (Abnormal codes are stored to make abnormals the player hasnt seen more likly to show up)

    private void ReturnRandomAbnormal()//spawns in random abnormal
    {
        List<int> Odds = new List<int> { };//List will store each Abnormals chances of spawning in. Chances are reduced if the player has already seen them
        int OddTotal = 0;//keep track of sum of all abnormal odds
        int abnormalcode = 0;//keep track of abnormal code
        //calc Odds
        
        for (int c = 0; c < characters.Length; c++)//loops through each crew member
        {
            for (int a = 0; a < characters[c].Abnormals.Length; a++)//loop through all abnormal states in each crew member
            {
                //has it been used in this game
                bool abnormalused = false;//store whether the abnormal has appeared before in the current round
                for (int h = 0; h < storage.AbnormalCodes.Count; h++)//goes through all abormal codes used in current round
                {
                    if (storage.AbnormalCodes[h] == abnormalcode)//if abnormal code used
                    {
                        abnormalused = true;//store as used
                        break;
                    }
                }
                if (abnormalused)//If the abnormal has been used before in this round
                {
                    Odds.Add(1);//make the odds of it appearing again small
                    OddTotal++;//increase the total odd count
                }
                else//check if abnormal has been seen by the player in any round
                {
                    abnormalused = false;//stores whether abnormal has been found
                    for (int s = 0; s < saves.AbnormalCodes.Count; s++)//loops through save file abnormal codes
                    {
                        if (saves.AbnormalCodes[s] == abnormalcode)//if the player has seen the abnormal before
                        {
                            abnormalused = true;//set found to true
                            break;
                        }
                    }
                    if (abnormalused) { Odds.Add(3);//if the player has seen the abnormal before give it smaller odds of appearing again compaired to a abnormal not seen by the player yet
                        OddTotal += 3;//increase the total odd count
                    }
                    else
                    { //if not seen by the player at all
                        Odds.Add(10);//if it large odds of appearing
                        OddTotal += 10;//increase the total odd count

                    }
                }
                abnormalcode++;//change to next abnormal code
            }
        }
        storage.totalabnormals = abnormalcode; //store the total amount of abnormals for ending scene
        //pick random odd

        int random = Random.Range(0, OddTotal);//pick a random number between 0 and the total odd number
        int indexchosen = 0;//stores the chosen odd in the list
        int oddscounter = 0;//store the odd count untill it reaches the chosen odd
        for (int i = 0; i < Odds.Count; i++)//loops through list of abnormal odds
        {
            oddscounter += Odds[i];//add odd to Oddscounter
            if (random < oddscounter)//if odds counter is larger than the random number pick
            {
                indexchosen = i;//choose the current index
                break;//break the loop
            }
        }
        //find adnormal chosen
        abnormalcode = 0;//used to find abnormal code
        for (int ch = 0; ch < characters.Length; ch++)//loop through crew member to find the abnormal
        {
            for (int ab = 0; ab < characters[ch].Abnormals.Length; ab++)//loop through abnormal in crew members
            {
                if (abnormalcode == indexchosen)//if current abnormal code in loop is equal to the index picked
                {
                    Destroy(characters[ch].SpawnedObject); //destroy normal verison of crewmate
                    characters[ch].SpawnedObject = Instantiate(characters[ch].Abnormals[ab], new Vector3(0, 0, 0), Quaternion.identity);//spawn abnormal version of crewmate
                    storage.AbnormalCodes.Add(abnormalcode);//store abnormal code in list of used abnormal codes
                    AbnormalCode = abnormalcode;//store the current abnormal code
                    return;
                }
                else { abnormalcode++; } //move to next abnormal code
            }
        }

    }

    //ran before start
    void Awake()
    {
        storage = FindObjectOfType<GameStorage>();//finds Game Storage Object
        opening.SetInteger("Opening", storage.Opening); //Sets the Opening animation of the scene -1 = start 0=Normal 1=Death by captain 2=Death by abnormal
        Nextscene =false;
        Timer = 0.5f;
        AbnormalCode = -1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        crewkilled = false;//scene opens with no crewmates dead
        if (storage.Opening < 1)//if not a death scene
        {
            if (storage.Opening == -1) { //if the very first opening scene
                AbnormalPresence = false;//spawn only normal crewmates
                maploc.SetInteger("Loc", 0);//set map location to 0
            }
            else//if opening is normal
            {
                int random = Random.Range(0, 5);//pick a random number
                if (random > 0)//if over 0 set abnormal presence to true 1 in 4 chance of there being an abnormal
                {
                    AbnormalPresence = true;
                }
                else { AbnormalPresence = false; }//else set abnormal presence to false
            }
            for(int i = 0; i < characters.Length; i++)//loop though all crewmates
            {
                characters[i].SpawnedObject = Instantiate(characters[i].Normal, new Vector3(0, 0, 0), Quaternion.identity);//spawn normal crewmate in
            }
            if (AbnormalPresence)//if abnormal is present
            {
                ReturnRandomAbnormal();//spawn random abnormal in (this will delete a normal crewmate)
            }
            maploc.SetInteger("Loc", storage.Journey);//set map location to current journey status 
        }
        storage.Opening = 0;//set opening back to normal


    }

    // Update is called once per frame
    void Update()
    {
        if (SleepInteraction.InteractedWith)//if bed has been interacted with
        {
            NextScene(false);//move to next scene (Player didnt die)
        }
        if (Nextscene)//if moving to next scene
        {
            Timer -= Time.deltaTime;//update timer
            if (Timer < 0)//if timer delay runs out move to next scene
            {
                if(storage.Journey > 8)//if journey comes to an end
                {
                    SceneManager.LoadScene("Won");//go to winning scene
                }
                else { SceneManager.LoadScene("GAME"); }//else restart this scene
                
            }
        }
    }

    private bool PresentInSave(int code) //checks if abnormal code is already in the players savefile
    {
        bool foundinsave = false;//stores whether code was found or not
        for (int i = 0; i < saves.AbnormalCodes.Count; i++)//loop though save file codes
        {
            if (saves.AbnormalCodes[i] == code)//if code is present
            {
                foundinsave = true;//found = true
                break;//break loop
            }
        }
        return foundinsave;//return whether code was found
    }

    public void NextScene(bool death)//Sets player up to move onto the next scene. 
    {
        if (!Nextscene) {//only run if Nextscene hasnt been called before
            if (death)//if the player is dead e.g(Jumped off ship, killed by abnormal or captain)
            {
                storage.Journey = 1;//set the player progress back to 1
                storage.Opening = 0; //set opening to normal
                storage.AbnormalCodes = new List<int>();//reset abnormal codes history
            }
            else if (!crewkilled && !AbnormalPresence)//if player hasnt failed
            {
                storage.Journey++;//increase the players progress
                storage.Opening = 0;//set opening to normal
            }
            else if (crewkilled) { storage.Opening = 1; } //if you kill a member of crew set opening to captain killing you scene
            else { storage.Opening = 2; }  //if you miss abnormal set opening to abnormal killing you scene
            eyes.SetBool("Open", false);//close the players eyes
            if(!AbnormalPresence && AbnormalCode != -1 && !PresentInSave(AbnormalCode) && !crewkilled)//if the player wins and the abnormal was new
            {
                saves.AbnormalCodes.Add(AbnormalCode);//save abnormal code
                saves.SaveGame();
            }
            Nextscene = true;//start delay for next scene
        }
        
    }
}
