
using System;
using System.Collections;
using Assets.SimpleLocalization;
using common;
using EasyMobile;
using UnityEngine;
using UnityEngine.Events;

public class EasyMobileManager: Singleton<EasyMobileManager>
    {
        public bool AdvancedUnlocked = false;
        public bool ProductChecked = false;
        private GameObject progressBarInapp;
        public static UnityEvent LevelPackPurchased=new UnityEvent();
        
        
        private void Start()
        {
            
            
        }

        private void Awake()
        {
            if (!RuntimeManager.IsInitialized())
                RuntimeManager.Init();
            
        }



        public void RequestRating()
        {

            if (LevelResults.GetLastSolvedLevel()<30)
            {
                return;
            }
            
            if (StoreReview.CanRequestRating())
            {
                
                var localized = new RatingDialogContent(
                    LocalizationManager.Localize("RATE_LOC_TITLE") +" "+ RatingDialogContent.PRODUCT_NAME_PLACEHOLDER,
                    LocalizationManager.Localize("RATE_LOC_MESSAGE") +" "+ RatingDialogContent.PRODUCT_NAME_PLACEHOLDER + "?",
                    LocalizationManager.Localize("RATE_LOC_LOWRATINGMESSAGE"),
                    LocalizationManager.Localize("RATE_LOC_HIGHRATINGMESSAGE"),
                    LocalizationManager.Localize("RATE_LOC_POSTPONEBUTTONLABEL"),
                    LocalizationManager.Localize("RATE_LOC_REFUSEBUTTONLABEL"),
                    LocalizationManager.Localize("RATE_LOC_RATEBUTTONLABEL"),
                    LocalizationManager.Localize("RATE_LOC_CANCELBUTTONLABEL"),
                    LocalizationManager.Localize("RATE_LOC_FEEDBACKBUTTONLABEL")
                );
                StoreReview.RequestRating(localized);    
            }
            
            
        }
        
        
        
        

        public void CheckProductsAvailability()
        {
            
// #if UNITY_STANDALONE || UNITY_EDITOR
//             AdvancedUnlocked = true;
//             return;
// #endif
            bool isInitialized = InAppPurchasing.IsInitialized();
            if (!isInitialized)
            {
                return;
            }
                        
            AdvancedUnlocked = InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_spacepuzzle_premiumlevels50_100);
            
            ProductChecked = true;

        }

        public void PurchaseAdvancedUnlocked()
        {

            if (!AdvancedUnlocked)
            {
                ProductChecked = false;
                CheckProductsAvailability();
                if (AdvancedUnlocked)
                {
                    return;
                }
            }
            InAppPurchasing.Purchase(EM_IAPConstants.Product_spacepuzzle_premiumlevels50_100);
        }


        private void OnEnable()
        {

            InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandle;
            InAppPurchasing.PurchaseFailed += PurchaseFailedHandle;
            
            SceneMover.OnMainMenuLoadEvent.AddListener(RequestRating);
            


        }


        private void Update()
        {
           

            if (!ProductChecked)
            {
                CheckProductsAvailability();
            }
            else
            {
                if (progressBarInapp == null)
                {
                    progressBarInapp=GameObject.Find("ProgressBarInapp");
                }
                else
                {
                    progressBarInapp.SetActive(false);   
                }
            }
            
            
        }

       
            
        
        
        public void SendMailToDeveloper()
        {
            Application.OpenURL("mailto:maxdk9@gmail.com?subject=Cosmic Brain Puzzle mobile app&body=Hi, i have a problem...");
        }



        private void OnDisable()
        {
            InAppPurchasing.PurchaseCompleted -= PurchaseCompletedHandle;
            InAppPurchasing.PurchaseFailed -= PurchaseFailedHandle;
            

        }

        private void PurchaseFailedHandle(IAPProduct product)
        {
            NativeUI.Alert("ERROR","Purchase failed " + product.Name);
        }

        private void PurchaseCompletedHandle(IAPProduct product)
        {
            switch (product.Name)
            
            {
                   case EM_IAPConstants.Product_spacepuzzle_premiumlevels50_100:
                       AdvancedUnlocked = true;
                       LevelPackPurchased.Invoke();
                       break;
                   default:
                       break;
            }
        }

        public bool UserSpendMoney()
        {
            return AdvancedUnlocked;
        }


        public void ShareAppLink()
        {

            String appUrl = "https://play.google.com/store/apps/details?id=mazzy.and.thirtyrails";
            
            
#if UNITY_IOS
        appUrl="";
 
#elif UNITY_ANDROID
            appUrl = "https://play.google.com/store/apps/details?id=mazzy.and.spacepuzzle";
#endif
            // Share a URL
            Sharing.ShareURL(appUrl);
        }


        public void ShareScreenshot()
        {
            this.StartCoroutine(SaveScreenshot());
            string path = System.IO.Path.Combine(Application.persistentDataPath, "screenshot");
            //Sharing.ShareImage(path, "This is a sample message");
            Sharing.ShareScreenshot("screenshot_spacepuzzle", "space puzzle ");
        }
        
        IEnumerator SaveScreenshot()
        {
            yield return new WaitForEndOfFrame();
            string path = Sharing.SaveScreenshot("screenshot");
        }

        public void OpenMyGames()
        {
            Application.OpenURL("https://play.google.com/store/apps/developer?id=Maxim+Matyushenko");
        }
    }
