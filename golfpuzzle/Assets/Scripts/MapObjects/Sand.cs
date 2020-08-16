using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;

public class Sand : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            iBall ball=other.GetComponent<iBall>();
            if (ball != null)
            {
                ball.inSand = true;    
            }   
        }
        
        
    }
}
