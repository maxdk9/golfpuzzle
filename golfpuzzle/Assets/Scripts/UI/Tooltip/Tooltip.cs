using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.EventSystems;


[DisallowMultipleComponent]
public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string TooltipText = "Hello";
    private TooltipController TooltipController;
    private EventSystem EventSystem;
    private bool CursorInside = false;
    private bool IsUIObject = false;
    private bool Showing = false;
    
    

    public delegate String SetInfoString();

    public SetInfoString setInfoString;

    private void Awake()
    {
        EventSystem = FindObjectOfType<EventSystem>();
        TooltipController = FindObjectOfType<TooltipController>();
        if (!TooltipController)
        {
            TooltipController = AddTooltipPrefabToScene();
        }
        if (!TooltipController)
        {
            Debug.LogWarning("No tooltip prefab");
        }
        if (GetComponent<RectTransform>())
            IsUIObject = true;
    }

    private void Update()
    {
        if (!CursorInside)
            return;
        TooltipController.ShowTooltip();
    }

    public static TooltipController AddTooltipPrefabToScene()
    {
        //GameObject uiCanvasGO=GameObject.Find("UICanvas");
        
        TooltipController result = Instantiate(Resources.Load<GameObject>("Prefabs/ui/BaseTextTooltip"))
            .GetComponentInChildren<TooltipController>();
        return result;
    }


    public void ShowTooltip()
    {
        Showing = true;
        CursorInside = true;

        if (setInfoString != null)
        {
            this.TooltipText = setInfoString();
        }

        TooltipController.SetRawText(LocalizationManager.Localize(TooltipText));
        TooltipController.ShowTooltip();
    }

    public void HideTooltip()
    {
        if (!Showing)
            return;
      //  Debug.Log("Hide Tooltip from tooltip");
        Showing = false;
        CursorInside = false;
        TooltipController.HideTooltip();
    }


    public void Reset()
    {
        if (GetComponent<RectTransform>())
            return;
        if (GetComponent<Collider>())
            return;
        gameObject.AddComponent<BoxCollider>();
    }


    private void OnMouseOver()
    {
        if (IsUIObject)
            return;
        if (!GameManager.TooltipModeActivated)
        {
            return;
        }
        
        if (EventSystem)
        {
            if (Input.GetMouseButton(0) && EventSystem.IsPointerOverGameObject())
            {
              //  Debug.Log("Pointer is over object " + this.gameObject);
                HideTooltip();
                return;
            }
        }

        ShowTooltip();
    }

    private void OnMouseExit()
    {
        if (!GameManager.TooltipModeActivated)
        {
            return;
        }
        
        if (IsUIObject)
            return;
        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsUIObject)
            return;
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsUIObject)
            return;
        HideTooltip();
    }
}