using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Highscores
{
    public class GameScore
    {
        public static int GetCurrentGameResult(int lastLevelNumber)
        {
            int res = 0;
            List<LevelResult> mResults=new List<LevelResult>();
            List<Level> levels = Levels.GetLevels();
            
            
            
            string levelResultString = PlayerPrefs.GetString(Levels.LevelResultListKey, "");

            if (levelResultString.Equals(""))
            {
                return 0;

            }
            else
            {
                mResults = JsonConvert.DeserializeObject<List<LevelResult>>(levelResultString);
            }

            foreach (LevelResult result in mResults)
            {

                if (result.levelNumber > lastLevelNumber)
                {
                    break;
                }
                
                Level level = GetLevelByNumber(levels, result.levelNumber);
                res += GetLevelResult(result,level);
            }

            return res;

        }

        private static Level GetLevelByNumber(List<Level> levels, int levelnumber)
        {
            foreach (var VARIABLE in levels)
            {
                if (VARIABLE.levelNumber == levelnumber)
                {
                    return VARIABLE;
                }
            }

            return null;
        }

        private static int GetLevelResult(LevelResult result,Level level)
        {
            if (result.starNumber == 0)
            {
                return 0;
            }
            float tekres = ((float) result.levelNumber / 5.0f + (float) level.solveMoveNumber) /
                           ((float) result.starNumber / 3.0f);

            return (int) tekres;


        }
    }
}