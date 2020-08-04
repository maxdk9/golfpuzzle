using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapObjects;
using UnityEngine;
using UnityEngine.Events;

public class Ball : iBall
{

    [SerializeField]
    public float moveDuration = .001f;

    public float waitForMoveDuration = .3f;
    
    [SerializeField] public bool docked = false;
    public bool inSand = false;
    

    private SpriteRenderer spriteRenderer;
    private static Color dockedColor=Color.yellow;


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


    // public void Move(Vector2 direction)
    // {
    //     if (docked)
    //     {
    //         return;
    //     }
    //
    //     inSand = false;
    //     moved = true;
    //     StartCoroutine(MoveBallRoutine(direction));
    //     
    // }

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
        if (Mathf.Abs(direction.x) < .5f)
        {
            direction.x = 0;
        }

        if (Mathf.Abs(direction.y) < .5f)
        {
            direction.y = 0;
        }
        
        
        direction.Normalize();
        
        
        
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            
            
            if (!docked&&!inSand&&!isSunked)
            {
                transform.DOMove(transform.position + new Vector3(direction.x,direction.y), moveDuration);
                return true;    
            }
            
        }

        return false;
    }


    public bool Blocked(Vector2 position, Vector2 direction)
    {
        
        Vector2 newPos=new Vector2(position.x,position.y)+direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var VARIABLE in walls)
        {
            if (VARIABLE.transform.position.x == newPos.x && VARIABLE.transform.position.y == newPos.y)
            {
                return true;
            }
        }


        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var VARIABLE in boxes)
        {
            if (VARIABLE.transform.position.x == newPos.x && VARIABLE.transform.position.y == newPos.y)
            {
                Ball ball = VARIABLE.gameObject.GetComponent<Ball>();
                if (ball )
                {
                    if (ball.docked)
                    {
                        continue;
                    }
                    if (ball.Blocked(ball.transform.position,direction))
                    {
                        return true;    
                    }
                    else
                    {
                        return false;
                    }
                }

                GrayBall grayBall = VARIABLE.gameObject.GetComponent<GrayBall>();
                if (grayBall)
                {
                    if (grayBall.Blocked(grayBall.transform.position,direction))
                    {
                        return true;    
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            
        }
        
        
        
        return false;
    }

    public void DestroySunk()
    {
        this.isSunked = true;
        this.transform.position=new Vector3(-200,-200);
    }

    public void SetDocked(bool b)
    {
        docked = b;
        
        spriteRenderer.color = dockedColor;
        spriteRenderer.sortingOrder = 1;
    }

    public bool Moved()
    {
        return moved;
    }
}
