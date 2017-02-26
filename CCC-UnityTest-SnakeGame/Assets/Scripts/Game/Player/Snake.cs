using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
snake class has a sort of LinkedList/Graph structure,
for sake of performance we will be using array to store all body parts, 
and evey podypart (Node in graph) will be linked with previous body part
*/
public class Snake : MonoBehaviour , IObserver{

    public SnakeBodyPart bodyPartPrefab;
    public SnakeHead headPrefab;
    //the mind of the hero !
    private SnakeHead head;
    public const int maxLength = 10;//should be populated from Level class, it is as same as the number of collectables in this class + 3(initial pody parts)


    internal void Clear()
    {
        BaseBodyPart part = GetHead();
        if (part)
            part.DestroyPart();
        
    }



    internal void Init(int width, int height)
    {
        //init snake, create head and 2 parets
        head = Instantiate(headPrefab, transform);
        head.transform.localPosition = new Vector3((int)(width*.5),0, (int)(height * .5));
        int randomDirection = GetRandomDirection();
        head.Init(randomDirection);
        //snakeBody.Add(head);
        head.enabled = false;
        for (int i = 1; i < 3; i++)
        {
            Vector3 pos = head.transform.localPosition;// lastPossition;
            if (randomDirection == Constants.RIGHT)
                pos.x -= i;
            else if (randomDirection == Constants.LEFT)
                pos.x += i;
            else if (randomDirection == Constants.TOP)
                pos.z -= i;
            else if (randomDirection == Constants.DOWN)
                pos.z += i;

            SnakeBodyPart part = Instantiate(bodyPartPrefab, transform);
            part.transform.localPosition = pos;
            BaseBodyPart tail = GetTail();
            part.SetPreviousPart(tail);//just find the last element in the linked list, attch ur self to it
            tail.SetNextPart(part);//get recognized by the tail
            //snakeBody.Add(part);//now add your self to the array
        }
    }

    private int GetRandomDirection()
    {
        var dirArr = new int[] { Constants.LEFT, Constants.RIGHT, Constants.TOP, Constants.DOWN };
        int index = new System.Random().Next(dirArr.Length);
        return dirArr[index];
    }

    public void Notify()
    {
        //create new snake body part and add it
        AddPartToBody();
    }

    private void AddPartToBody()
    {
        SnakeBodyPart part = Instantiate(bodyPartPrefab, transform);
        BaseBodyPart currentTail = GetTail();
        part.transform.localPosition = currentTail.lastPossition;
        part.SetPreviousPart(currentTail);//previous means the next part to the direction of head from tail
        currentTail.SetNextPart(part);
    }

    public SnakeHead GetHead()
    {
        return head;
    }
    private BaseBodyPart GetTail()
    {
        BaseBodyPart part = head;
        while (part && part.nextPart)
            part = part.nextPart;
        return part;
    }

    public void NotifyWith(Vector3 position1, Vector3 position2){}
}
