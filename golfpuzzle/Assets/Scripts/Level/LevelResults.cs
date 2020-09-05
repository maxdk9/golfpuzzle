
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using UnityEngine;

    public class LevelResults
    {
        public static string LevelResultListKey = "LevelResultListKey";

        
        public  static List<LevelResult> getLevelResults()
        {
            
            List<LevelResult> mResults=new List<LevelResult>();
            string levelResultString = PlayerPrefs.GetString(LevelResultListKey, "");

            if (!levelResultString.Equals(""))
            {
                mResults = JsonConvert.DeserializeObject<List<LevelResult>>(levelResultString);
            }
            return mResults;
        }


        public static int GetLastSolvedLevel()
        {
            int res = 0;
            List<LevelResult> mResults = getLevelResults();
            foreach (var VARIABLE in mResults)
            {
                if (VARIABLE.starNumber > 0&&res<VARIABLE.levelNumber)
                {
                    res = VARIABLE.levelNumber;
                }
            }
            return res;
        }
        
        
        
    }
