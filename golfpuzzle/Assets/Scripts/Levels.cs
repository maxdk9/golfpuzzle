using System;
using System.Collections.Generic;
using UnityEngine;


public class Levels : MonoBehaviour
{
    public string fileName;
    public List<Level> mLevels=new List<Level>();

    
    
    
    private void Awake()
    {
        TextAsset textAsset = (TextAsset) Resources.Load(fileName);
        if (!textAsset)
        {
            Debug.Log("Level file "+fileName+" not found");
            return;
        }

        else
        {
            Debug.Log("Levels imported");
        }

        string completeText = textAsset.text;

        
        string[] lines;
        lines = completeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
        mLevels.Add(new Level());
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                mLevels.Add(new Level());
                continue;
            }
            mLevels[mLevels.Count-1].mRows.Add(line);
        }

    }
}

[Serializable]
public class Level
{
    public List<String> mRows = new List<string>();

    public int Height
    {
        get { return mRows.Count; }
    }


    public int Width
    {
        get
        {
            int maxlength = 0;
            foreach (var VARIABLE in mRows)
            {
                if (VARIABLE.Length > maxlength)
                {
                    maxlength = VARIABLE.Length;
                }
            }

            return maxlength;
        }
    }
}
