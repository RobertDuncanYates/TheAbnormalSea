using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject Player;//stores players object
    public float PlayerReach;//stores how far the player can reach
    public Animator swinganimation;//reference to sword animator
    public Camera MainCam;//reference to main camera
    public GameObject crosshair;//reference to UI cross hair

    private Interaction interactionobject;//stores the interactionobject the cross hair is currently on
    private bool ValidInteraction; //stores whether the player has a valid interaction
    public GameObject interactUIText;//stores gameobject of UI text
    public TextMeshProUGUI interactTextMesh;//reference to UI text

    // Start is called before the first frame update
    void Start()
    {
        ValidInteraction=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//if RMB down swing sword
        {
            swinganimation.SetBool("Swing", true);
        }
        else { swinganimation.SetBool("Swing", false); }//else don't swing sword
        var ray = MainCam.ScreenPointToRay(crosshair.transform.position);//fire raycast from crosshair
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))//if raycast hits
        {
            try//try and find a interaction script 
            {
                interactionobject = hit.collider.gameObject.GetComponent<Interaction>();
                float dist = Vector3.Distance(Player.transform.position, hit.collider.gameObject.transform.position);//calc distence between player and hit object
                if (interactionobject.Interactable && dist <= PlayerReach) { //if object is within players reach and interaction object is interactable
                    ValidInteraction = true;//set valid interaction to true
                    interactTextMesh.text = "Press E to " + interactionobject.TypeText + "!";//set text 
                }
                else { ValidInteraction = false; }//else there is no valid interaction

            }
            catch
            {
                ValidInteraction = false;//if the script fails to find interaction object there is no valid interaction
            }
        }
        else { ValidInteraction = false; }//if the raycast doesnt hit anything. There is no valid interaction
        interactUIText.SetActive(ValidInteraction);//show interaction UI text if there is a valid interaction
        if (Input.GetKeyDown(KeyCode.E) && ValidInteraction && interactionobject != null)//if e is pressed and there is a valid interaction
        {
            interactionobject.InteractedWith = true;//Interact with Object
        }

    }
}
