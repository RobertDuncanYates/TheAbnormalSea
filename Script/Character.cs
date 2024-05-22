using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character //crewmate
{
    public GameObject Normal;//holds prefab of normal crewmate
    public GameObject[] Abnormals;//holds prefab of all abnormal varients 
    public GameObject SpawnedObject;//stores the currently spawned in crewmate
}
