using System;
using System.Collections;
using System.Collections.Generic;
using MapObjects;
using UnityEngine;

public class Key : MonoBehaviour
{

    public bool activated;

    private iBall activeBall;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        iBall ball = other.gameObject.GetComponent<iBall>();
        if (ball == null)
        {
            return;
        }

        activeBall = ball;
        activated = true;
        GameManager.KeyActivatedEvent.Invoke();
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        iBall ball = other.gameObject.GetComponent<iBall>();
        if (ball == null)
        {
            return;
        }

        if (ball == activeBall)
        {
            activated = false;    
        }
        
        GameManager.KeyActivatedEvent.Invoke();
    }
}
