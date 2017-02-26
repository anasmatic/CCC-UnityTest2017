using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour,IObserver {

    // Use this for initialization
    public Snake snake;
    public Text countDownText;
    public InputHandler inputHandler;
    public GameObject fruitsContainer;

    
    //private string[] contDownStrings = {"READY!","SET!","GO!"};

    void Start () {
        EventManager.ListenTo(EventManager.GAME_OVER, OnGameOverHandler);
    }

    internal void Init()
    {
        snake.Clear();

        snake.Init(width, height);    
    }
    
    //TODO: to Level
    int width = 20;
    int height = 20;
    internal void StartGame()
    {
        var head = snake.GetHead();

        Map.Instance.InitMap(width, height);
        //init input
        inputHandler.Init(head);
        //init camera
        //Camera.main.GetComponent<CameraFollowPlayerHandler>().SetHeroToFollow(head.gameObject);
        //init fruits pool, and send the snake to subscribe the fruit collision trigger
        FruitsPool.Instance.InitPool(new IObserver[] { snake, this });
        //start factory
        FruitsFactory.Instance.ResumeProduction();
        StartCoroutine(FruitFactoryCoroutine());
        //activate snake
        head.enabled = true;
    }


    private IEnumerator FruitFactoryCoroutine()
    {
        if (FruitsFactory.Instance.getIsActive()) { 
            int waitTiem = FruitsFactory.Instance.CreateFruit(width, height, snake.GetHead());
            yield return new WaitForSeconds(waitTiem);
            StartCoroutine(FruitFactoryCoroutine());
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}

    //got a message from the frout factory
    public void Notify()
    {
        StopAllCoroutines();
        StartCoroutine(FruitFactoryCoroutine());
    }

    public void NotifyWith(Vector3 position1, Vector3 position2){}

    internal void Pause(){}

    //stop function update of game objects, hault factories, and clean the pool !
    private void OnGameOverHandler()
    {
        snake.enabled = false;
        snake.GetHead().enabled = false;
        inputHandler.enabled = false;
        StopCoroutine(FruitFactoryCoroutine());
        FruitsFactory.Instance.StopProduction();
        FruitsPool.Instance.PauseCurrent();
    }

}
