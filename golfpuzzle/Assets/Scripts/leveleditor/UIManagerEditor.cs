
using System;
using System.IO;
using common;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerEditor : MonoBehaviour
{

    public TMP_InputField SizeWidthField;
    public TMP_InputField SizeHeightField;
    public TextMeshProUGUI InformationLabel;
    public Button ClearMapButton;
    public Button SaveMapButton;
    public Button MailToButton;
    public Button TestMapButton;
    public Button LoadMapButton;



    
    public Transform MapObjectsTransform;
    public GameObject MapObjectEdPrefab;

    public LevelBuilder mLevelBuilder;
    
    void Start()
    {
        FillMapObjects();
        SizeHeightField.text = 10.ToString();
        SizeWidthField.text = 10.ToString();
        ClearMapButton.onClick.AddListener(ClearMapButtonOnClick);
        
        // #if !UNITY_EDITOR && UNITY_ANDROID
        //      SaveMapButton.onClick.AddListener(SaveButtonClick_Android);    
        //      LoadMapButton.onClick.AddListener(LoadButtonClick_Android);
        // #endif
        //
        // #if UNITY_EDITOR
        //     SaveMapButton.onClick.AddListener(SaveButtonClick_Editor);    
        //     LoadMapButton.onClick.AddListener(LoadButtonClick_Editor);
        // #endif
        //
        
             SaveMapButton.onClick.AddListener(SaveButtonClick_Editor);    
             LoadMapButton.onClick.AddListener(LoadButtonClick_Editor);
        
        
        MailToButton.onClick.AddListener(MailToButtonClick);
        TestMapButton.onClick.AddListener(TestMapButtonClick);
    }

    private void LoadButtonClick_Editor()
    {
        MyFileBrowser browser = GameObject.FindObjectOfType<MyFileBrowser>();
        LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        browser.LoadTexture(levelEditor.LoadLevelTexture2d);
    }

    private void SaveButtonClick_Editor()
    {

        MyFileBrowser browser = GameObject.FindObjectOfType<MyFileBrowser>();
        LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        Texture2D texture2D = levelEditor.GetCurrentMapLevel().map;
        
        browser.SaveTexture(texture2D);
        

        // LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        // Texture2D texture = levelEditor.GetCurrentMapTexture();
        // if (texture == null)
        // {
        //     EditorUtility.DisplayDialog(
        //         "Select Texture",
        //         "You Must Select a Texture first!",
        //         "Ok");
        //     return;
        // }
        //
        // var path = EditorUtility.SaveFilePanel(
        //     "Save texture as PNG",
        //     "",
        //     texture.name + ".png",
        //     "png");
        //
        // if (path.Length != 0)
        // {
        //     var pngData = texture.EncodeToPNG();
        //     if (pngData != null)
        //         File.WriteAllBytes(path, pngData);
        // }

    }

    private void TestMapButtonClick()
    {
        LevelEditor levelEditor = GameObject.FindObjectOfType<LevelEditor>();
        levelEditor.UpdateGlobaleCurrentLevelFromEditor();
        levelEditor.SaveCurrentMapTextureInPreferences();
        
        SceneMover.Instance.SetCurrentScreen(SceneMover.enumScreen.gametesmap);
    }

    private void MailToButtonClick()
    { 

        try
        {
            LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
            Texture2D texture = levelEditor.GetCurrentMapLevel().map;
            NativeShare share = new NativeShare();
            share.AddFile(texture,"Mapimage.png"  );
            share.Share();
        }
        catch(Exception e)
        {
            InformationLabel.text = e.Message;
        }
    }



    private void LoadButtonClick_Android()
    {
        


    }
    
    

    // private void SaveButtonClick_Android()
    // {
    //     
    //     try
    //     {
    //         LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
    //         Texture2D texture = levelEditor.GetCurrentMapTexture();
    //         GameManager.CurrentSavedMap = texture;
    //         levelEditor.SaveCurrentMapTextureInPreferences();
    //
    //
    //     }
    //     catch (Exception e)
    //     {
    //         InformationLabel.text = e.Message;
    //         return;
    //     }
    //     InformationLabel.text = "Map succesfully saved";
    //     
    //     
    // }
    
    
    
    
    
    private void ClearMapButtonOnClick()
    {
        
        LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        levelEditor.RefillBoard(Int32.Parse(SizeWidthField.text),Int32.Parse(SizeHeightField.text));
    }

    private void FillMapObjects()
    {
        foreach (ColorToPrefab colorToPrefab in mLevelBuilder.colorMappings)
        {
            GameObject mapOb = Instantiate(MapObjectEdPrefab, MapObjectsTransform);
            MapObjectEd mapObjectEd = mapOb.GetComponent<MapObjectEd>();
            mapObjectEd.mColorToPrefab = colorToPrefab;
            mapObjectEd.Init();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
