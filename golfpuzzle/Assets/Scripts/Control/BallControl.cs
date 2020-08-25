﻿using System;
using System.Collections;
using System.Collections.Generic;
using Control;
using MapObjects;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BallControl : MonoBehaviour
{
    
    public static UnityEvent BallMoveEvent=new UnityEvent();
    private bool dragging = false;


    private Vector3 startPosition;
    private Vector3 finPosition;

    private Vector3 startPositionTouch;
    private Vector3 finPositionTouch;
    
    
    private float aspectRatio;
    private float minXLength;
    private float minYLength;
    
    public string SolutionSave="";
    

    private void Start()
    {
        aspectRatio = (float)Screen.width / (float)Screen.height;
        minXLength = (float)(Screen.width) * .15f;
        minYLength = (float)(Screen.height) * .15f*aspectRatio;
    
      
        SwipeDetector.OnSwipe += MoveBallOnDirection;
    }


    private void Update()
    {
    #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            moveInput.y = 0;
        }
        else
        {
            moveInput.x = 0;
        }

        //Vector2 moveInput=new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput.Normalize();
        if (moveInput.SqrMagnitude() > .5f)
        {
            if (GameManager.Instance.mReadyForInput)
            {
                GameManager.Instance.mReadyForInput = false;
                GameManager.Instance.MoveCounter++;
                GameManager.Instance.ResetBalls();
                AddMoveToSolutionSave(moveInput);
                BallMoveEvent.Invoke();
                foreach (iBall iBall in GameManager.Instance.Balls)
                {
                    iBall.Move(moveInput);
                }

                UIManager.UpdateMoveCounterEvent.Invoke();
            }
        }
    #endif

#if UNITY_ANDROID && !UNITY_EDITOR
                if (GameManager.Instance.mReadyForInput)
                {
                    //OnTouch();
                }
#endif
    }

    // private void OnTouch()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //
    //         // Move the cube if the screen has the finger moving.
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //               startPositionTouch = touch.position;
    //               _followTrail.transform.position = startPositionTouch;
    //         }
    //         
    //         if (touch.phase == TouchPhase.Ended)
    //         {
    //             
    //             GameManager.Instance.mReadyForInput = false;
    //             finPositionTouch = touch.position;
    //             
    //             Vector2 moveInput = finPositionTouch-startPositionTouch;
    //             if((Mathf.Abs(moveInput.x)<=minXLength)&&(Mathf.Abs(moveInput.y)<=minYLength))
    //             {
    //                 
    //                 GameManager.Instance.mReadyForInput = true;
    //                 return;
    //             }
    //
    //             if  (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y*aspectRatio))
    //             {
    //                 moveInput.y = 0;
    //                 
    //             }
    //             else
    //             {
    //                 moveInput.x = 0;
    //             }
    //             
    //             moveInput.Normalize();
    //             if (moveInput.SqrMagnitude() > .5f)
    //             {
    //                 
    //                 GameManager.Instance.SetBalls();
    //                 GameManager.Instance.MoveCounter++;
    //                 GameManager.Instance.uiManager.TestLabel.text = "Ball number = "+GameManager.Instance.Balls.Length;
    //                 
    //                 BallMoveEvent.Invoke();
    //                 AddMoveToSolutionSave(moveInput);
    //                 foreach (iBall iBall in GameManager.Instance.Balls)
    //                 {           
    //                     iBall.Move(moveInput);    
    //                     _followTrail.Move(moveInput);
    //                 }
    //                 UIManager.UpdateMoveCounterEvent.Invoke();   
    //             }
    //             else
    //             {
    //                 GameManager.Instance.mReadyForInput = true;
    //             }
    //         }
    //
    //         
    //     }
    // }


    public void MoveBallOnDirection(SwipeData swipeData)
    {

        Vector2 direction=GetDirection(swipeData.Direction);
        GameManager.Instance.mReadyForInput = false;
        GameManager.Instance.SetBalls();
        GameManager.Instance.MoveCounter++;
        GameManager.Instance.uiManager.TestLabel.text = "Ball number = "+GameManager.Instance.Balls.Length;
                    
        BallMoveEvent.Invoke();
        AddMoveToSolutionSave(direction);
        foreach (iBall iBall in GameManager.Instance.Balls)
        {           
            iBall.Move(direction);    
            
        }
        UIManager.UpdateMoveCounterEvent.Invoke();
    }

    private Vector2 GetDirection(SwipeDirection swipeDirection)
    {
        if (swipeDirection == SwipeDirection.up)
        {
            return Vector2.up;
        }
        if (swipeDirection == SwipeDirection.right)
        {
            return Vector2.right;
        }
        if (swipeDirection == SwipeDirection.left)
        {
            return Vector2.left;
        }
        if (swipeDirection == SwipeDirection.down)
        {
            return Vector2.down;
        }

        return Vector2.zero;
    }


    private void AddMoveToSolutionSave(Vector2 moveInput)
    {
        string move="";
        if (moveInput.Equals(Vector2.down))
        {
            move = "s";
        }
        
        if (moveInput.Equals(Vector2.up))
        {
            move = "n";
        }
        
        if (moveInput.Equals(Vector2.left))
        {
            move = "w";
        }
        
        if (moveInput.Equals(Vector2.right))
        {
            move = "e";
        }

        SolutionSave =SolutionSave+ move;
        
    
    }
}

        
    

 
