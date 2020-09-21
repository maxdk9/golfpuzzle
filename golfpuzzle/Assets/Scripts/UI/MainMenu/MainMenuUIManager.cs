using System.Collections;
using System.Collections.Generic;
using common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public Button newGameButton;

    public Button continueGameButton;

    public Button editorButton;

    public Button testLevelButton;
    
    public Button optionsLevelButton;
    public Image LogoImage;
    public Image LogoPlanet;

    public Button MoreGamesButton;
    public Button TwitterButton;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(newGameButtonOnClick);
        editorButton.onClick.AddListener(editorButtonOnClick);
        
        MoreGamesButton.onClick.AddListener(MoreGamesButtonClick);
        TwitterButton.onClick.AddListener(ShareButtonOnClick);
   

        StartCoroutine(FadeInLogoRoutine());

    }

    private void ShareButtonOnClick()
    {
        EasyMobileManager easyMobileManager = FindObjectOfType<EasyMobileManager>();
        easyMobileManager.ShareAppLink();
    }

    private void MoreGamesButtonClick()
    {
        EasyMobileManager easyMobileManager = FindObjectOfType<EasyMobileManager>();
        easyMobileManager.OpenMyGames();
    }


    private IEnumerator FadeInLogoRoutine()
    {
        float duration = .5f;
        LogoImage.CrossFadeAlpha(0,0,true);
        LogoPlanet.CrossFadeAlpha(0,0,true);
        yield return new WaitForSeconds(.01f);

        
        LogoImage.CrossFadeAlpha(1,duration,true);
        LogoPlanet.CrossFadeAlpha(1,duration,true);
    }

    private void newGameButtonOnClick()
    {
        
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.chooselevel);
       
    }

    private void editorButtonOnClick()
    {
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.editor);
       
    }

    private void testLevelButtonOnClick()
    {
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.gametesmap);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
