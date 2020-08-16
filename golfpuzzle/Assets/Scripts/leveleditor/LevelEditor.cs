using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{


    public static Level GlobalCurrentLevel;
    
    
    private Camera MainCamera;
    private ColorToPrefab currentColorToPrefab;
    public GameObject EditorTilePrefab;
    public int width;
    public int height;
    public LevelBuilder mLevelBuilder;

    public Transform EditorBoard;
    
    
    public static int borderTileIndex = 0;
    public static int emptyTileIndex = 8;

    public EditorTile[,] EditorTiles;
    
    
    // Start is called before the first frame update
    void Start()
    {
        width = 10;
        height = 10;
        LoadCurrentMapFromPreferences();
        
    }

    public void SetMainCamera()
    {
        float borderSize = .2f;
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        float aspectRatio = (float) Screen.width / (float) Screen.height;


        float verticalSize = ((float) width / 2f + (float) borderSize) / aspectRatio;
        ;
        float horizontalSize = ((float) height / 2f + (float) borderSize) / aspectRatio;

        MainCamera.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
        MainCamera.transform.position = new Vector3(borderSize + (float) (width - 1) / 2.0f,
            (float) (MainCamera.orthographicSize) / 2.0f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentColorToPrefab(ColorToPrefab mColorToPrefab)
    {
        currentColorToPrefab = mColorToPrefab;
        
    }

    public ColorToPrefab GetCurrentColorToPrefab()
    {
        return currentColorToPrefab;
    }


    public void GenerateBoard()
    {
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                ColorToPrefab colorToPrefab;
                if (i == 0 || j == 0 || j == height - 1 || i == width - 1)
                {
                    colorToPrefab = mLevelBuilder.colorMappings[borderTileIndex];
                }

                else
                {
                    colorToPrefab = mLevelBuilder.colorMappings[emptyTileIndex];
                }
                
                GameObject EditorTileGO = Instantiate(EditorTilePrefab,new Vector3(i,j,0),Quaternion.identity,EditorBoard);
                EditorTileGO.name = "ET_" + i.ToString() + "_" + j.ToString();
                EditorTile editorTile = EditorTileGO.GetComponent<EditorTile>();
                EditorTiles[i, j] = editorTile;
                editorTile.SetColorToPrefab(colorToPrefab);
                
                EditorTileGO.SetActive(false);
                EditorTileGO.SetActive(true);


            }
        }
    }



    public Color[] getCurrentColorMap()
    {
        Color[] result=new Color[width*height];

        int currentIndex = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                EditorTile editorTile = EditorTiles[j, i];
                result[currentIndex] = editorTile.mColorToPrefab.color;
                currentIndex++;
            }   
        }
        return result;
    }

    //
    // public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    // {
    //     byte[] _bytes =_texture.EncodeToPNG();
    //     System.IO.File.WriteAllBytes(_fullPath, _bytes);
    //     //Debug.Log(_bytes.Length/1024  + "Kb was saved as: " + _fullPath);
    // }


    private Texture2D GetCurrentMapTexture()
    {
        SaveMapSizeInPreferences(width,height);
        Color[] colors = getCurrentColorMap();
        Texture2D result =
            LevelEditor.TextureFromColourMap(colors, width, height);
        return result;
    }

    public Level GetCurrentMapLevel()
    {
        Level level=new Level();
        
        
        Texture2D texture2D = GetCurrentMapTexture();
        
        level.width = width;
        level.height = height;
        level.map = texture2D;
        
        level.SetColorArray();
        
        return level;


    }
    

    private static void SaveMapSizeInPreferences(int width,int height)
    {
        PlayerPrefs.SetInt("EditedMapWidth",width);
        PlayerPrefs.SetInt("EditedMapHeight",height);
    }

    public static int GetSavedMapWidth()
    {
        return PlayerPrefs.GetInt("EditedMapWidth", 2);
    }
    
    public static int GetSavedMapHeight()
    {
        return PlayerPrefs.GetInt("EditedMapHeight", 2);
    }
    

    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height) {
        Texture2D texture = new Texture2D (width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        
        texture.SetPixels (colourMap);
        texture.Apply ();
        return texture;
    }
    

    public void RefillBoard(int w,int h)
    {
        SetBoardSize(w,h);
        GenerateBoard();
    }


    public void SetBoardSize(int w,int h)
    {
        EditorTile.RemoveAllTiles();
        this.width = w;
        this.height = h;
        EditorTiles=new EditorTile[width,height];
        SetMainCamera();
    }
    

    


   

    public void LoadLevel(Level level)
    {
        GlobalCurrentLevel = level;
        SetBoardSize(level.width,level.height);
        
        
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Color c = level.map.GetPixel(i, j);

                ColorToPrefab colorToPrefab = mLevelBuilder.GetColorToPrefab(c);
                if (colorToPrefab == null)
                {
                    continue;
                }
                
                
                GameObject EditorTileGO = Instantiate(EditorTilePrefab,new Vector3(i,j,0),Quaternion.identity,EditorBoard);
                EditorTileGO.name = "ET_" + i.ToString() + "_" + j.ToString();
                EditorTile editorTile = EditorTileGO.GetComponent<EditorTile>();
                EditorTiles[i, j] = editorTile;
                editorTile.SetColorToPrefab(colorToPrefab);
                
                EditorTileGO.SetActive(false);
                EditorTileGO.SetActive(true);
                    
            }
        }
        
        
        
        
        
    }

    public void LoadLevelTexture2d(Texture2D texture)
    {
        
        Level level=new Level();
        level.width = texture.width;
        level.height = texture.height;
        level.map = texture;
        level.SetColorArray();   
        LoadLevel(level);
        

        // SetBoardSize(texture.width,texture.height);
        // for (int i = 0; i < width; i++)
        // {
        //     for (int j = 0; j < height; j++)
        //     {
        //         Color c = texture.GetPixel(i, j);
        //
        //         ColorToPrefab colorToPrefab = mLevelBuilder.GetColorToPrefab(c);
        //         if (colorToPrefab == null)
        //         {
        //             continue;
        //         }
        //
        //
        //         GameObject EditorTileGO = Instantiate(EditorTilePrefab, new Vector3(i, j, 0), Quaternion.identity,
        //             EditorBoard);
        //         EditorTileGO.name = "ET_" + i.ToString() + "_" + j.ToString();
        //         EditorTile editorTile = EditorTileGO.GetComponent<EditorTile>();
        //         EditorTiles[i, j] = editorTile;
        //         editorTile.SetColorToPrefab(colorToPrefab);
        //
        //         EditorTileGO.SetActive(false);
        //         EditorTileGO.SetActive(true);
        //     }
        // }
    }

    
    
    public void SaveCurrentMapTextureInPreferences()
    {
        if (GlobalCurrentLevel != null)
        {

            SaveMapSizeInPreferences(GlobalCurrentLevel.width, GlobalCurrentLevel.height);
            string savedString = JsonConvert.SerializeObject(GlobalCurrentLevel);
            PlayerPrefs.SetString("CurrentSavedLevel",savedString);
            PlayerPrefs.Save();    
        }
        // Texture2D texture = GetCurrentMapTexture();
        // Level savedLavel=new Level(); 
        // savedLavel.height = texture.height;
        // savedLavel.width = texture.width;
        // savedLavel.map = texture;
        // savedLavel.SetColorArray();
                    
        
    }



    public void LoadCurrentMapFromPreferences()
    {
        string currentSavedMap = PlayerPrefs.GetString("CurrentSavedLevel", "");
        if (currentSavedMap.Equals(""))
        {
            return;
        }

        try
        {
            
            Level level = JsonConvert.DeserializeObject<Level>(currentSavedMap);
            level.SetTextureFromColorArray();
            LoadLevel(level);

        }
        catch (Exception e)
        {
            
        }

            
    }


    public void UpdateGlobaleCurrentLevelFromEditor()
    {
        GlobalCurrentLevel = GetCurrentMapLevel();
    }
}