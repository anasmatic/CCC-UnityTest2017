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

    //happens on game start, controlled by FruitsFactory
    public void InitPool (IObserver[] observers) {
        //save observers for later ( handling empty pool)
        this.observers = observers;
        //init pool as queue
        pool = new Queue<BaseFruit>(POOL_SIZE);
        //create as many fruits as pool size 
        BaseFruit fruit;
        for(int i = 0; i< POOL_SIZE; i++)
        {
            //create from prefab
            fruit = Instantiate(redFruitPrefab,transform);
            //add observers that will be notified upon collision
            fruit.Subscribe(observers);
            //deactivate visibility
            fruit.gameObject.SetActive(false);
            //add to pool
            pool.Enqueue(fruit);
        }
	}

    internal void PauseCurrent()
    {
        if(currentFruit)
            currentFruit.enabled = false;
    }

    internal void ResumeCurrent()
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
            pool.Enqueue(currentFruit);//immediately add it back to the end of the queue
        }
        return currentFruit;
    }

    #if UNITY_EDITOR
        //used by GamePlayTests class
        public void ConstructForUnityTest(int poolSize){
            GameObject fruitPrefab = (GameObject) Resources.Load("Prefabs/Fruit");
            this.redFruitPrefab = fruitPrefab.GetComponent<BaseFruit>();
            POOL_SIZE = poolSize;
        }
        
    #endif

}
