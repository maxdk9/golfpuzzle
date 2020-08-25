using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoleTarget : MonoBehaviour
{
    
    
    public static UnityEvent DockBallInHoleEvent=new UnityEvent();
    public bool mBusy;

    public GameObject particleNormal;
    public GameObject particleRed;

    private void Start()
    {
        ChangeParticlesColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            if (mBusy)
            {
                return;
            }
            Ball ball=other.GetComponent<Ball>();
            if (ball != null)
            {
                mBusy = true;
                ChangeParticlesColor();
                
                
                ball.SetDocked(true);
                DockBallInHoleEvent.Invoke(); 
            }
            
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name.CompareTo("Ball") == 0)
        {
            mBusy = false;
            ChangeParticlesColor();
        }
    }

    private void ChangeParticlesColor()
    {
        if (mBusy)
        {
            particleNormal.SetActive(false);
            particleRed.SetActive(true);
        }
        else
        {
            particleNormal.SetActive(true);
            particleRed.SetActive(false);
        }
    }
    
}
