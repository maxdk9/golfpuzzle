using System;
using common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyInappWindow : SimpleWindow
    {
        public Button BuyInApp;
        public Button ToMainMenuButton;
        public Button NextLevelButton;

        private void Start()
        {
            BuyInApp.onClick.AddListener(BuyInAppClick);
            ToMainMenuButton.onClick.AddListener(ToMainMenuButtonClick);
            NextLevelButton.onClick.AddListener(NextLevelButtonOnClick);
            
        }

        private void NextLevelButtonOnClick()
        {
            this.Close();
            GameManager.Instance.NextLevel();
        }

        private void ToMainMenuButtonClick()
        {
            SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.mainmenu);
        }

        private void BuyInAppClick()
        {
            EasyMobileManager.Instance.PurchaseAdvancedUnlocked();
        }


        public override void Show()
        {
            base.Show();
            if (!LevelBuilder.NextLevelAvailable())
            {
                NextLevelButton.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (EasyMobileManager.Instance.AdvancedUnlocked)
            {
                NextLevelButton.gameObject.SetActive(true);
            }
        }
    }
}