using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{

    public static UnityEvent OpenGateEvent=new UnityEvent();
    
    public bool mOpen;
    private SpriteRenderer renderer;
    [SerializeField]
    public Sprite GateImage;

    public Animator mAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        mAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        mOpen = false;
        renderer.sprite = GateImage;
        mAnimator.SetBool("Hide",false);
    }


    public void Open()
    {
        mOpen = true;
        renderer.sprite = null;
        mAnimator.SetBool("Hide",true);
        OpenGateEvent.Invoke();
        
    }
}
