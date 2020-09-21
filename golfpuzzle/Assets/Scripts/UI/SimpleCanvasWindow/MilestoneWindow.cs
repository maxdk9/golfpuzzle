using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Highscores;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class MilestoneWindow : SimpleWindow
{
    public TextMeshProUGUI levelNumberLabel;
    public TextMeshProUGUI scoreValue;
    public Button closeButton;
    public Button shareButton;

    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(closeButtonOnClick);    
        shareButton.onClick.AddListener(shareButtonOnClick);
        
    }

    private void shareButtonOnClick()
    {
        EasyMobileManager easyMobileManager = FindObjectOfType<EasyMobileManager>();
        easyMobileManager.ShareScreenshot();
    }

    private void closeButtonOnClick()
    {
        Close();
        if (LevelBuilder.NextLevelAvailable())
        {
            GameManager.Instance.NextLevel();    
        }
        else
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.buyInappWindow.Show();
        }
        
    }

    public override void  Show()
    {
        LevelBuilder levelBuilder = FindObjectOfType<LevelBuilder>();
        levelNumberLabel.text = levelBuilder.mCurrentLevel.ToString();
        scoreValue.GetComponent<ShowChronometer>().StartChronometer(GameScore.GetCurrentGameResult(levelBuilder.mCurrentLevel));
        base.Show();
        
    }
   

    // Update is called once per frame
    void Update()
    {
     
    }
}
