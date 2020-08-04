using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchMapObjectEd : MonoBehaviour, IPointerDownHandler
{
    
    private LevelEditor levelEditor;
    // Start is called before the first frame update
    void Start()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        MapObjectEd.DisableAllOutlines();
        
        this.GetComponent<Outline>().enabled=true;
        MapObjectEd mapObjectEd = this.GetComponent<MapObjectEd>();
        levelEditor.SetCurrentColorToPrefab(mapObjectEd.mColorToPrefab);
    }
}
