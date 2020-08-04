using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Security.Cryptography.X509Certificates;
using Assets.SimpleLocalization;
using consts;
using Highscores;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI LevelCounter;
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI BestResultCounter;
    public Button ResetButton;
    public Button NextLevelButton;
    public WinLevelPanel winLevelPanel;

    public TextMeshProUGUI TestLabel;
    
    public static UnityEvent UpdateGameUIEvent=new UnityEvent();
    public static UnityEvent UpdateMoveCounterEvent=new UnityEvent();

    [SerializeField]
    public GameObject prefabWinLevelPanel;
    [SerializeField]
    public Canvas UICanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameUIEvent.AddListener(UpdateLevelCounter);
        UpdateGameUIEvent.AddListener(UpdateMoveCounter);
        UpdateGameUIEvent.AddListener(UpdateBestResultCounter);
        UpdateMoveCounterEvent.AddListener(UpdateMoveCounter);
        GameObject winLevelPanelGO = GameObject.Instantiate(prefabWinLevelPanel, UICanvas.transform);
        winLevelPanel = winLevelPanelGO.GetComponent<WinLevelPanel>();
        winLevelPanelGO.SetActive(false);
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
    
}
