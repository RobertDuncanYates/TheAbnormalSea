using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform PlayerBoady;//stores playes body
    public float Sensitivity;//store mouse sensitivity
    private float XRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock Mouse Curser
        Cursor.visible = false; //hide cursor

    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;//get mouse inputs
        float mousey = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
        XRotation -= mousey;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);//lock rotation about in a range
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);//rotate on y
        PlayerBoady.Rotate(Vector3.up * mousex);//rotate on x

    }
}
