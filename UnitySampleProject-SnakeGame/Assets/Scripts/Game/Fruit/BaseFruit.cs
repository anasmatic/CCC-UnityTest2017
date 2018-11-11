using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFruit : MonoBehaviour,IFruit, IObservable
{
    //cooldown and life time
    public int lifeTime = 0;
    private List<IObserver> observers = new List<IObserver>();
    protected Vector3 startPos;

    protected virtual void OnEnable()
    {
        startPos = transform.localPosition;
        Invoke("DestroyMe", lifeTime);
    }

    //Observers subscriptions happens upon game pool initialization 
    #region IObservable
    public void Subscribe(IObserver observer)
    {
        if(observers.Count == 0)
            observers.Add(observer);
    }
    public void Subscribe(IObserver[] _observers)
    {
        if(this.observers.Count == 0){
            this.observers.AddRange(_observers);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }
    #endregion

    //on Collision , Notify your observers, 
    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        if (observers != null)
        {
            foreach (var observer in observers)
                observer.Notify();
        }
    }

    public void DestroyMe()
    {
        gameObject.SetActive(false);
    }


    //every fruit should have its own animation in it class
    public virtual void animate() { }
    // no need to override update in children, just override animate()
    void Update()
    {
        animate();
    }


    void OnDisable()
    {
        CancelInvoke("DestroyMe");
    }

}
