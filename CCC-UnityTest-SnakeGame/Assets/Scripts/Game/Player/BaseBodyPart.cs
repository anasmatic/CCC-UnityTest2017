using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBodyPart : MonoBehaviour, ISnakeBodyPart,IObservable
{
    internal BaseBodyPart previousPart = null;//the part in front of me in the direction to the head
    internal BaseBodyPart nextPart = null;//the part behind me in the direction to the head
    internal int timeCounter = 0;
    internal Vector3 lastPossition;
    internal Vector3 newPossition;
    private List<IObserver> observers = new List<IObserver>();

    internal int direction;

    // Use this for initialization
    internal void Start () {
        newPossition = lastPossition = transform.localPosition;
        Subscribe(Map.Instance);
    }
	
	// Update is called once per frame
	internal virtual void Update () {
        
    }
    /*
        only head deserves to handle collisions,
        no other body parts need to .
    */
    public virtual bool CollideHandler()
    {
        return false;
    }

    /*
        this function triggerd from Input handler
    */
    public virtual void MovementHandler(int direction)
    {
        if (Math.Abs(direction) == Math.Abs(this.direction)) return;

        this.direction = direction;
        MovementHandler();
    }

    public virtual void MovementHandler()
    {
        //do chain reaction along the linked list\

        if (observers != null)
        {
            foreach (var observer in observers)
                observer.NotifyWith(newPossition, lastPossition);
        }
        if (nextPart)
            nextPart.MovementHandler();
    }

    #region Linked List helper
    //the part infront of me (to head direction)
    internal void SetPreviousPart(BaseBodyPart snakeBodyPart)
    {
        previousPart = snakeBodyPart;
    }
    //the part behind me (to tail direction)
    public virtual void SetNextPart(BaseBodyPart snakeBodyPart)
    {
        nextPart = snakeBodyPart;
    }
    #endregion

    #region IObservable
    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Subscribe(IObserver[] observer){}

    public void Unsubscribe(IObserver observer){}
    #endregion

    /*
    works from head to tail
    */
    public void DestroyPart()
    {
        if (nextPart)
            nextPart.DestroyPart();
        Destroy(gameObject);
    }
}
