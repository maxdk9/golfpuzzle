using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Highscores;
using TMPro;
using UnityEditor;
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

        public GameObject CheckPurchaseGroup;
        public Slider CheckPurchaseSlider;
        
        
        

        
        
        [SerializeField]
        private Levels levels;
        
        
        
        private void Awake()
        {
            ScrollRectTransform = GetComponent<RectTransform>();
            mScrollRect = GetComponent<ScrollRect>();
            mContent = mScrollRect.content;    
        }


        private void Update()
        {
             
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


            CheckPurchaseGroup.SetActive(true);
            CheckPurchaseSlider.value = 0;
            StartCoroutine(ShowCheckPurchaseRoutine());

           // PrintLevelSolution();


        }

       

        private IEnumerator ShowCheckPurchaseRoutine()
        {
            float fillSpeed = .03f;
            float cycleDuration = .3f;
            
            while (!EasyMobileManager.Instance.ProductChecked)
            {
                yield return new WaitForSeconds(cycleDuration);
                CheckPurchaseSlider.value += fillSpeed;
            }   
            CheckPurchaseSlider.value = 1;
            yield return new WaitForSeconds(.3f);
           CheckPurchaseGroup.SetActive(false);
       
        }

       
    }
