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
