
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class sound 
{
    public string name;
    public subtitle Subtitle;//subtitle of clip. leave blank if subtitle not required
    public AudioClip clip;//Audio Source
    [Range(0f, 1f)]
    public float volume;//Volume
    [Range(.1f, 3f)]
    public float pitch;//Pitch

    [HideInInspector]
    public AudioSource source;
    public bool loop; //if sound loops
    public bool playOnAwake; //if sound plays at when object is ativated
    [Range(0f, 1f)]
    public float SpatialBlend; //if sound is 2d or 3d 0 for 2d 1 for 3d
    [Range(0f, 5f)]
    public float DopplerLevel;
    [Range(0f, 360f)]
    public float Spread;
    public float MinDistance;
    public float MaxDistance;
    public bool IgnorePause; //if the sound will carry on playing even if the AudioListener is Paused (Usful for making pause menus)
    [HideInInspector]
    public bool Loaded; //used by audio manager to tell if AudioSource  has already been loaded.

    


}
