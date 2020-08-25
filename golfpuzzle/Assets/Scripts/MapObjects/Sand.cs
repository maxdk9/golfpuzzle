using System;
using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;

public class Sand : MonoBehaviour
{

    public static UnityEvent EnterToSandEvent=new UnityEvent();
    
    
    public Color SandColor = Color.yellow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            iBall ball=other.GetComponent<iBall>();
            if (ball != null)
            {
                EnterToSandEvent.Invoke();
                ball.inSand = true;
                SpriteRenderer spriteRenderer = ball.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = SandColor;
                }
            }   
        }
        
        
    }

    


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            iBall ball=other.GetComponent<iBall>();
            if (ball != null)
            {
                SpriteRenderer spriteRenderer = ball.gameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.white;
                }
            }   
        }
    }
}
