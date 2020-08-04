
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Transform MapObjectsTransform;
    public GameObject MapObjectEdPrefab;

    public LevelBuilder mLevelBuilder;
    
    void Start()
    {
        FillMapObjects();
        SizeHeightField.text = 10.ToString();
        SizeWidthField.text = 10.ToString();
        ClearMapButton.onClick.AddListener(ClearMapButtonOnClick);
        SaveMapButton.onClick.AddListener(SaveMapButtonClick);
        MailToButton.onClick.AddListener(MailToButtonClick);
        TestMapButton.onClick.AddListener(TestMapButtonClick);
    }

    private void TestMapButtonClick()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void MailToButtonClick()
    { 

        try
        {
            
            LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
            Texture2D texture = levelEditor.GetCurrentMapTexture();
            NativeShare share = new NativeShare();
            share.AddFile(texture,"Mapimage.png"  );
            share.Share();
        }
        catch(Exception e)
        {
            InformationLabel.text = e.Message;
        }
    }

    private void SaveMapButtonClick()
    {
        try
        {
            LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
            Texture2D texture = levelEditor.GetCurrentMapTexture();   
            byte[] bytes;
            bytes = texture.EncodeToPNG();
            System.IO.FileStream fileSave;
            string path = LevelEditor.GetCurrentMapPath();
            fileSave = new FileStream(path, FileMode.Create);
            System.IO.BinaryWriter binary;
            binary = new BinaryWriter(fileSave);
            binary.Write(bytes);
            fileSave.Close();
        }
        catch (Exception e)
        {
            InformationLabel.text = e.Message;
            return;
        }
        InformationLabel.text = "Map succesfully saved";
    }
    
    private void ClearMapButtonOnClick()
    {
        EditorTile.RemoveAllTiles();
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
