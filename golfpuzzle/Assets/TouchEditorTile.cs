using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEditorTile : MonoBehaviour
{
    private EditorTile mEditorTile;

    private LevelEditor levelEditor;
    // Start is called before the first frame update
    void Start()
    {
        mEditorTile = this.GetComponent<EditorTile>();
        levelEditor = FindObjectOfType<LevelEditor>();
    }


    private void OnMouseDown()
    {
        
        if (levelEditor.GetCurrentColorToPrefab() == null)
        {
            return;
        }
        
        mEditorTile.SetColorToPrefab(levelEditor.GetCurrentColorToPrefab());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
