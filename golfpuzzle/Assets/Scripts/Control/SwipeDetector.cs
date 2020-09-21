using System;
using UnityEngine;

namespace Control
{
    public class SwipeDetector: MonoBehaviour
    {
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;
        
        private Vector2 fingerDownPosition1=new Vector2();
        private Vector2 fingerUpPosition1=new Vector2();
        
        
        private float minDistanceForSwipe;
        private float aspectRatio;
        
        public static event Action<SwipeData> OnSwipe=delegate(SwipeData data) {  };


        private UIManager uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }


        private void Start()
        {
            aspectRatio = (float)Screen.width / (float)Screen.height;
            minDistanceForSwipe = (float)(Screen.width) * .2f;
            
            
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

                fingerDownPosition1.x = fingerDownPosition.x;
                fingerDownPosition1.y = fingerDownPosition.y;

                fingerUpPosition1.x = fingerUpPosition.x;
                fingerUpPosition1.y = fingerUpPosition.y;
                
                
                
                
                
                if (isVerticalSwipe())
                {
                    SwipeDirection direction = fingerDownPosition.y - fingerUpPosition.y > 0
                        ? SwipeDirection.up
                        : SwipeDirection.down;

                    string teststring =
                        String.Format("direction {0} Up = {1} Down {2}",direction.ToString(),fingerUpPosition1.ToString() , fingerDownPosition1.ToString());
                    uiManager.SetTestLabelText(teststring);
                    SendSwipe(direction);
                }
                else
                {
                    SwipeDirection direction = fingerDownPosition.x - fingerUpPosition.x > 0
                        ? SwipeDirection.right
                        : SwipeDirection.left;
                    
                    string teststring =
                        String.Format("direction {0} Up = {1} Down {2}",direction.ToString(),fingerUpPosition1.ToString() , fingerDownPosition1.ToString());
                    
                    
                    uiManager.SetTestLabelText(teststring);
                    
                    SendSwipe(direction);
                }
                fingerUpPosition = fingerDownPosition;
            }
        }

        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                Direction=direction,
                StartPosition = fingerDownPosition1,
                EndPosition = fingerUpPosition1
                    
                    
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

        public string GetDescription()
        {
            string res = "";
            res = String.Format("Direction : {0} ,start position x {1},y{2} ,fin position x {1},y{2}",
                Direction.ToString(), StartPosition.x, StartPosition.y, EndPosition.x, EndPosition.y);
            return res;
        }
    }

    public enum SwipeDirection
    {
        up,
        down,
        right,
        left
    }

    
    
    
}