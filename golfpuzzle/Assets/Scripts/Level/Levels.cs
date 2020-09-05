using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


public class Levels : MonoBehaviour
{
    public string fileName;
    public List<Level> mLevels=new List<Level>();
    public List<LevelResult> mResults=new List<LevelResult>();
    public static string LevelResultListKey = "LevelResultListKey";



    
    
    
    
    public void LoadLevels()
    {
        String filename = "cards1.json";
        string filePath = GetStreamingAssetsFilePath.GetPath(filename);
        string dataAsJson="";
        if (File.Exists(filePath))
        {
            dataAsJson = File.ReadAllText(filePath);
            mLevels = JsonConvert.DeserializeObject<List<Level>>(dataAsJson,
                new Newtonsoft.Json.Converters.StringEnumConverter());
        }
        else
        {
            Debug.Log(filename + "No file exist");
        }
        
        
        foreach (var VARIABLE in mLevels)
        {
            VARIABLE.Init();
         
        }


        
    }
    
    
    private void Awake()
    {
        LoadLevels();
        LoadLevelsResult();
    }

    private void LoadLevelsResult()
    {
        string levelResultString = PlayerPrefs.GetString(LevelResultListKey, "");

        if (levelResultString.Equals(""))
        {
            foreach (Level l in mLevels)
            {
                LevelResult levelResult=new LevelResult();
                levelResult.levelNumber = l.levelNumber;
                levelResult.starNumber = 0;
                levelResult.playerSolution = "";        
                mResults.Add(levelResult);
            }

            string levelResultNewString = JsonConvert.SerializeObject(mResults);
            PlayerPrefs.SetString(LevelResultListKey,levelResultNewString);
            PlayerPrefs.Save();

        }
        else
        {
            mResults = JsonConvert.DeserializeObject<List<LevelResult>>(levelResultString);
        }

        AddNewResults();


    }

    private void AddNewResults()
    {
        if (! (mLevels.Count>mResults.Count))
        {
            return;
        }
        
        foreach (Level level in mLevels)
        {
            bool resultFinded = false;
            
            
            foreach (LevelResult result in mResults)
            {
                if (result.levelNumber == level.levelNumber)
                {
                    resultFinded = true;
                    break;
                }
            }

            if (!resultFinded)
            {
                LevelResult levelResult=new LevelResult();
                levelResult.levelNumber = level.levelNumber;
                levelResult.starNumber = 0;
                levelResult.playerSolution = "";        
                mResults.Add(levelResult);
            }
            
            
            
        }
        string levelResultNewString = JsonConvert.SerializeObject(mResults);
        PlayerPrefs.SetString(LevelResultListKey,levelResultNewString);
        PlayerPrefs.Save();
        
        
        
    }


    public void AddResult(LevelResult result)
    {

        bool entryFound = false;
        for (int i=0;i<mResults.Count;i++)
        {

            LevelResult tekresult = mResults[i];
            
            
            if (result.levelNumber == tekresult.levelNumber)
            {
                entryFound = true;
                if (result.starNumber > tekresult.starNumber)
                {
                    int tekresultIndex = mResults.IndexOf(tekresult);
                    mResults[tekresultIndex] = result;
        
                }
            }
        }

        if (!entryFound)
        {
            mResults.Add(result);    
        }
        string levelResultNewString = JsonConvert.SerializeObject(mResults);
        PlayerPrefs.SetString(LevelResultListKey,levelResultNewString);
        PlayerPrefs.Save();
        
        
        
        
    }


    public int GetLastLevelSolved()
    {
        int res = 0;
        foreach (var VARIABLE in mResults)
        {
            if (VARIABLE.starNumber > 0&& VARIABLE.levelNumber>res)
            {
                res = VARIABLE.levelNumber;
            }
        }

        return res;
    }
}

[System.Serializable]
public class Level
{
    public int levelNumber;
    public int solveMoveNumber;
    public string mMapPath;
    public int width;
    public int height;
    public string solution;
    public string []  colorArray;
    
    [NonSerialized]
    public Texture2D map;
    
    
    public void Init()
    {
        map = Resources.Load<Texture2D>(mMapPath);
        width = map.width;
        height = map.height;
    }


    public void SetColorArray()
    {
        colorArray = new string [width * height];
        int m = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color c = map.GetPixel(i, j);
                string s=ColorUtility.ToHtmlStringRGBA(c);
                colorArray[m] = "#"+s;
                m++;
            }
        }
    }

    public void SetTextureFromColorArray()
    {
        map=new Texture2D(width,height);
        
        int m = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                string colorString = colorArray[m];
                Color color;
                ColorUtility.TryParseHtmlString(colorString, out color);
                map.SetPixel(i,j,color);
                m++;
            }
        }
    }
    
    
}


[System.Serializable]
public class LevelResult
{
    public int levelNumber;
    public string playerSolution;
    public int starNumber;
    
}


public class GetStreamingAssetsFilePath
{

    public static String GetPath(String filename)
    {
        string dbPath = "";
        string realPath="";
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string oriPath = System.IO.Path.Combine(Application.streamingAssetsPath, filename);

            // Android only use WWW to read file
            WWW reader = new WWW(oriPath);
            while (!reader.isDone)
            {
            }

            realPath = Application.persistentDataPath + "/db";
            System.IO.File.WriteAllBytes(realPath, reader.bytes);

            dbPath = realPath;
        }
        else
        {
            dbPath =  Application.dataPath+"/StreamingAssets/" + filename;
        }

        return dbPath;
    }
    
}
