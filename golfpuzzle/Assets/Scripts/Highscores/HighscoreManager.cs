using UnityEngine;

namespace Highscores
{
    public class HighscoreManager
    {

        public static string hsLevelPrefix = "hs_user_levelscore";
        private static HighscoreManager Instance;

        public static HighscoreManager GetInstance()
        {
            if (Instance == null)
            {
                Instance=new HighscoreManager();
            }

            return Instance;
        }


        public int GetLevelScore(int level)
        {
            int res=PlayerPrefs.GetInt(hsLevelPrefix + level.ToString(),0);
            return res;
        }


        public void SetLevelScore(int level, int score)
        {
            int oldScore = GetLevelScore(level);
            if (oldScore == 0)
            {
                PlayerPrefs.SetInt(hsLevelPrefix + level.ToString(),score);
            }
            if (oldScore > score)
            {
                PlayerPrefs.SetInt(hsLevelPrefix + level.ToString(),score);
            }
            
            
        }
    }
}