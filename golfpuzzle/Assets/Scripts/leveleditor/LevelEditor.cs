using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
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
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                EditorTile editorTile = EditorTiles[j, i];
                result[currentIndex] = editorTile.mColorToPrefab.color;
                currentIndex++;
            }   
        }
        return result;
    }

    
    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes =_texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        //Debug.Log(_bytes.Length/1024  + "Kb was saved as: " + _fullPath);
    }


    public Texture2D GetCurrentMapTexture()
    {
        SaveMapSizeInPreferences(width,height);
        Color[] colors = getCurrentColorMap();
        Texture2D result =
            LevelEditor.TextureFromColourMap(colors, width, height);
        return result;
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
        this.width = w;
        this.height = h;
        EditorTiles=new EditorTile[width,height];
        SetMainCamera();
        GenerateBoard();
    }

    public static string GetCurrentMapPath()
    {
        string filename = "myimage.png";
        
        string path = GetStreamingAssetsFilePath.GetPath(filename);
        
       
        
        
        
        return path;
    }


    public void SetStartParams()
    {
        EditorTiles=new EditorTile[width,height];
        SetMainCamera();
        GenerateBoard();
    }
    
    // public void SetGameObjects()
    // {
    //     mLevelBuilder = FindObjectOfType<LevelBuilder>();
    //     this.EditorBoard = GameObject.Find("EditorBoard").transform;
    //
    // }
}
