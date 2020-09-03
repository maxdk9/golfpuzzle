using Assets.SimpleLocalization;
using UnityEngine;

namespace Preferences
{
    public class GamePreferences
    {
        public static string disableTutorialPref = "disableTutorialPref";
        public static string muteMusic = "muteMusic";
        public static string muteSfx = "muteSfx";
        public static string languageString = "languageString";
        



        public static bool MuteMusic()
        {
            return PlayerPrefs.GetInt(muteMusic, 0) == 1;
        }
        
        public static bool MuteSFX()
        {
            return PlayerPrefs.GetInt(muteSfx, 0) == 1;
        }

        
        
        public static bool ShowTutorial()
        {
            return PlayerPrefs.GetInt(disableTutorialPref, 0) == 0;
        }

        public static void SetDisableTutorial(bool b)
        {
            
            PlayerPrefs.SetInt(disableTutorialPref,b ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static bool GetMuteMusic()
        {
            return PlayerPrefs.GetInt(muteMusic, 0) == 1;
        }
        
        public static bool GetMuteSfx()
        {
            return PlayerPrefs.GetInt(muteSfx, 0) == 1;
        }
        
        
        public static void SetMuteMusic(bool b)
        {
            PlayerPrefs.SetInt(muteMusic,b ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void SetMuteSfx(bool b)
        {
            PlayerPrefs.SetInt(muteSfx,b ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void SetLanguage(string s)
        {
            PlayerPrefs.SetString(languageString,s);
            
            PlayerPrefs.Save();

            if (!s.Equals(""))
            {
                LocalizationManager.Language = s;
                LocalizationManager.Read();    
            }
            
        }

        public static string GetLanguageString()
        {
            string res = PlayerPrefs.GetString(languageString, "");
            return res;
        }

        
    }
}