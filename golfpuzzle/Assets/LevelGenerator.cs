using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public List<ColorToPrefab> colorMappings;
    private void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
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
}
