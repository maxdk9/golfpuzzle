
using Assets.SimpleLocalization;
using common;
using consts;
using Highscores;
using TMPro;
using Tutorial;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI LevelCounter;
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI BestResultCounter;
    public TextMeshProUGUI SolutionLabel;
    public TextMeshProUGUI SolutionLabel2;
    
    public Button ResetButton;
    public Button NextLevelButton;
    public Button ToEditorButton;
    public Button ActivateTootlipModeButton;
    public Button ToMainMenuButton;
    
    public WinLevelPanel winLevelPanel;

    public TutorialMesWindow tutorialMessageWindow;
    public MilestoneWindow milestoneWindow;
    
    
    public BuyInappWindow buyInappWindow;
    

    public Image LoadingImage;
    public TextMeshProUGUI TestLabel;
    
    public static UnityEvent UpdateGameUIEvent=new UnityEvent();
    public static UnityEvent UpdateMoveCounterEvent=new UnityEvent();
    

    [SerializeField]
    public GameObject prefabWinLevelPanel;

    [SerializeField] public GameObject prefabMilestoneWindow;
    [SerializeField] public GameObject prefabBuyInappWindow;

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
        UpdateGameUIEvent.AddListener(UpdateSolutionLabel2);
        
        
        ToMainMenuButton.onClick.AddListener(ToMainMenuButtonClick);
        
        
        UpdateMoveCounterEvent.AddListener(UpdateMoveCounter);
        ToEditorButton.onClick.AddListener(ToEditorButtonClick);
        ToEditorButton.gameObject.SetActive(GameManager.TestMap);
        NextLevelButton.onClick.AddListener(NextLevelButtonClick);
        ResetButton.onClick.AddListener(ResetButtonOnClick);
        
        GameObject winLevelPanelGO = GameObject.Instantiate(prefabWinLevelPanel, UICanvas.transform);
        winLevelPanel = winLevelPanelGO.GetComponent<WinLevelPanel>();
        winLevelPanelGO.SetActive(false);

        GameObject messageWindowGO = GameObject.Instantiate(prefabMessageWindow, UICanvas.transform);
        tutorialMessageWindow = messageWindowGO.GetComponent<TutorialMesWindow>();



        GameObject buyInappWindowGO = GameObject.Instantiate(prefabBuyInappWindow, UICanvas.transform);
        buyInappWindow = buyInappWindowGO.GetComponent<BuyInappWindow>();
        buyInappWindowGO.transform.position=new Vector3(2000,0,0);
        
        
        GameObject milestonWindowGO = GameObject.Instantiate(prefabMilestoneWindow, UICanvas.transform);
        milestoneWindow = milestonWindowGO.GetComponent<MilestoneWindow>();
        milestonWindowGO.transform.position=new Vector3(2000,0,0);
        
        
        
        GameManager.WinLevelEvent.AddListener(tutorialMessageWindow.Hide);
            //TutorialManager.ClearLastTutorialPointEvent.AddListener(messageWindow.Hide);
        TutorialManager.ShowTutorialMessageEvent.AddListener(tutorialMessageWindow.ShowKey);
        
        
    }

    private void ToMainMenuButtonClick()
    {
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.mainmenu);
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
        if (LevelBuilder.MilestoneLevel())
        {
            milestoneWindow.Show();
        }
        else
        {
            GameManager.Instance.NextLevel();
        }        
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

        
        
        BestResultCounter.text = LocalizationManager.Localize(StringConstants.Bestmoveslabel) + ":" +
                                 hsresult.ToString();
    }

    public void UpdateSolutionLabel2()
    {
        string solution = GameManager.Instance.mLevelBuilder.GetCurrentLevel().solution;
        
        SolutionLabel2.text = " "+solution;
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



    public void ShowMilestoneWindow()
    {
        milestoneWindow.Show();
    }


    public void SetTestLabelText(string s)
    {
        TestLabel.text = s;
    }
    
}
