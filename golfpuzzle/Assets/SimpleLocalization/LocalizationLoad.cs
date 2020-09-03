using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using Preferences;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationLoad : MonoBehaviour
{
    

    
    public void Awake()
    {

        string localizationString = GamePreferences.GetLanguageString();
        if (!localizationString.Equals(""))
        {
            LocalizationManager.Language = localizationString;
            LocalizationManager.Read();
        }
        else
        {
            LocalizationManager.Read();

            switch (Application.systemLanguage)
            {
            
                case SystemLanguage.Russian:
                    LocalizationManager.Language = "Russian";
                    break;
                default:
                    LocalizationManager.Language = "English";
                    break;
            }    
        }
            
    }

    /// <summary>
    /// Change localization at runtime
    /// </summary>
    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }

    
}
