using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public GameObject myobject;
    public SubtitleSystem SubtitleSys;
    public bool ActiveSound;
    public sound[] Sounds; //stores an array of music
    // Start is called before the first frame update
    void Start()
    {
        if(SubtitleSys == null) { SubtitleSys = FindObjectOfType<SubtitleSystem>(); }
        myobject = gameObject;
    }

    void Update()
    {
        
    }

    public void ActivateAudioManager()//unmutes audio manager when world is in play
    {
        ActiveSound=true;
        for(int i =0; i < Sounds.Length; i++)
        {
            if (Sounds[i].Loaded) { Sounds[i].source.volume = Sounds[i].volume; }
        }
    }

    private void request(string name)//requests an audio track to be loaded. This is for optimisation so the game doesnt load every audio track at the start
    {
        sound s = Array.Find(Sounds, sound => sound.name == name);
        if (!s.Loaded)//if not previous loaded in. Load the track
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = 0;
            if (ActiveSound) { s.source.volume = s.volume; }
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.spatialBlend = s.SpatialBlend;
            s.source.ignoreListenerPause= s.IgnorePause;
            if (s.SpatialBlend != 0)//if 3d audio source enter in the following details
            {
                s.source.rolloffMode = AudioRolloffMode.Linear;
                s.source.minDistance = s.MinDistance;
                s.source.maxDistance = s.MaxDistance;
                s.source.dopplerLevel = s.DopplerLevel;
                s.source.spread = s.Spread;
            }
            s.Loaded = true;
        }
    }
    

    public float Play(string name) //plays the sound file <name>. Returns Length of sound file to be used in timers
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Play();
        if (s.Subtitle.SubActive)
        {
            SubtitleSys.AddSubtitle(s.Subtitle, s.source.clip.length);
        }
        return (s.source.clip.length);
    }

    public void Stop(string name)//stops the sound file <name>
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public float PlayRandom() //play random sound Returns Length of sound file to be used in timers
    {
        var number = UnityEngine.Random.Range(0, Sounds.Length);
        return (Play(Sounds[number].name));
    }

    public string ReturnRandomTrackName() //returns name of a random track for the mainaudiocontrol
    {
        var number = UnityEngine.Random.Range(0, Sounds.Length);
        return (Sounds[number].name);
    }

    public void StopAll()//stops all sounds
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            try
            {
                sound s = Sounds[i];
                s.source.Stop();
            }
            catch { }
        }
    }
    public void SetVolume(string name, float volume)//sets the volume of sound file <name>
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.volume = volume;
    }
    public void SetVolumeAll(float Volume)//sets every sound files volume
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            try
            {
                sound s = Sounds[i];
                s.source.volume = Volume;
            }
            catch { }
        }
    }
    public void Pause(string name)//pauses the sound file <name>
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Pause();
    }
    public void SetPitch(string name, float pitch)//sets the pitch of sound file <name>
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        s.source.pitch = pitch;
    }
    public float GetLength(string name)//gets length of audio clip for timing purpose 
    {
        request(name);
        sound s = Array.Find(Sounds, sound => sound.name == name);
        return (s.source.clip.length);
    }
    public subtitle ReturnSubtitle(string name)//returns subtitle of track
    {
        sound s = Array.Find(Sounds, sound => sound.name == name);
        return (s.Subtitle);
    }
}
