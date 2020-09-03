using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TooltipController : MonoBehaviour
{
    private Image Panel;
    private TextMeshProUGUI TooltipText;
    private RectTransform Rect;
    private int ShowInFrames = -1;
    private bool ShowNow = false;
    private Vector3 tooltipPosition;
    private void Awake()
    {
        TooltipText = GetComponentInChildren<TextMeshProUGUI>();
        Panel = GetComponent<Image>();
        Rect = GetComponent<RectTransform>();
        HideTooltip();
        
        tooltipPosition=new Vector3((float)Screen.width*.1f,(float)Screen.height*.8f,0);
        TooltipText.fontSize = (int)((float) Screen.width * .05f);
    }

    void Update()
    {
        UpdateShow();
    }

    private void UpdateShow()
    {
        if (ShowInFrames == -1)
            return;

        if (ShowInFrames == 0)
            ShowNow = true;

        if (ShowNow)
        {
            
            Vector2 corner = new Vector2((Input.mousePosition.x / Screen.width), (Input.mousePosition.y / Screen.height));    
            // Rect.pivot = new Vector2(1f,1)*corner;
            // Rect.anchoredPosition = Input.mousePosition;
            Rect.position = tooltipPosition;
        }
        ShowInFrames -= 1;
    }
    public void SetRawText(string text)
    {
        TooltipText.text = text;
        
    }
    
    public void ShowTooltip()
    {
        if (ShowInFrames == -1)
            ShowInFrames = 2;
    }

    public void HideTooltip()
    {
        
        ShowInFrames = -1;
        ShowNow = false;
       // Rect.anchoredPosition = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
       
       Rect.position=new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }
}