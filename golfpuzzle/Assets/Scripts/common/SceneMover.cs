using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace common
{
    public class SceneMover : Singleton<SceneMover>
    {
        private enumScreen currentScreen;

        public void SetCurrentScreen(enumScreen s)
        {
            currentScreen = s;
            switch (currentScreen)
            {
                case enumScreen.editor:
                    
                    this.StartCoroutine(LoadLevelEditorRoutine());
                    
                    break;
                case enumScreen.game:
                    GameManager.TestMap = false;
                    
                    this.StartCoroutine(LoadSampleSceneRoutine());    
                    
                    break;
                case enumScreen.gametesmap:
                    GameManager.TestMap = true;
                    this.StartCoroutine(LoadSampleSceneRoutine());
                    break;
                case enumScreen.mainmenu:
                    SceneManager.LoadSceneAsync("MainMenuScene");
                    break;
                case enumScreen.chooselevel:
                    SceneManager.LoadSceneAsync("ChooseLevelScene");
                    break;
            }
        }

        private IEnumerator LoadLevelEditorRoutine()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LevelEditor");
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
            
        }

        private IEnumerator LoadSampleSceneRoutine()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            GameManager.Instance.LoadLevelScene();

        }


        public enum enumScreen
        {
            mainmenu,
            editor,
            game,
            gametesmap,
            chooselevel
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (currentScreen == enumScreen.mainmenu)
                {
                    Application.Quit();
                }
                else
                {
                    SetCurrentScreen(enumScreen.mainmenu);
                }
            }
        }
    }
}