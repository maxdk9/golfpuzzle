using System.Collections;
using System.Collections.Generic;
using common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    private bool levelEnabled;

    private LevelResult levelResult;
    public TextMeshProUGUI levelNumberLabel;
    public Image[] starImages;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    

    public void SetResult(LevelResult result)
    {
        levelResult = result;
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

    public void StartLevel()
    {

        if (!levelEnabled)
        {
            
            
            MessageWindowCommon.CreateWindow(GameObject.Find("Canvas").transform,this.transform.position).Show("Level not available");
            return;
        }
        DataHolder.ChosenLevelNumber = levelResult.levelNumber;
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.game);
    }
    
    
    
    
    
    
    
}
