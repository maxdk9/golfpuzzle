using System.Collections;
using System.Collections.Generic;
using common;
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(newGameButtonOnClick);
        editorButton.onClick.AddListener(editorButtonOnClick);
        testLevelButton.onClick.AddListener(testLevelButtonOnClick);
        
    }

    private void newGameButtonOnClick()
    {
        //GameManager.TestMap = false;
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.game);
        //SceneManager.LoadSceneAsync("SampleScene");
    }

    private void editorButtonOnClick()
    {
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.editor);
        //SceneManager.LoadSceneAsync("LevelEditor");
    }

    private void testLevelButtonOnClick()
    {
        // GameManager.TestMap = true;
        // SceneManager.LoadSceneAsync("SampleScene");
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.gametesmap);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
