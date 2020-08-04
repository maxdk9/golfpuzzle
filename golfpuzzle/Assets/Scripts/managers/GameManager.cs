using System;
using System.Collections;
using System.Collections.Generic;
using Highscores;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public static bool TestMap = false;
    
    
    [SerializeField] public bool mReadyForInput;
    
    public LevelBuilder mLevelBuilder;
    public iBall[] Balls;
    public int MoveCounter;
    public Camera MainCamera;
    
    public HoleTarget[] Holes;

    private BallControl ballControl;
    public UIManager uiManager;
    private bool LevelSolved = false;
    public static UnityEvent WinLevelEvent=new UnityEvent();
    public static UnityEvent CheckWinLevelEvent=new UnityEvent();
    
    
    private void Start()
    {
    
        
        CheckWinLevelEvent.AddListener(CheckWinLevel);
        WinLevelEvent.AddListener(WinLevel);
    }

    private void WinLevel()
    {
        mReadyForInput = false;
        HighscoreManager.GetInstance().SetLevelScore(mLevelBuilder.GetCurrentLevel().levelNumber,MoveCounter);
        //UIManager.Instance.NextLevelButton.gameObject.active = true;
        
                
        uiManager.winLevelPanel.Show(GetWinLevelScoreStar());
        Debug.Log("Level win");
    }

    private int GetWinLevelScoreStar()
    {
        int tekresult = MoveCounter - mLevelBuilder.GetCurrentLevel().solveMoveNumber;
        if (tekresult <= 2)
        {
            return 3-tekresult;
        }


        return 0;
    }

    private void CheckWinLevel()
    {
        bool res = true;
        Holes = FindObjectsOfType<HoleTarget>();
        
        foreach (HoleTarget hole in Holes)
        {
            if (!hole.mBusy)
            {
                res = false;
            }
        }

        if (res)
        {
            SetLevelSolved(true);    
        }
        
    }

    private void Update()
    {
        // if (!mReadyForInput&&!LevelSolved)
        // {
        //     CheckBallStop();    
        // }
        
    }

    private IEnumerator LoadLevelSceneRoutine()
    {
        
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);;
        while (!asyncOperation.isDone)
        {
            yield return null;
        }    
        
        yield return new WaitForSeconds(.1f);
        Scene levelScene = SceneManager.GetSceneByName("LevelScene");
        SceneManager.SetActiveScene(levelScene);


        if (GameManager.TestMap)
        {
            mLevelBuilder.BuildTestMap();
        }
        else
        {
            mLevelBuilder.BuildCurrentLevel();    
        }
        
        StartNewLevel();
        
        
    }

    private void StartNewLevel()
    {
        SetMainCamera();
        MoveCounter = 0;
        SetBalls();
        
        foreach (iBall ball in Balls)
        {
            //ball.ballStopped.AddListener(CheckBallStop);
        }
        
        SetLevelSolved(false);
        mReadyForInput = true;
        this.StopAllCoroutines();
        this.StartCoroutine(CheckBallStopRoutine());
        UIManager.UpdateGameUIEvent.Invoke();
        
    }

    private IEnumerator CheckBallStopRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.4f);
            if (!mReadyForInput&&!LevelSolved)
            {
                CheckBallStop();    
            }    
        }
        
        
    }

    private void SetMainCamera()
    {
        float borderSize = .2f;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        int width = mLevelBuilder.GetCurrentLevel().width;
        int height = mLevelBuilder.GetCurrentLevel().height;
        float aspectRatio = (float) Screen.width / (float) Screen.height;
        
        
        
        
        float verticalSize = ((float) width / 2f + (float) borderSize) / aspectRatio;;
        float horizontalSize = ((float) height / 2f + (float) borderSize) / aspectRatio;
            
        MainCamera.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
        MainCamera.transform.position = new Vector3(borderSize+(float)(width-1)/2.0f, (float) (MainCamera.orthographicSize) / 2.0f, -10);
        ballControl.boxCollider2d.transform.position=new Vector3(MainCamera.transform.position.x,MainCamera.transform.position.y,0);
        

    }




    public void NextLevel()
    {
        uiManager.NextLevelButton.gameObject.active = false;
        mLevelBuilder.NextLevel();
        StartCoroutine(ResetSceneAsync());
    }

    public void SetBalls()
    {
        Balls = FindObjectsOfType<iBall>();
    }

    public void CheckBallStop()
    {
        SetBalls();
        bool allBallStopped = true;
        foreach (var VARIABLE in Balls)
        {
            if (VARIABLE.moved)
            {
                allBallStopped = false;
                break;
            }
        }
        if (allBallStopped)
        {
            mReadyForInput = true;
        }
        CheckWinLevelEvent.Invoke();
    }


    private IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncOperation1 = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncOperation1.isDone)
        {
            yield return null;

        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));

        mLevelBuilder.BuildCurrentLevel();
        StartNewLevel();
       
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());
    }
    
    private void SetLevelSolved(bool b)
    {
        LevelSolved = b;
        if (LevelSolved)
        {
            WinLevelEvent.Invoke();
        }
    }

    public void LoadLevelScene()
    {

        SetGameObjects();
        mReadyForInput = true;
        StartCoroutine(LoadLevelSceneRoutine());        
    }

    private void SetGameObjects()
    {
        mLevelBuilder = FindObjectOfType<LevelBuilder>();
        uiManager = FindObjectOfType<UIManager>();
        ballControl = FindObjectOfType<BallControl>();
    }
}
