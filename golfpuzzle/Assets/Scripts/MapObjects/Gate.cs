using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{

    public static UnityEvent OpenGateEvent=new UnityEvent();
    
    public bool mOpen;
    public bool mBusy;
    
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

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<iBall>() != null)
        {

            if (other.GetComponent<Bomb>() != null)
            {
                return;
            }
            mBusy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.GetComponent<iBall>() != null)
        {
            mBusy = false;
        }
    }

    

    public void Close()
    {
        mOpen = false;
        renderer.sprite = GateImage;
        mAnimator.SetBool("Hide",false);
        
        Debug.Log("Gate is closed");
    }


    public void Open()
    {
        mOpen = true;
        renderer.sprite = null;
        mAnimator.SetBool("Hide",true);
        OpenGateEvent.Invoke();
        Debug.Log("Gate is open");
    }
}
