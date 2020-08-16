using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip mainTheme;
    
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        musicSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
        PlayMusic(mainTheme);
    }

    
    public void PlayMusic(AudioClip musicClip)
    {

        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.clip = musicClip;
        musicSource.Play();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
