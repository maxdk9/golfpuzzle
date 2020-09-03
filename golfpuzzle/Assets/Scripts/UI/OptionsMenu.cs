using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using Preferences;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown LanguageDropdown;

    public Toggle MuteSfxToggle;
    public Toggle MuteMusicToggle;
    public Toggle DisableTutorialToggle;

    public static UnityEvent MuteSfxEvent=new UnityEvent();
    public static UnityEvent MuteMusicEvent=new UnityEvent();
    
    
    private Dictionary<string, int> languagesDictionary=new Dictionary<string, int>();
    
    
    void Start()
    {
        
        MuteMusicEvent.Invoke();
        MuteSfxEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        
        SetLanaguagesDictionary();
        SetCurrentValues();
        
        
    }

    private void SetCurrentValues()
    {
        MuteSfxToggle.isOn = GamePreferences.GetMuteSfx();
        MuteMusicToggle.isOn = GamePreferences.GetMuteMusic();
        DisableTutorialToggle.isOn = !GamePreferences.ShowTutorial();
        
        int languagevalue=0;
        if (languagesDictionary.TryGetValue(GamePreferences.GetLanguageString(), out languagevalue))
        {
            LanguageDropdown.value = languagevalue;
        }
    }


    public void OnChangeMuteSfx()
    {

        GamePreferences.SetMuteSfx(MuteSfxToggle.isOn);
        
        MuteSfxEvent.Invoke();
		
    }



    public void OnChangeDisableTutorial()
    {
        GamePreferences.SetDisableTutorial(DisableTutorialToggle.isOn);
    }

    public void OnChangeMuteMusic()
    {

        GamePreferences.SetMuteMusic(MuteMusicToggle.isOn);
        
        MuteMusicEvent.Invoke();
    }
    
    
    private void SetLanaguagesDictionary()
    {
        languagesDictionary.Clear();
        languagesDictionary.Add(SystemLanguage.English.ToString(),0);
        languagesDictionary.Add(SystemLanguage.Russian.ToString(),1);
        languagesDictionary.Add(SystemLanguage.Ukrainian.ToString(),2);
    }
    
    
    public void OnLanguageDropdownChanged()
    {
        int currentDropdownValue = LanguageDropdown.value;

        string languageString="English";
        foreach ( KeyValuePair<string,int> keyValuePair in languagesDictionary)
        {
            if (currentDropdownValue == keyValuePair.Value)
            {
                languageString = keyValuePair.Key;
                break;
            }
        }
        

        GamePreferences.SetLanguage(languageString);
        LocalizationManager.Language = languageString;
        
        
        

    }
    
    
    
}
