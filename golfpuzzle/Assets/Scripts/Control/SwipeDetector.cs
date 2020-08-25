using System;
using UnityEngine;

namespace Control
{
    public class SwipeDetector: MonoBehaviour
    {
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;
        private float minDistanceForSwipe;
        private float aspectRatio;
        
        public static event Action<SwipeData> OnSwipe=delegate(SwipeData data) {  }; 
        

        private void Start()
        {
            aspectRatio = (float)Screen.width / (float)Screen.height;
            minDistanceForSwipe = (float)(Screen.width) * .15f;
            
            
        }

        private void Update()
        {
            if (!GameManager.Instance.mReadyForInput)
            {
                return;
            }
            foreach (Touch touch in Input.touches)
            {
                
                if (touch.phase == TouchPhase.Began)
                {
                    fingerDownPosition = touch.position;
                    fingerUpPosition = touch.position;   
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }


                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceSheckMet())
            {
                if (isVerticalSwipe())
                {
                    SwipeDirection direction = fingerDownPosition.y - fingerUpPosition.y > 0
                        ? SwipeDirection.up
                        : SwipeDirection.down;

                    SendSwipe(direction);
                }
                else
                {
                    SwipeDirection direction = fingerDownPosition.x - fingerUpPosition.x > 0
                        ? SwipeDirection.right
                        : SwipeDirection.left;
                    SendSwipe(direction);
                }
                
            }
        }

        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                Direction=direction,
                StartPosition = fingerDownPosition,
                EndPosition = fingerUpPosition
                    
                    
            };
            OnSwipe(swipeData);
        }

        

        private bool SwipeDistanceSheckMet()
        {
            return VerticalMovementDistance() > minDistanceForSwipe ||
                   HorizontalMovementDistance() > minDistanceForSwipe;
        }

        private bool isVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.x-fingerUpPosition.x);
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        }
    }



    public class SwipeData
    {
        public SwipeDirection Direction;
        public Vector2 StartPosition;
        public Vector2 EndPosition;

    }

    public enum SwipeDirection
    {
        up,
        down,
        right,
        left
    }

    
    
    
}