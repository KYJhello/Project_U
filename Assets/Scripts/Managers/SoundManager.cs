using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float VolumeSFX = 1f;
    public float VolumeBGM = 1f;

    public Sound[] effetSounds;
    public Sound[] bgmSounds;

    public AudioSource audiosourcePlayer;
    public AudioSource[] audioSourceEffects;

    public string playSoundName;


}
