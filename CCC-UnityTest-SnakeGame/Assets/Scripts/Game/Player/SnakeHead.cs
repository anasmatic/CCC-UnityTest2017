using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : BaseBodyPart {

    public LayerMask wallsLayer;
    public LayerMask playerLayer;

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        print("COLLISION");
    }

    internal void Init(int direction)
    {
        this.direction = direction;
        lastPossition = transform.position;
        RotateToDirection();
    }

    private void RotateToDirection()
    {
        transform.rotation = Quaternion.identity;
        var rot = GetRotationVectorWithRespectToDirection();
        transform.Rotate( rot.x,rot.y,rot.z);
    }

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

    public override void MovementHandler()
    {
        //check before move for collision
        
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

        if (CanMove()){
            //TODO:animate movement using currenPossition
            transform.localPosition = newPossition;
            RotateToDirection();
            base.MovementHandler();
        }else {
            //TODO: dead eefect or animation
            //TODO: event trigger to Game handler, that you are dead
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
            //TODO: can't move, die mother fucker
            print("CheckIfTHisMoveLeadToHit TRUE");
            print("________________ GameOVer __________________");
            EventManager.TriggerEvent(EventManager.GAME_OVER);
            enabled = false;
            return false;
        }
        return true;
    }

    private bool CheckIfTHisMoveLeadToHit(Vector3 newPossition)
    {
        RaycastHit hit;
        //print(transform.position + " , " + transform.TransformPoint(newPossition));
        //Debug.DrawLine(transform.position, transform.TransformPoint(newPossition), Color.cyan,1);
        //LineCast works in World positions, so we need to convert the local point newPosition to world
        //bool isHit = Physics.Linecast(transform.TransformPoint(transform.localPosition), transform.TransformPoint(newPossition), out hit, wallsLayer);
        //print(newPossition+ ","+ ((hit.collider != null)?hit.collider.name:"null"));
        Vector3 directionAsVector = GetDirectionAsVector();
            
            //Vector3.forward; ;
        
        Debug.DrawRay(transform.position, directionAsVector, Color.cyan, 1);
        bool isHit = Physics.Raycast(transform.position, directionAsVector, 1, wallsLayer|playerLayer);
        return isHit;
    }

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
