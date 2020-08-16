using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapObjects;
using UnityEngine;

public class GrayBall : iBall
{
    [SerializeField]
    public float moveDuration = .05f;

    public float waitForMoveDuration = .3f;


    
    
    public override void Move(Vector2 direction)
    {

        

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
    
    
    private bool MoveBall(Vector2 direction)
    {
        
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
            if(!this.isSunked&&!inSand){
                transform.DOMove(transform.position + new Vector3(direction.x,direction.y), moveDuration);
                return true;    
            }
                
            
            
        }

        return false;
    }
    
    
    
  

   
}
