using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MainGameControl gamecontrol; //reference to game control. Script which store data between scenes
    public float speed = 1; //stores player speed
    public CharacterController character; //reference to character controller
    public LayerMask groundlayer; //reference to ground layer
    public GameObject groundobject;//reference to ground detection point

    private float FallingSpeed; //stores players falling speed
    public bool turnoffanimator; //allows opening animator to turn itself off
    public Animator openinganimation; //reference to opening animation

    public bool KillPlayer;//allows animator to kill player
    

    // Update is called once per frame
    void Update()
    {
        if (turnoffanimator)//if animator needs to turn off
        {
            turnoffanimator = false;
            openinganimation.enabled = false; //turn off animator
        }
        if (KillPlayer)//if kill player
        {
            gamecontrol.NextScene(true);//load next scene (Kill player = true)
        }
    }


    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");//get input
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x * speed) + (transform.forward * z * speed); //moves the player along the x and z
        character.Move(move * Time.fixedDeltaTime); //move* speed *Time.deltaTime
                                                    //gravity
        bool Grounded = CheckIfGrounded();//check if on ground
        if (Grounded) { FallingSpeed = 0; }//if on ground reset falling speed
        else
        {
            FallingSpeed += -0.1f * Time.fixedDeltaTime; //else increase falling speed
            character.Move(Vector3.up * FallingSpeed * Time.fixedDeltaTime); //make player fall
        }
    }
    private bool CheckIfGrounded()
    {
        float raylength = character.center.y + 0.01f; //fire ray down
        //bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, groundlayer);
        bool hasHit = Physics.CheckSphere(groundobject.transform.position, 0.4f, groundlayer); //store if it hits ground
        if (gameObject.transform.position.y == 0)//if y == 0 return on ground
        {
            return true;
        }
        else if (gameObject.transform.position.y < 0)//if less than 0 tp player back to 0
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
        return (hasHit);

    }
}
