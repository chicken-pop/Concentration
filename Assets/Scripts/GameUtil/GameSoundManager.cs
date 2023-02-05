using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : SingletonMonoBehaviour<GameSoundManager>
{
    private AudioSource BGMAudioSource;

    private AudioSource[] SEAudioSources = new AudioSource[3];

    private List<AudioClip> BGMAudioClips = new List<AudioClip>();

    private List<AudioClip> SEAudioClips = new List<AudioClip>();

    public enum BGMTypes
    {
        invalide = -1,
        GameStart,
        GameMain,
        GameResult,
    }

    public BGMTypes BGMType;

    public enum SETypes
    {
        Invalid = -1,
        CardOpen,
        CardClone,
    }

    public SETypes SEType;

    public void Initualze()
    {
        BGMAudioSource = this.gameObject.AddComponent<AudioSource>();
        for(int i= 0; i < SEAudioSources.Length; i++)
        {
            SEAudioSources[i] = this.gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetBGMAudioClips(AudioClip bgmAudio)
    {
        BGMAudioClips.Add(bgmAudio);
    }
    public void SetSEAudioClips(AudioClip seAudio)
    {
        SEAudioClips.Add(seAudio);
    } 

    public void PlayBGM(BGMTypes BGMType)
    {
        if(BGMAudioClips[(int)BGMType] == null)
        {
            return;
        }
        BGMAudioSource.clip = BGMAudioClips[(int)BGMType];
        BGMAudioSource.Play();
    }

    public void PlaySE(SETypes seType)
    {
        if(SEAudioClips[(int)SEType] == null)
        {
            return;
        }
        foreach(var seAudio in SEAudioSources)
        {
            if (!seAudio.isPlaying)
            {
                seAudio.PlayOneShot(SEAudioClips[(int)seType]);
                seAudio.Play();
                break;
            }
        }
    }
}
