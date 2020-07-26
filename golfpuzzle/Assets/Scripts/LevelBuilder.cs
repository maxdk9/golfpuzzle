using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public int mCurrentLevel;
    public List<LevelElement> mLevelElements;
    private Level mLevel;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = mLevelElements.Find(le => le.mCharacter == c.ToString());
        if (levelElement != null)
        {
            return levelElement.mPrefab;
        }

        return null;
    }


    public void Build()
    {
        mLevel = GetComponent<Levels>().mLevels[mCurrentLevel];
        int startx = -mLevel.Width/2;
        int x = startx;
        int y = -mLevel.Height / 2;
        foreach (string row in mLevel.mRows)
        {
            foreach (char ch in row)
            {
                Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }

                x++;
            }

            y++;
            x = startx;
        }
        
    }

    public void NextLevel()
    {
        mCurrentLevel++;
        if (mCurrentLevel >= GetComponent<Levels>().mLevels.Count)
        {
            mCurrentLevel = 0;
        }
    }

}

[Serializable]
public class LevelElement
{
    public string mCharacter;
    public GameObject mPrefab;
    
}