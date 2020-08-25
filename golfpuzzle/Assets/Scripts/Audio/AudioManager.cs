using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    private bool firstMusicSourceIsPlaying;


    public static string MusicDisabled = "MusicDisabled";
    public static string SFXDisabled = "SFXDisabled";


    public void Initialization()
    {
        
    }
    
    
    private void Start()
    {
        this.gameObject.AddComponent<AudioEvents>();
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
        
        //   OptionsMenu.MuteMusicEvent.AddListener(MuteMusic);
        //OptionsMenu.MuteSfxEvent.AddListener(MuteSfx);
        
        
    }

    private void MuteSfx()
    {

        bool sfxDisabled = PlayerPrefs.GetInt(SFXDisabled) > 0;
        sfxSource.mute = sfxDisabled;
        
    }

    private void MuteMusic()
    {

        bool musicDisabled = PlayerPrefs.GetInt("MusicDisabled")>0;
        musicSource.mute = musicDisabled;
        musicSource2.mute = musicDisabled;
    }
    

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        
        activeSource.clip = musicClip;
        activeSource.Play();
        
    }

    


    public void PlayMusicIfSilence(AudioClip clip)
    {
        if (!IsSilence())
        {
            return;
        }
        PlayMusic(clip);
    }
    
    
    
    public void PlayMusicWithFade(AudioClip clip, float transitionTime=3.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        StartCoroutine(UpdateMusicWithFade(activeSource, clip, transitionTime));
    }


    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 3.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        

        float t = 0;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }
        original.Stop();
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }

        float t = 0;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();
        
        
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip,volume);
    }


    
    

    public bool IsSilence()
    {
        
        
        return (!((musicSource.isPlaying) || (musicSource2.isPlaying)));
    }
}
