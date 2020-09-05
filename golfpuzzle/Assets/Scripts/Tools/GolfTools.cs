using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using UnityEngine;

public class GolfTools : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RenameFiles()
    {

        string olddirectory = @"D:\1\modify";
        DirectoryInfo d = new DirectoryInfo(olddirectory);

        string newdirectory = @"D:\1\new";
        FileInfo[] infos = d.GetFiles();
        foreach(FileInfo f in infos)
        {
            //File.Move(f.FullName, f.FullName.Replace("abc_",""));
            string filename = f.Name;
            string extension = f.Extension;
            
            
            string filename1=filename.Replace( extension, "");
            
            int filenumber=Int32.Parse(filename1);
            filenumber = filenumber + 1;

            string newname = filenumber.ToString() + extension;

            string newfullname = f.FullName.Replace(olddirectory, newdirectory);
            newfullname = newfullname.Replace(filename, newname);
            
            
            File.Copy(f.FullName,newfullname);
            
            //Debug.Log(String.Format("Old name : {0} ; new name :{1}",f.FullName,newfullname));
            
            
        }
    }


    public void CorrectJson()
    {
        
        String filename = "cards1.json";
        string filePath = GetStreamingAssetsFilePath.GetPath(filename);
        string dataAsJson="";
        List<Level> mLevels=new List<Level>();
        if (File.Exists(filePath))
        {
            dataAsJson = File.ReadAllText(filePath);
            
         
            mLevels = JsonConvert.DeserializeObject<List<Level>>(dataAsJson,
                new Newtonsoft.Json.Converters.StringEnumConverter());
        }
        else
        {
            Debug.Log(filename + "No file exist");
        }

        foreach (Level level in mLevels)
        {
            level.mMapPath = "LevelStorage/" + level.levelNumber.ToString();
        }

        String result = JsonConvert.SerializeObject(mLevels);
        Debug.Log(result);


    }
    
    

    public void ShowMessageWindow()
    {
        this.StartCoroutine(ShowOrHideRoutine());


    }

    private IEnumerator ShowOrHideRoutine()
    {
        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();

        if (uiManager.tutorialMessageWindow.showed)
        {
            uiManager.tutorialMessageWindow.Hide();
            
        }
        else
        {
            uiManager.tutorialMessageWindow.Show();    
        }
        
        

        yield return null;
    }
}
