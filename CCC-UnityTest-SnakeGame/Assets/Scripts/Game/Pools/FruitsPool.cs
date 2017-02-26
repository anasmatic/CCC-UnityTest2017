using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsPool : MonoBehaviour {


    public Queue<BaseFruit> pool;
    private BaseFruit currentFruit;
    public int POOL_SIZE = 2;
    public bool canExpandPool = false;
    public BaseFruit redFruitPrefab;

    private IObserver[] observers;

    private static FruitsPool instance;
    public static FruitsPool Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(FruitsPool)) as FruitsPool;

            if (instance == null) //still null ?!
                throw new System.NullReferenceException("FruitsLoop need to be added to game object on scene");

            return instance;
        }
    }

    // Use this for initialization
    public void InitPool (IObserver[] observers) {
        this.observers = observers;
        pool = new Queue<BaseFruit>(POOL_SIZE);
        BaseFruit fruit;
        for(int i = 0; i< POOL_SIZE; i++)
        {
            fruit = Instantiate(redFruitPrefab,transform);
            fruit.Subscribe(observers);
            fruit.gameObject.SetActive(false);
            pool.Enqueue(fruit);
        }
	}

    internal void PauseCurrent()
    {
        if(currentFruit)
            currentFruit.enabled = false;
    }

    internal void resumeCurrent()
    {
        if (currentFruit)
            currentFruit.enabled = true;
    }

    internal void hideCurrent()
    {
        if (currentFruit)
            currentFruit.gameObject.SetActive(false);
    }

    public BaseFruit GetFruitFromPool()
    {
        currentFruit = null;
        if (pool.Peek().gameObject.activeInHierarchy) {//is in use
            if (canExpandPool) {//the pool is not restricted
                //then create
                currentFruit = Instantiate(redFruitPrefab);
                currentFruit.Subscribe(observers);
                pool.Enqueue(currentFruit);//add it the end
            }
            //yourFruit is still null
        }else {
            currentFruit = pool.Dequeue();
            pool.Enqueue(currentFruit);//imidiatly add it back to the end of the queue
        }
        return currentFruit;
    }

}
