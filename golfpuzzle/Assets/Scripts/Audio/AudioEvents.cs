using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    public class AudioEvents : MonoBehaviour
    {
        public AudioClip music1;
        public AudioClip music2;
        public AudioClip sfxSwipe;
        public AudioClip sfxOpenDoor;
        public AudioClip sfxExplosion;
        private AudioClip sfxSand;
        public AudioClip sfxDocking;
        public AudioClip sfxButtonClick;
        
        
        public static UnityEvent commonButtonClickEvent=new UnityEvent();
        private bool silenceCoroutineStarted;
        private int silenceInterval = 60;
        private int lastPlayedMusicIndex=0;        
        private List<AudioClip> listForRandomPlaying=new List<AudioClip>();
        


        private void Start()
        {

            music1 = Resources.Load<AudioClip>("Sound/inspired");
            music2 = Resources.Load<AudioClip>("Sound/inspired"); 
            
            sfxSwipe=Resources.Load<AudioClip>("Sfx/swipe");
            sfxButtonClick=Resources.Load<AudioClip>("Sfx/uibutton");
            
            sfxOpenDoor=Resources.Load<AudioClip>("Sfx/opendoor");            
            sfxExplosion=Resources.Load<AudioClip>("Sfx/explosion");
            sfxSand=Resources.Load<AudioClip>("Sfx/sand");
            sfxDocking=Resources.Load<AudioClip>("Sfx/docking");
            
            
            
            
            
            
            Sand.EnterToSandEvent.AddListener((() =>
            {
                AudioManager.Instance.PlaySFX(sfxSand);
            }));
            
            Explosion.ExplosionEvent.AddListener((() =>
            {
                AudioManager.Instance.PlaySFX(sfxExplosion);
            }));
            
            Gate.OpenGateEvent.AddListener((() =>
            {
                AudioManager.Instance.PlaySFX(sfxOpenDoor);
            }));
            
            HoleTarget.DockBallInHoleEvent.AddListener((() =>
            {
                AudioManager.Instance.PlaySFX(sfxDocking);
            }));
            
            silenceCoroutineStarted = false;
            BallControl.BallMoveEvent.AddListener((() =>
            {
                AudioManager.Instance.PlaySFX(sfxSwipe);
            }));
        }

        private void Awake()
        {
            listForRandomPlaying.Add(music1);
            listForRandomPlaying.Add(music2);
            commonButtonClickEvent.AddListener(PlayCommonButtonClick);
        }

        

        


        public void PlayRandomMusicOnSilence()
        {
            if (AudioManager.Instance.IsSilence())
            {
                AudioManager.Instance.PlayMusicWithCrossFade(GetRandomTrack());    
            }
                
            
        }

        private AudioClip GetRandomTrack()
        {
            int clipindex = 0;
                
            while (true)
            {
                clipindex = UnityEngine.Random.Range(0, listForRandomPlaying.Count);
                if (clipindex != lastPlayedMusicIndex)
                {
                    break;
                }
            }

            lastPlayedMusicIndex = clipindex;
            
            return listForRandomPlaying[clipindex];
        }


        public void PlayMusicGameScreen()
        {
            AudioManager.Instance.PlayMusicIfSilence(music1);
        }

        private void Update()
        {
            

            if (!silenceCoroutineStarted)
            {
                silenceCoroutineStarted = true;
                StartCoroutine(SilenceCoroutine());
            }
        }

        private IEnumerator SilenceCoroutine()
        {
            yield return new WaitForSeconds(silenceInterval);
            PlayRandomMusicOnSilence();
            yield return new WaitForSeconds(1f);
            silenceCoroutineStarted = false;
        }

        public void PlayCommonButtonClick()
        {
            AudioManager.Instance.PlaySFX(sfxButtonClick);
        }
    }
}