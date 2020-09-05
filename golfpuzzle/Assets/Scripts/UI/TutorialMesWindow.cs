using Assets.SimpleLocalization;
using Preferences;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TutorialMesWindow : MonoBehaviour
    {
        public TextMeshProUGUI messageLabel;
        public Image backImage;
        
        public Toggle disableTutorialToggle;
        
        private CanvasGroup canvasGroup;
        private HideBeyondScreenComponent hideComponent;
        public bool showed = false;

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
            if (disableTutorialToggle.isOn)
            {
                GamePreferences.SetDisableTutorial(true);
                TutorialManager.ClearLastTutorialPointEvent.Invoke();
                this.Hide();    
            }
        
        }
        
        
    }
}