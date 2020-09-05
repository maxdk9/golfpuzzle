using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;


public class LevelViewManager : MonoBehaviour
    {
        public List<LevelView> mLevelViewList;
        [SerializeField] public GameObject LevelViewPrefab;
        private RectTransform ScrollRectTransform;
        private ScrollRect mScrollRect;
        private RectTransform mContent;
        
        
        

        
        
        [SerializeField]
        private Levels levels;
        
        
        
        private void Awake()
        {
            ScrollRectTransform = GetComponent<RectTransform>();
            mScrollRect = GetComponent<ScrollRect>();
            mContent = mScrollRect.content;    
        }


        


        private void Start()
        {
            levels = FindObjectOfType<Levels>();
            int lastLevelSolved = levels.GetLastLevelSolved();

            RectTransform focusLevelTransform=null;


            int focusIndex = 0;
            
            foreach (LevelResult result in levels.mResults)
            {
                GameObject LevelViewGO = Instantiate(LevelViewPrefab, mContent);
                LevelView levelView = LevelViewGO.GetComponent<LevelView>();
                if (result.levelNumber <= lastLevelSolved + 1)
                {
                    levelView.LevelEnabled = true;
                    focusIndex++;
                    focusLevelTransform = LevelViewGO.GetComponent<RectTransform>();
                }
                else
                {
                    levelView.LevelEnabled = false;
                }
                levelView.SetResult(result);   
            }

            if (focusLevelTransform != null)
            {
                
                mScrollRect.verticalNormalizedPosition = 1-((float) (focusIndex/4) / (float) (levels.mResults.Count/4));


                //MoveToTarget(focusLevelTransform);
            }
        }


        public void MoveToTarget(RectTransform target)
        {
            
            mScrollRect.verticalNormalizedPosition = .5f;// itemY / difference;
        }
    }
