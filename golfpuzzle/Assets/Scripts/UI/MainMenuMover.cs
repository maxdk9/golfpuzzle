using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenuMover : MonoBehaviour
{
    
    public RectTransform OptionsTransform;
    public RectTransform CreditsTransform;

    public Vector3 hidePosition=new Vector3(3000,0,0);
    public Vector3 showPosition=new Vector3(0,0,0);


    public float duration = .5f;


    public void ShowMainMenu()
    {
        OptionsTransform.DOMove(hidePosition, duration);
        CreditsTransform.DOMove(hidePosition, duration);
    }


    public void ShowCreditsMenu()
    {
        CreditsTransform.DOMove(showPosition, duration);
        OptionsTransform.DOMove(hidePosition, duration);
    }

    public void ShowOptionsMenu()
    {
        CreditsTransform.DOMove(hidePosition, duration);
        OptionsTransform.DOMove(showPosition, duration);
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
