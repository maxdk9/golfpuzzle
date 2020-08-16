using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MapObjects
{
    public class iBall : MonoBehaviour
    {
        public bool moved;

        public bool isSunked;
        public bool inSand;
   
        
        public GameObject particleExplosion;
        public float durationExplosion=.5f;
        public UnityEvent ballStopped = new UnityEvent();

        public virtual void Move(Vector2 direction)
        {
        }

        // public virtual void DestroySunk()
        // {
        // }

        public void ResetBall()
        {
            moved = false;
            inSand = false;
        }
        

        public bool Blocked(Vector2 position, Vector2 direction)
        {
            if (isSunked)
            {
                return false;
            }
            Vector2 newPos = new Vector2(position.x, position.y) + direction;
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (var VARIABLE in walls)
            {
                if (VARIABLE.transform.position.x == newPos.x && VARIABLE.transform.position.y == newPos.y)
                {
                    return true;
                }
            }


            GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
            foreach (var VARIABLE in gates)
            {
                Gate gateComponent = VARIABLE.GetComponent<Gate>();
                if (gateComponent == null)
                {
                    continue;
                }

                if (gateComponent.mOpen)
                {
                    continue;
                }

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
                    if (ball)
                    {
                        if (ball.docked)
                        {
                            continue;
                        }

                        if (ball.inSand)
                        {
                            return true;
                        }

                        if (isSunked)
                        {
                            return false;
                        }
                        
                        
                        if (ball.Blocked(ball.transform.position, direction))
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
                        if (grayBall.inSand)
                        {
                            return true;
                        }
                        if (grayBall.Blocked(grayBall.transform.position, direction))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    MetalBlock metalBall = VARIABLE.gameObject.GetComponent<MetalBlock>();
                    if (metalBall)
                    {
                        // if (metalBall.moveDone)
                        // {
                        //     return false;
                        // }
                        if (metalBall.inSand)
                        {
                            return true;
                        }
                        if (metalBall.Blocked(metalBall.transform.position, direction))
                        {
                            return true;
                        }
                        if (metalBall.startPosition.x == metalBall.transform.position.x &&
                            metalBall.startPosition.y == metalBall.transform.position.y)
                        {
                            return false;
                        }
                        return true;


                        
                    }
                    
                    Bomb bomb = VARIABLE.gameObject.GetComponent<Bomb>();
                    if (bomb)
                    {
                        if (bomb.inSand)
                        {
                            return true;
                        }
                        
                        if (bomb.BombIsMoved)
                        {
                            return false;
                        }
                        
                        
                        if (bomb.Blocked(bomb.transform.position, direction))
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


        public void Explode()
        {
            StartCoroutine(ExlosionRoutine());
        }
        
        
        private IEnumerator ExlosionRoutine()
        {
            this.moved = false;
            this.isSunked = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.particleExplosion.SetActive(true);
            yield return new WaitForSeconds(durationExplosion);
            GameObject.Destroy(gameObject);
        
        }
    }
}