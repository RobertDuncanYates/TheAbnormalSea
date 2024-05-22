using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour //players interactor will recognise any objecy with this script and will tell it can be interacted with
{
    public bool Interactable;//if object can be interacted with
    public bool InteractedWith; //Whether the object has been interacted with
    public string TypeText;//interaction text
}
