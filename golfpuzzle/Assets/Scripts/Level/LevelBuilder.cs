using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public int mCurrentLevel;
    public List<ColorToPrefab> colorMappings;
    private Level mLevel;

    


    public void BuildCurrentLevel()
    {
        if (mLevel == null)
        {
            mLevel = GetComponent<Levels>().mLevels[mCurrentLevel];
        }
        Texture2D map = mLevel.map;
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
        
    }

    public void NextLevel()
    {
        mCurrentLevel++;
        if (mCurrentLevel >= GetComponent<Levels>().mLevels.Count)
        {
            mCurrentLevel = 0;
        }
        mLevel = GetComponent<Levels>().mLevels[mCurrentLevel];
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


    public void BuildTestMap()
    {
        Level level=new Level();
        level.mMapPath = LevelEditor.GetCurrentMapPath();
        byte[] pngBytes = System.IO.File.ReadAllBytes(level.mMapPath);
        Texture2D tex = new Texture2D(LevelEditor.GetSavedMapWidth(), LevelEditor.GetSavedMapHeight());
        tex.LoadImage(pngBytes);
        level.map = tex;
        level.bestMoves = 5;
        level.levelNumber = 1;
        level.solveMoveNumber = 5;
        level.width = level.map.width;
        level.height = level.map.height;


        mLevel = level;
        
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

