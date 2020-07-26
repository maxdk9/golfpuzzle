using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool mReadyForInput;
    public Player mPlayer;
    public LevelBuilder mLevelBuilder;
    public GameObject mNextButton;

    private void Start()
    {
        mReadyForInput = true;
        mLevelBuilder.Build();
        mPlayer = FindObjectOfType<Player>();
    }

    private void Update()
    {
        
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");
        // Debug.Log("Horizontal="+horizontal);
        // Debug.Log("Vertical="+vertical);
        
         Vector2 moveInput=new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
          
         //Vector2 moveInput=new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
         moveInput.Normalize();
         if (moveInput.SqrMagnitude() > .5f)
         {
             if (mReadyForInput)
             {
                 mReadyForInput = false;
                 
                 mPlayer.Move(moveInput);
             }
         }
         else
         {
             mReadyForInput = true;
         }
    }


    public void NextLevel()
    {
        mNextButton.SetActive(false);

       // RemoveOldObjects();
       ResetScene();
        mLevelBuilder.NextLevel();
        mLevelBuilder.Build();
        
        
        //StartCoroutine(ResetSceneAsync());
    }

    private void RemoveOldObjects()
    {
        var objects = GameObject.FindGameObjectsWithTag("Box");
        foreach (var VARIABLE in objects)
        {
            Destroy(VARIABLE.gameObject);
        }

        var obplayer = GameObject.FindObjectOfType<Player>();
        
        
            Destroy(obplayer.gameObject);
        
        
        var objects2 = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var VARIABLE in objects2)
        {
            Destroy(VARIABLE.gameObject);
        }
        
        var objects3 = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var VARIABLE in objects3)
        {
            Destroy(VARIABLE.gameObject);
        }
         
    }

    private IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncOperation.isDone)
            {
                yield return null;
                Debug.Log("Loading");
            }

            Resources.UnloadUnusedAssets();
        }
        
        
        AsyncOperation asyncOperation1 = SceneManager.LoadSceneAsync("LevelScene",LoadSceneMode.Additive);
        while (!asyncOperation1.isDone)
        {
            yield return null;
            Debug.Log("Loading");
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        mLevelBuilder.Build();
        mPlayer = FindObjectOfType<Player>();
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());

    }
}
