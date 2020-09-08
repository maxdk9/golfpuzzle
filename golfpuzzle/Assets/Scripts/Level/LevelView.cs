using System.Collections;
using System.Collections.Generic;
using common;
using Highscores;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    private bool levelEnabled;
    private bool levelPurchased;
    

    private LevelResult levelResult;
    public TextMeshProUGUI levelNumberLabel;
    public Image[] starImages;
    public Image dollarImage;
    public GameObject StarImagesGroup;
    public Sprite starGray;
    public Sprite starGold;
    private Color colorEnabled = Color.white;
    private Color colorDisabled = Color.gray;
    
    
    
    
    

    public bool LevelEnabled
    {
        get => levelEnabled;
        set
        {
            levelEnabled = value;
            levelNumberLabel.color = this.levelEnabled ? colorEnabled : colorDisabled;

        }
    }

    public bool LevelPurchased
    {
        get => levelPurchased;
        set
        {
            levelPurchased = value;
            if (levelPurchased)
            {
                dollarImage.enabled = false;
                StarImagesGroup.SetActive(true);
            }
            else
            {
                dollarImage.enabled = true;
                StarImagesGroup.SetActive(false);
            }
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        EasyMobileManager.LevelPackPurchased.AddListener((() =>
            {
                UpdateLevelPurchased();
            }));
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelPurchased)
        {
            UpdateLevelPurchased();
        }
    }

    private void UpdateLevelPurchased()
    {
        LevelPurchased = LevelAvailability.LevelPurchased(levelResult.levelNumber);
    }
    
    
    

    public void SetResult(LevelResult result)
    {
        levelResult = result;
        UpdateLevelPurchased();
        UpdateUI();
    }

    private void UpdateUI()
    {
        levelNumberLabel.text = levelResult.levelNumber.ToString();
    
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < levelResult.starNumber)
            {
                starImages[i].sprite = starGold;
            }
            else
            {
                starImages[i].sprite = starGray;
            }
        }
    }

    private string GetCurrentSolution()
    {
        string solution = HighscoreManager.GetInstance()
            .GetLevelSolution(this.levelResult.levelNumber);
        return solution;
    }

    public void StartLevel()
    {

        if (!levelPurchased)
        {
            EasyMobileManager.Instance.PurchaseAdvancedUnlocked();
            return;
        }
        
        if (!levelEnabled)
        {
            
            
            MessageWindowCommon.CreateWindow(GameObject.Find("Canvas").transform,this.transform.position).Show("Level not available");
            return;
        }
        
        
        DataHolder.ChosenLevelNumber = levelResult.levelNumber;
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.game);
    }
    
    
    
    
    
    
    
}
