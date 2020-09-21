using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MapObjects;
using UnityEngine;

public class Bomb : iBall
{
    [SerializeField] public float moveDuration = .001f;
    public float waitForMoveDuration = .3f;
    private SpriteRenderer spriteRenderer;

    
    
    public bool BombIsMoved;

    private bool Exploded = false;
    public override void Move(Vector2 direction)
    {
        inSand = false;
        moved = true;
        BombIsMoved = false;
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
           
           
           
           

            if (inSand)
            {
                return false;
            }
            
               if (BombIsMoved)
                {
                    transform.DOMove(transform.position + new Vector3(direction.x, direction.y) * .5f, moveDuration);
                }
            

            return false;
        }
        else
        {
            if ( !inSand && !isSunked)
            {
                BombIsMoved = true;
                transform.DOMove(transform.position + new Vector3(direction.x, direction.y), moveDuration);
                return true;
            }
        }

        return false;
    }



    
    public void DestroySunk()
    {
        this.isSunked = true;
        this.transform.position = new Vector3(-200, -200);
    }

    

    public bool Moved()
    {
        return moved;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.Exploded)
        {
            return;
        }
        
        if (other.transform.tag.CompareTo("Wall") == 0)
        {
            iWall wall = other.gameObject.GetComponent<iWall>();
            if (wall == null)
            {
                return;
            }

            moved = false;
            
            
            WallDestructable destructable=other.GetComponent<WallDestructable>();
            if (destructable != null)
            {
                destructable.Explode();
                //GameObject.Destroy(destructable.gameObject);    
            }

            //this.Exploded = true;
            this.StartCoroutine(ExlosionRoutine());
            
        }
        
        
        
        
        
        
        
        
        if (other.transform.tag.CompareTo("Ball") == 0)
        {
            Bomb tBomb = other.GetComponent<Bomb>();
            if (tBomb)
            {
                if (tBomb.Exploded)
                {
                    return;
                }
            }
            
            iBall ball = other.gameObject.GetComponent<iBall>();
            if (ball == null)
            {
                return;
            }


            //this.Exploded = true;
            moved = false;
            GameObject.Destroy(ball.gameObject);    
            

            this.StartCoroutine(ExlosionRoutine());
            
        }
        
        
        if (other.transform.tag.CompareTo("Gate")==0)
        {
            moved = false;
            this.StartCoroutine(ExlosionRoutine());   
        }
        
        
        
        
    }

    private IEnumerator ExlosionRoutine()
    {
        this.Exploded = true;
        this.BombIsMoved = false;
        this.GetComponent<SpriteRenderer>().enabled = false;

        
        GameObject explosion = Instantiate(GameManager.Instance.mPrefabDatabase.RedExplosionPrefab,
            this.transform.position, Quaternion.identity);
        
        
        // this.particleExplosion.SetActive(true);
        // yield return new WaitForSeconds(durationExplosion);
        GameObject.Destroy(gameObject);
        yield return null;
    }
}
