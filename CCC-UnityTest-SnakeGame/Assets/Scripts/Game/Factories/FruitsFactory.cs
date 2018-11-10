using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitsFactory : MonoBehaviour {

    //Singleton
    private static FruitsFactory instance;
    public static FruitsFactory Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(FruitsFactory)) as FruitsFactory;

            if (instance == null) //still null ?!
                throw new System.NullReferenceException("FruitsFactory need to be added to game object on scene");

            return instance;
        }
    }

    private  bool isActive;
    
    //create and add to empty pos, and return the cooldown time of the fruit
    public int CreateFruit (int columns, int rows, Game.Player.SnakeHead snakeHead) {
    
        BaseFruit fruit = FruitsPool.Instance.GetFruitFromPool();
        if (fruit)
        {
            Vector3 pos = Map.Instance.GetValedPosition();
            
            fruit.transform.localPosition = pos;
            fruit.gameObject.SetActive(true);
            return fruit.lifeTime;
        }
        return 1;
    }

    internal void StopProduction()
    {
        isActive = false;
    }
    internal void ResumeProduction()
    {
        isActive = true;
        //used on game over, because this game has no pause button !
        FruitsPool.Instance.hideCurrent();
    }
    public bool getIsActive()
    {
        return isActive;
    }
}
