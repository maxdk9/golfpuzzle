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
    [SerializeField] public PrefabDatabase mPrefabDatabase;
    [SerializeField] public ImageDatabase MImageDatabase;
    
    
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
    public static UnityEvent KeyActivatedEvent=new UnityEvent();
    public static UnityEvent OpenAllGateEvent=new UnityEvent();
    public static UnityEvent CloseAllGateEvent=new UnityEvent();
    public static bool TooltipModeActivated;
    


    private void Start()
    {
    
        KeyActivatedEvent.AddListener(CheckAllKeyActivation);
        CheckWinLevelEvent.AddListener(CheckWinLevel);
        WinLevelEvent.AddListener(WinLevel);
        OpenAllGateEvent.AddListener(OpenAllGame);
        CloseAllGateEvent.AddListener(CloseAllGate);
        AudioManager.Instance.Initialization();
    
        
        SetGameObjects();
    }

    

    private void CloseAllGate()
    {
        Gate[] gates = FindObjectsOfType<Gate>();
        foreach (Gate gate in gates)
        {
            if (gate.mOpen)
            {
                gate.Close();    
            }
            
        }
    }

    private void OpenAllGame()
    {
        Gate[] gates = FindObjectsOfType<Gate>();
        foreach (Gate gate in gates)
        {
            if (!gate.mOpen)
            {
                gate.Open();
            }
        }
    }

    private void CheckAllKeyActivation()
    {
        bool allKeysActivated = true;
        Key [] keys=FindObjectsOfType<Key>();
        foreach (Key key in keys)
        {
            if (!key.activated)
            {
                allKeysActivated = false;
            }
        }

        if (allKeysActivated)
        {
            OpenAllGateEvent.Invoke();
        }
        else
        {
            CloseAllGateEvent.Invoke();
        }
        
    }

    private void WinLevel()
    {
        if (TestMap)
        {
            
                uiManager.TestLabel.text = "Solution="+this.ballControl.SolutionSave;
            
        }
        else
        {
            mReadyForInput = false;

            string solution = this.ballControl.SolutionSave;
            int starNumber = GetWinLevelScoreStar();
            int currentLevelNumber = mLevelBuilder.GetCurrentLevel().levelNumber;
            LevelResult levelResult=new LevelResult();
            levelResult.levelNumber = currentLevelNumber;
            levelResult.starNumber = starNumber;
            levelResult.playerSolution = solution;
            
            
            HighscoreManager.GetInstance().SetLevelScore(currentLevelNumber,MoveCounter);
            HighscoreManager.GetInstance().SetLevelSolution(currentLevelNumber,solution);
            
            mLevelBuilder.GetComponent<Levels>().AddResult(levelResult);
            uiManager.winLevelPanel.Show(starNumber);
            Debug.Log("Level win");
            
            
        }
        
    }

    private int GetWinLevelScoreStar()
    {
        int tekresult =  mLevelBuilder.GetCurrentLevel().solveMoveNumber-MoveCounter;
        
        if (tekresult >= 0)
        {
            return 3;
        }
        else
        {
            if (tekresult == -1)
            {
                return 2;
            }
        }
        return 1;
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
        float borderSize = .01f;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        int width = mLevelBuilder.GetCurrentLevel().width;
        int height = mLevelBuilder.GetCurrentLevel().height;
        float aspectRatio = (float) Screen.width / (float) Screen.height;
        
        
        
        
        float verticalSize = ((float) width / 2f + (float) borderSize) / aspectRatio;;
        float horizontalSize = ((float) height / 2f + (float) borderSize) / aspectRatio;
            
        MainCamera.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;


        if (MainCamera.orthographicSize > 10)
        {
            MainCamera.orthographicSize = MainCamera.orthographicSize - .5f;    
        }
        
        MainCamera.transform.position = new Vector3(borderSize+(float)(width-1)/2.0f, (float) (MainCamera.orthographicSize) / 2.0f, -10);
        //ballControl.boxCollider2d.transform.position=new Vector3(MainCamera.transform.position.x,MainCamera.transform.position.y,0);
        

    }




    public void NextLevel()
    {
        //uiManager.NextLevelButton.gameObject.active = false;
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
        ballControl.SolutionSave = "";

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

    public void ResetBalls()
    {
        SetBalls();
        foreach (var VARIABLE in Balls)
        
        {
            VARIABLE.ResetBall();    
        }
    }

    public void ActivateTooltipMode()
    {
        TooltipModeActivated = !TooltipModeActivated;
        
        // if (TooltipModeActivated)
        // {
        //     ballControl.boxCollider2d.enabled = false;
        // }
        // else
        // {
        //     ballControl.boxCollider2d.enabled = true;
        // }
                
        
    }
    
    
}
