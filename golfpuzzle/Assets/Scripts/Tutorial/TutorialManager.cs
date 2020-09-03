using System;
using System.Collections.Generic;
using DG.Tweening;
using Preferences;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        public TutorialPointData [] tutorialPopups;
     

        public Dictionary<string, UnityEvent> TutorialEventDictionary=new Dictionary<string, UnityEvent>();
        private static int eventCount = 20;
        
        public static UnityEvent ClearLastTutorialPointEvent=new UnityEvent();
        public static StringEvent ShowTutorialMessageEvent=new StringEvent();
        
        
        private void Awake()
        {
            LevelBuilder.OnBuildLevelEvent.AddListener(GenerateTutorialPoints);
            GenerateTutorialEvents();
            ClearLastTutorialPointEvent.AddListener(ClearLastTutorialPoint);
            

        }

        private void GenerateTutorialEvents()
        {
            
            List<object>  teklist=new List<object>();
            TutorialEventDictionary.Clear();
            for (int i = 0; i < eventCount; i++)
            {
                string eventkey = "Tutorial" + i;
                UnityEvent tekevent=new UnityEvent();
                tekevent.AddListener(() =>
                {
                    this.GetType().GetMethod(eventkey).Invoke(this, teklist.ToArray());
                });
                
                TutorialEventDictionary.Add(eventkey,tekevent);
            }
        }

        public void GenerateTutorialPoints(int levelNumber)
        {
            if (!GamePreferences.ShowTutorial())
            {
                return;
            }

            foreach (TutorialPointData tpdata in tutorialPopups)
            {
                if (tpdata.levelNumber != levelNumber)
                {
                    continue;
                }
                GameObject tutorialPointGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialPointPrefab,
                    new Vector3(tpdata.position.x, tpdata.position.y, 0), Quaternion.identity);
                TutorialPoint tutorialPoint = tutorialPointGO.GetComponent<TutorialPoint>();
                tutorialPoint.SetData(tpdata);
            }            
        }


        

        public void ActivateEventByName(string tutorialEventName, string dataMessageKey)
        {

            ClearLastTutorialPointEvent.Invoke();
            ShowTutorialMessageEvent.Invoke(dataMessageKey);
            TutorialEventDictionary[tutorialEventName]?.Invoke();
        }

        private void ClearLastTutorialPoint()
        {
            
            ScaledChosenComponent.RemoveAll();
            TutorialMonobeh.RemoveAll();
            
        }




        #region TUTORIAL_NUMBER_EVENTS

        
        
        
        
        public void Tutorial18()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,90));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        public void Tutorial17()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,180));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        public void Tutorial16()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        
        
        public void Tutorial15()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        public void Tutorial14()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,90));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        public void Tutorial13()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        public void Tutorial12()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 3), Quaternion.Euler(0,0,270));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        
        public void Tutorial11()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(7, 1), Quaternion.Euler(0,0,270));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }

                
        public void Tutorial10()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(7, 1), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        
        public void Tutorial9()
        {
         
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(7, 1), Quaternion.Euler(0,0,90));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }
        
        
        
        public void Tutorial8()
        {
           
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(7, 1), Quaternion.Euler(0,0,180));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
            
         
        }

        
        public void Tutorial7()
        {
            
           
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(7, 1), Quaternion.Euler(0,0,90));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }

        

        public void Tutorial6()
        {
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 4), Quaternion.Euler(0,0,270));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }
        
        
        public void Tutorial5()
        {
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 4), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }
        
        
        public void Tutorial4()
        {
            MetalBlock [] balls=FindObjectsOfType<MetalBlock>();
            foreach (MetalBlock ball in balls)
            {
                ball.gameObject.AddComponent<ScaledChosenComponent>();
            }
            
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(3, 4), Quaternion.Euler(0,0,0));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }
        
        
        public void Tutorial2()
        {
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(2, 3), Quaternion.Euler(0,0,270));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }

        public void Tutorial3()
        {
            HoleTarget [] balls=FindObjectsOfType<HoleTarget>();
            foreach (HoleTarget ball in balls)
            {
                ball.gameObject.AddComponent<ScaledChosenComponent>();
            }
            
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(2, 3), Quaternion.Euler(0,0,180));
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();
        }
        
        
        public void Tutorial1()
        {

            Ball [] balls=FindObjectsOfType<Ball>();
            foreach (Ball ball in balls)
            {
                ball.gameObject.AddComponent<ScaledChosenComponent>();
            }
            Debug.Log("Tutorial 1 event invoked");
            GameObject tutorialArrowGO = Instantiate(GameManager.Instance.mPrefabDatabase.TutorialArrowPrefab,
                new Vector3(2, 3), Quaternion.identity);
            tutorialArrowGO.AddComponent<ScaledChosenComponent>();

        }

        #endregion
        
        
        
    }
    
    
    
}