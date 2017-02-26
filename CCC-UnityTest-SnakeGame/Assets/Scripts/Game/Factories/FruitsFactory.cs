using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsFactory : MonoBehaviour {

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
    

    public int CreateFruit (int columns, int rows, SnakeHead snakeHead) {
    //TODO: make sure it won't laps the snake
        BaseFruit fruit = FruitsPool.Instance.GetFruitFromPool();
        if (fruit)
        {
            Vector3 pos = Map.Instance.GetValedPosition();
            //pos = new Vector3(Random.Range(1, columns-1), 0, Random.Range(1, rows-1)*-1);
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
        FruitsPool.Instance.hideCurrent();
    }
    public bool getIsActive()
    {
        return isActive;
    }
}
