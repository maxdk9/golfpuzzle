using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObjectEd : MonoBehaviour
{
    public ColorToPrefab mColorToPrefab;

    public Image icon;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        icon = GetComponent<Image>();
        SpriteRenderer renderer = mColorToPrefab.prefab.GetComponent<SpriteRenderer>();
        
        icon.sprite = renderer.sprite;
        icon.color = renderer.color;
    }

    public static void DisableAllOutlines()
    {
        MapObjectEd[] mapObjectEds = GameObject.FindObjectsOfType<MapObjectEd>();
        foreach (MapObjectEd mapObjectEd in mapObjectEds)
        {
            mapObjectEd.GetComponent<Outline>().enabled = false;
        }
    }
}
