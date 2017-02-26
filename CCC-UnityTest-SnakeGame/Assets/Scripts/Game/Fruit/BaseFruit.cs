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
    
    #region IObservable
    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }
    public void Subscribe(IObserver[] observers)
    {
        this.observers.AddRange(observers);
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    //this is not a IObservable, that is Unity function
    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        if (observers != null)
        {
            foreach (var observer in observers)
                observer.Notify();
        }
    }
    #endregion

    public void DestroyMe()//I don't want this to be overriden
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
