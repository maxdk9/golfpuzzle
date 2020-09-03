using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Assets.SimpleLocalization;
using DG.Tweening;
using Preferences;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MessageWindow : MonoBehaviour
{
    public TextMeshProUGUI messageLabel;
    public bool showed = false;
    private HideBeyondScreenComponent hideComponent;
    public Image backgroundImage;
    private CanvasGroup canvasGroup;
    public Toggle DisableTutorialToggle;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        hideComponent = this.GetComponent<HideBeyondScreenComponent>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    public void Hide()
    {
        if (hideComponent)
        {
            hideComponent.Hide();
            
        }
        
        
        showed = false;
    }


    public void Show()
    {

        if (canvasGroup.interactable)
        {
            hideComponent.Show();
                
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        showed = true;
        
    }

    public void Show(string message)
    {
        
        messageLabel.text = message;
        Show();
    }

    public void ShowKey(string key)
    {
        string message = LocalizationManager.Localize(key);
        Show(message);
    }

    public void DisableTutorialMessages()
    {
        if (DisableTutorialToggle.value)
        {
            GamePreferences.SetDisableTutorial(true);
            this.Hide();    
        }
        
    }

}
