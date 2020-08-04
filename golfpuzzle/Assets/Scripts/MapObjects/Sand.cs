using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            Ball ball=other.GetComponent<Ball>();
            if (ball != null)
            {
                ball.inSand = true;    
            }   
        }
        
        
    }
}
