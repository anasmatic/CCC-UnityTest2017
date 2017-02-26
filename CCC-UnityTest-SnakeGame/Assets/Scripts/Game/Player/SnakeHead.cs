using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BaseBodyPart {

    public LayerMask wallsLayer;
    public LayerMask playerLayer;

    /*
    //using OnTriggerEnter or OnCollisionEnter is not the best way to trigger collision in "step base movment"
    //it is better to raycast/linecast the next move position, and see if it is allowed
    //this way we will have pre collision system, we can make use of it when we add more features ot the game
    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        print("COLLISION");
    }
    */

        //init but don't move yet
    internal void Init(int direction)
    {
        this.direction = direction;
        lastPossition = transform.position;
        RotateToDirection();
    }
    //the head has eyes, and though eye should be always infront
    private void RotateToDirection()
    {
        transform.rotation = Quaternion.identity;
        var rot = GetRotationVectorWithRespectToDirection();
        transform.Rotate( rot.x,rot.y,rot.z);
    }

    //update function can be replaced later with a (tick) function,
    //something like StartCoroutine , that handels smooth animation for the snake
    internal override void Update()
    {
        timeCounter++;
        if (timeCounter >= Constants.TICK_SPEED)
        {
            if (!CollideHandler())//no collisions
            {
                MovementHandler();
            }
            timeCounter = 0;
        }
    }

    //this function translates directon to actual position
    public override void MovementHandler()
    {
        switch (direction)
        {
            case Constants.LEFT:
                StepLeft();
                break;
            case Constants.RIGHT:
                StepRight();
                break;
            case Constants.TOP:
                StepTop();
                break;
            case Constants.DOWN:
                StepDown();
                break;
            default:
                //do nothing
                break;
        }

        //check before move for collision
        if (CanMove()){
            //TODO:animate movement using currenPossition
            transform.localPosition = newPossition;
            RotateToDirection();
            base.MovementHandler();
        }else {
            //TODO: dead effect or animation
        }
    }

    private void StepLeft()
    {
        newPossition.x--;
    }
    private void StepRight()
    {
        newPossition.x++;
    }
    private void StepTop()
    {
        newPossition.z++;
    }
    private void StepDown()
    {
        newPossition.z--;
    }


    private bool CanMove()
    {
        //if (now)
        timeCounter = 0;// Constants.TICK_SPEED;
        lastPossition = transform.localPosition;

        if(CheckIfTHisMoveLeadToHit( newPossition))
        {
            EventManager.TriggerEvent(EventManager.GAME_OVER);
            enabled = false;
            return false;
        }
        return true;
    }

    private bool CheckIfTHisMoveLeadToHit(Vector3 newPossition)
    {
        Vector3 directionAsVector = GetDirectionAsVector();
        //Debug.DrawRay(transform.position, directionAsVector, Color.cyan, 1);
        //check for collision with walls and player
        bool isHit = Physics.Raycast(transform.position, directionAsVector, 1, wallsLayer|playerLayer);
        return isHit;
    }

    //translate string const direction to basic vector direction
    private Vector3 GetDirectionAsVector()
    {
        Vector3 directionAsVector = Vector3.forward; ;
        
        switch (direction)
        {
            case Constants.LEFT:
                directionAsVector = Vector3.left;
                break;
            case Constants.RIGHT:
                directionAsVector = Vector3.right;
                break;
            case Constants.TOP:
                directionAsVector = Vector3.forward;
                break;
            case Constants.DOWN:
                directionAsVector = Vector3.back;
                break;
            default:
                //do nothing
                break;
        }
        
        return directionAsVector;
    }

    //helper to rotate the head
    private Vector3 GetRotationVectorWithRespectToDirection()
    {
        Vector3 directionAsVector = Vector3.zero;

        switch (direction)
        {
            case Constants.LEFT:
                directionAsVector.y = 270;
                break;
            case Constants.RIGHT:
                directionAsVector.y = 90;
                break;
            case Constants.TOP:
                directionAsVector.y = 0;
                break;
            case Constants.DOWN:
                directionAsVector.y = 180;
                break;
            default:
                //do nothing
                break;
        }

        return directionAsVector;
    }
}
