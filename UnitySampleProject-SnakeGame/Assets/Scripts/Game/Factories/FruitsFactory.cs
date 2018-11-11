using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitsFactory : MonoBehaviour {

    public FruitsPool pool;
    private bool isActive;
    void Awake(){
        //avoid null error
        if(pool == null)
            pool = GetComponent<FruitsPool>();
    }
    //init factory, this is coming from GamePlayManager.StartGame
    internal void init(IObserver[] observers)
    {
        //init pool, pass the observers that fruits will subscribe to.
        pool.InitPool(observers);
    }

    //create and add to empty pos, and return the cooldown time of the fruit
    public BaseFruit CreateFruit (int columns, int rows, Game.Player.SnakeHead snakeHead, Vector3 pos , out int waitTime) {
        pool.hideCurrent();
        BaseFruit fruit = pool.GetFruitFromPool(); 
        fruit.transform.localPosition = pos;
        waitTime = fruit.lifeTime;
        fruit.gameObject.SetActive(true);

        return fruit;
    }

    //deactivate factory, deactivate current fruit from pool
    internal void StopProduction()
    {
        isActive = false;
        pool.PauseCurrent();
    }
    //activate factory, activate current fruit from pool
    internal void ResumeProduction()
    {
        isActive = true;
        pool.ResumeCurrent();
    }
    public bool getIsActive()
    {
        return isActive;
    }
}
