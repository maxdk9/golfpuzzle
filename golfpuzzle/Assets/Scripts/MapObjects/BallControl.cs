using System;
using System.Collections;
using System.Collections.Generic;
using MapObjects;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private bool dragging = false;


    private Vector3 startPosition;
    private Vector3 finPosition;

    private Vector3 startPositionTouch;
    private Vector3 finPositionTouch;
    
    
    private float aspectRation;
    private float minXLength;
    private float minYLength;
    public BoxCollider2D boxCollider2d;
    

    private void Start()
    {
        aspectRation = Screen.width / Screen.height;
        minXLength = (Screen.width) * .1f;
        minYLength = (Screen.height) * .1f;
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    
    
    

    


    
    
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        //Vector2 moveInput=new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput.Normalize();
        if (moveInput.SqrMagnitude() > .5f)
        {
            if (GameManager.Instance.mReadyForInput)
            {
                GameManager.Instance.mReadyForInput = false;
                GameManager.Instance.MoveCounter++;
                foreach (iBall iBall in GameManager.Instance.Balls)
                {           
                    iBall.Move(moveInput);    
                }
                UIManager.UpdateMoveCounterEvent.Invoke();       
            }
        }
        else
        {
            //mReadyForInput = true;
        }
        #if UNITY_ANDROID
                if (GameManager.Instance.mReadyForInput)
                {
                    OnTouch();
                }
        #endif
        
    }

    private void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Began)
            {
                startPositionTouch = touch.position;
                GameManager.Instance.uiManager.TestLabel.text = "SP;X= " + startPositionTouch.x + " Y= " + startPositionTouch.y;
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                finPositionTouch = touch.position;
                GameManager.Instance.uiManager.TestLabel.text = "FP;X= " + finPositionTouch.x + " Y= " + finPositionTouch.y;
                Vector2 moveInput = finPositionTouch-startPositionTouch;
                if((Mathf.Abs(moveInput.x)<=minXLength)&&(Mathf.Abs(moveInput.y)<=minYLength))
                {
                    return;
                }
            
                moveInput.Normalize();
                if (moveInput.SqrMagnitude() > .5f)
                {
                    GameManager.Instance.mReadyForInput = false;
                    GameManager.Instance.MoveCounter++;
                    foreach (iBall iBall in GameManager.Instance.Balls)
                    {           
                        iBall.Move(moveInput);    
                    }
                    UIManager.UpdateMoveCounterEvent.Invoke();   
                }
            }

            
        }
    }
    }

        
    

 

