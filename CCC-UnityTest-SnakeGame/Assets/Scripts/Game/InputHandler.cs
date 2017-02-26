﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    private Command left, right, top, down;
    private SnakeHead player;
    float startTime = 0;
    Vector2 swipeStartPos;
    float swipeDistanceThreshold;
    float swipeTimeThreshold;

    public Text debugText;

    void Awake()
    {
        enabled = false;
    }

    internal void Init(SnakeHead head)
    {
        player = head;
        enabled = true;
    }

    // Use this for initialization
    void Start () {
        left = new MoveLeftCommand();
        right = new MoveRightCommand();
        top = new MoveTopCommand();
        down = new MoveDownCommand();
    }
	
	// Update is called once per frame
	void Update () {
        HandleInputs();
        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                swipeStartPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                //now we need to know the magnitude and direction of movment , and whether it happens under the threshold time
                float timeTaken = Time.time - startTime;
                Vector2 subVector = (touch.position - swipeStartPos);
                float distanceMoved = subVector.magnitude;
                double AngleOfMovemnt = Math.Atan2(subVector.y, subVector.x) * Mathf.Rad2Deg;

                //debugText.text = (AngleOfMovemnt)+ " ˚";
                if (AngleOfMovemnt > -45 && AngleOfMovemnt < 45)
                    right.Execute(player);
                else if (AngleOfMovemnt >= 45 && AngleOfMovemnt < 135)
                    top.Execute(player);
                else if (AngleOfMovemnt >= 135 || AngleOfMovemnt < -135)
                    left.Execute(player);
                else if (AngleOfMovemnt >= -135)
                    down.Execute(player);
            }
            //TODO: handle touch pahse cancel if needed

        }
    }

    private void HandleInputs()
    {
        //TODO:use touch swipes
        if (Input.GetKeyDown(KeyCode.A))
        {
            left.Execute(player);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            right.Execute(player);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            down.Execute(player);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            top.Execute(player);
        }
        
    }
}