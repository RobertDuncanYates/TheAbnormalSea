using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
    //used to add funtionality to menus
    public GameStorage storage;//stores reference to games storage (A script used for storing info between scenes)
    private int totalabnormal;//stores total abnormals in game
    public TextMeshProUGUI TheText;//stores reference to textbox
    public SaveManager saves;//stores reference to Save manager (Script used for interacting with Savefiles)

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; //Lock Mouse Curser
        Cursor.visible = true; //hide cursor
        try//try and catch (Stops error if game storage is already destroyed
        {
            storage = FindObjectOfType<GameStorage>();//finds game storage
            totalabnormal= storage.totalabnormals;//stores total abnormals
            Destroy(storage.me);//Destroy gamestorage so new one will appear
        }
        catch { }
        TheText.text = saves.AbnormalCodes.Count + "/" + totalabnormal.ToString() + " Abnormals Found";//set text to display how many abnormals the player has found
    }

   
    public void Quit()//Runs quit command
    {
        Application.Quit();
    }
    public void GoToScene(string scene)//changes the scene
    {
        SceneManager.LoadScene(scene);
    }
}
