using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFruit : MonoBehaviour,IFruit, IObservable
{
    public int lifeTime = 0;
    private List<IObserver> observers = new List<IObserver>();
    protected Vector3 startPos;

    protected virtual void OnEnable()
    {
        startPos = transform.localPosition;
        Invoke("DestroyMe", lifeTime);
    }

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

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        if (observers != null)
        {
            foreach (var observer in observers)
                observer.Notify();
        }
    }

    public virtual void animate()
    {

    }

    public void DestroyMe()//I don't want this to be overriden
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        animate();
    }


    void OnDisable()
    {
        CancelInvoke("DestroyMe");
    }

}
