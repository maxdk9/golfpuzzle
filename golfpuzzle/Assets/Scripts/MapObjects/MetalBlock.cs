using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapObjects;
using UnityEngine;

public class MetalBlock : iBall
{
    [SerializeField]
    public float moveDuration = .05f;

    public float waitForMoveDuration = .3f;
    public Vector2 startPosition;

    
    
    public override void Move(Vector2 direction)
    {


    
        moved = true;
        StartCoroutine(MoveBallRoutine(direction));
    }

    private IEnumerator MoveBallRoutine(Vector2 direction)
    {
        startPosition = this.transform.position;
        moved = true;
        while (moved)
        {
            yield return new WaitForSeconds(moveDuration);
            moved = MoveBall(direction);    
        }
        yield return new WaitForSeconds(waitForMoveDuration);
        ballStopped.Invoke();
        
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
        
        
        
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            if(!this.isSunked){
                transform.DOMove(transform.position + new Vector3(direction.x,direction.y), moveDuration);
                return false;    
            }
        }

        return false;
    }
    
    
    
    

    
}
