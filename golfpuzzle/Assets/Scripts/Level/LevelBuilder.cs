using System;
using System.Collections;
using System.Collections.Generic;
using common;
using Tutorial;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour
{
    public int mCurrentLevel;
    public List<ColorToPrefab> colorMappings;
    private Level mLevel;
    public static IntEvent OnBuildLevelEvent=new IntEvent();


    
    
    public void BuildCurrentLevel()
    {
        
        if (mLevel == null)
        {
            if (DataHolder.ChosenLevelNumber > 0)
            {
                mCurrentLevel = DataHolder.ChosenLevelNumber;    
            }
            
            SetCurrentLevel();
        }
        Texture2D map = mLevel.map;
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
        OnBuildLevelEvent.Invoke(mLevel.levelNumber);
    }

    public void NextLevel()
    {
        mCurrentLevel++;
        if (mCurrentLevel >= GetComponent<Levels>().mLevels.Count)
        {
            mCurrentLevel = 0;
        }

        SetCurrentLevel();
    }

    private void SetCurrentLevel()
    {
        foreach (Level level in GetComponent<Levels>().mLevels)
        {
            if (level.levelNumber == mCurrentLevel)
            {
                mLevel = level;
                break;
            }
        }
    }


    public Level GetCurrentLevel()
    {
        return mLevel;
    }
    
    
  

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = mLevel.map.GetPixel(x, y);
        
        if (pixelColor.a == 0)
        {
            return;
        }
        
        foreach (ColorToPrefab colorToPrefab in colorMappings)
        {
            if (colorToPrefab.color.Equals(pixelColor))
            {
                Instantiate(colorToPrefab.prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
        
    }



    public ColorToPrefab GetColorToPrefab(Color c)
    {
        foreach (ColorToPrefab colorToPrefab in colorMappings)
        {
            if (colorToPrefab.color.Equals(c))
            {
                return colorToPrefab;
            }
        }

        return null;
    }

    public void BuildTestMap()
    {
        

        Texture2D tex;
        if (LevelEditor.GlobalCurrentLevel == null)
        {

            return;
        }
        

        
        mLevel = LevelEditor.GlobalCurrentLevel;
        
        Texture2D map = mLevel.map;
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
        
        
    }
}

