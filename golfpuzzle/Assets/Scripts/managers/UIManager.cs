using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Security.Cryptography.X509Certificates;
using Assets.SimpleLocalization;
using common;
using consts;
using Highscores;
using TMPro;
using Tutorial;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI LevelCounter;
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI BestResultCounter;
    public TextMeshProUGUI SolutionLabel;
    
    public Button ResetButton;
    public Button NextLevelButton;
    public Button ToEditorButton;
    public Button ActivateTootlipModeButton;
    public WinLevelPanel winLevelPanel;

    public MessageWindow messageWindow;

    public Image LoadingImage;
    public TextMeshProUGUI TestLabel;
    
    public static UnityEvent UpdateGameUIEvent=new UnityEvent();
    public static UnityEvent UpdateMoveCounterEvent=new UnityEvent();
    

    [SerializeField]
    public GameObject prefabWinLevelPanel;

    public GameObject prefabMessageWindow;
    [SerializeField]
    public Canvas UICanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameUIEvent.AddListener(UpdateLevelCounter);
        UpdateGameUIEvent.AddListener(UpdateMoveCounter);
        UpdateGameUIEvent.AddListener(UpdateBestResultCounter);
        UpdateGameUIEvent.AddListener(UpdateSolutionCounter);
        
        UpdateMoveCounterEvent.AddListener(UpdateMoveCounter);
        ToEditorButton.onClick.AddListener(ToEditorButtonClick);
        ToEditorButton.gameObject.SetActive(GameManager.TestMap);
        NextLevelButton.onClick.AddListener(NextLevelButtonClick);
        ResetButton.onClick.AddListener(ResetButtonOnClick);
        
        GameObject winLevelPanelGO = GameObject.Instantiate(prefabWinLevelPanel, UICanvas.transform);
        winLevelPanel = winLevelPanelGO.GetComponent<WinLevelPanel>();
        winLevelPanelGO.SetActive(false);

        GameObject messageWindowGO = GameObject.Instantiate(prefabMessageWindow, UICanvas.transform);
        messageWindow = messageWindowGO.GetComponent<MessageWindow>();
        
        GameManager.WinLevelEvent.AddListener(messageWindow.Hide);
            //TutorialManager.ClearLastTutorialPointEvent.AddListener(messageWindow.Hide);
        TutorialManager.ShowTutorialMessageEvent.AddListener(messageWindow.ShowKey);
        
        
    }


    private void ToEditorButtonClick()
    {
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.editor);
    }

    private void ResetButtonOnClick()
    {
        GameManager.Instance.ResetScene();
    }

    private void NextLevelButtonClick()
    {
        GameManager.Instance.NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelCounter()
    {
        LevelCounter.text = LocalizationManager.Localize(StringConstants.LevelLabel) + ":" +
                                 GameManager.Instance.mLevelBuilder.GetCurrentLevel().levelNumber.ToString();
    }

    public void UpdateMoveCounter()
    {
        MoveCounter.text = LocalizationManager.Localize(StringConstants.MovesLabel) + ":" +
                                 GameManager.Instance.MoveCounter.ToString()+"/"+
                                 GameManager.Instance.mLevelBuilder.GetCurrentLevel().solveMoveNumber.ToString();
    }

    public void UpdateBestResultCounter()
    {

        int hsresult=HighscoreManager.GetInstance().GetLevelScore(GameManager.Instance.mLevelBuilder.GetCurrentLevel().levelNumber);

        string solution = GameManager.Instance.mLevelBuilder.GetCurrentLevel().solution;
        
        BestResultCounter.text = LocalizationManager.Localize(StringConstants.Bestmoveslabel) + ":" +
                                 hsresult.ToString()+" "+solution;
    }


    public void UpdateSolutionCounter()
    {
        string solution = HighscoreManager.GetInstance()
            .GetLevelSolution(GameManager.Instance.mLevelBuilder.GetCurrentLevel().levelNumber);
        SolutionLabel.text = solution;
        
    }

    public void ActivateTooltipModeButtonClick()
    {
        GameManager.Instance.ActivateTooltipMode();
        if (GameManager.TooltipModeActivated)
        {
            ActivateTootlipModeButton.image.sprite = GameManager.Instance.MImageDatabase.tooltipButtonImageActive;
        }
        else
        {
            ActivateTootlipModeButton.image.sprite = GameManager.Instance.MImageDatabase.tooltipButtonImageNormal;
        }
        
        
    }


    
    
}
