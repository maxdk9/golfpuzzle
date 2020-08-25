using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;

public class Ball : iBall
{
    [SerializeField] public float moveDuration = .1f;
    public float waitForMoveDuration = .3f;

    [SerializeField] public bool docked = false;


    
    
    
    private SpriteRenderer spriteRenderer;
    private static Color dockedColor = Color.green;


    public override void Move(Vector2 direction)
    {
        if (docked)
        {
            return;
        }

        inSand = false;
        moved = true;
        StartCoroutine(MoveBallRoutine(direction));
    }

    private IEnumerator MoveBallRoutine(Vector2 direction)
    {
        moved = true;
        while (moved)
        {
            yield return new WaitForSeconds(moveDuration);
            moved = MoveBall(direction);
        }

        yield return new WaitForSeconds(waitForMoveDuration);
        ballStopped.Invoke();
    }

    private void Start()
    {
        
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private bool MoveBall(Vector2 direction)
    {
        // if (Mathf.Abs(direction.x) < .5f)
        // {
        //     direction.x = 0;
        // }
        //
        // if (Mathf.Abs(direction.y) < .5f)
        // {
        //     direction.y = 0;
        // }
        //
        //
        // direction.Normalize();
        if (isSunked)
        {
            transform.DOMove(transform.position + new Vector3(direction.x, direction.y), moveDuration);
            return false;
        }

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if (!docked && !inSand && !isSunked)
            
            {
                transform.DOMove(transform.position + new Vector3(direction.x, direction.y), moveDuration);
                return true;
            }
        }

        return false;
    }

    public void DestroySunk()
    {
        //this.StartCoroutine(ExlosionRoutine());
        
        //this.isSunked = true;
        //this.transform.position = new Vector3(-200, -200);
    }
    
    
    
   

    public void SetDocked(bool b)
    {
        docked = b;

        spriteRenderer.color = dockedColor;
        spriteRenderer.sortingOrder = 5;
    }

    public bool Moved()
    {
        return moved;
    }
    
   
}