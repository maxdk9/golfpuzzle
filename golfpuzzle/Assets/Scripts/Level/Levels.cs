using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


public class Levels : MonoBehaviour
{
    public string fileName;
    public List<Level> mLevels=new List<Level>();
    
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
