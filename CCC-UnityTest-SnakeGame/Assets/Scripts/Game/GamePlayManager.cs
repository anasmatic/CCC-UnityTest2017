using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    using Player;
    public class GamePlayManager : MonoBehaviour, IObserver
    {
        public Snake snake;
        public InputHandler inputHandler;

        internal void Init()
        {
            snake.Clear();

            snake.Init(width, height);
        }

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
            snake.ResumeMovement();
        }


        private IEnumerator FruitFactoryCoroutine()
        {
            if (FruitsFactory.Instance.getIsActive())
            {
                int waitTime = FruitsFactory.Instance.CreateFruit(width, height, snake.GetHead());
                yield return new WaitForSeconds(waitTime);
                StartCoroutine(FruitFactoryCoroutine());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        //got a message from the frout factory
        public void Notify()
        {
            StopAllCoroutines();
            StartCoroutine(FruitFactoryCoroutine());
        }

        public void NotifyWith(Vector3 position1, Vector3 position2) { }

        internal void Pause()
        {
            Time.timeScale = 0;
            FruitsFactory.Instance.StopProduction();
            snake.StopMovement();
        }
        internal void Resume()
        {
            Time.timeScale = 1;
            FruitsFactory.Instance.ResumeProduction();
            snake.ResumeMovement();
        }

        //stop function update of game objects, hault factories, and clean the pool !
        internal void GameOver()
        {
            snake.enabled = false;
            snake.GetHead().enabled = false;
            inputHandler.enabled = false;
            StopCoroutine(FruitFactoryCoroutine());
            FruitsFactory.Instance.StopProduction();
            FruitsPool.Instance.PauseCurrent();
        }

    }
}