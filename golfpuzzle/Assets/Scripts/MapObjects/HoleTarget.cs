using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTarget : MonoBehaviour
{
    public bool mBusy;

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
                ball.SetDocked(true);
                  
            }
            
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name.CompareTo("Ball") == 0)
        {
            mBusy = false;
        }
    }
}
