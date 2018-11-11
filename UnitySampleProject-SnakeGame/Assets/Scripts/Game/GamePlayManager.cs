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
        //hero script reference
        public Snake snake;
        //user input handler reference
        public InputHandler inputHandler;
        //fruits factory script reference
        public FruitsFactory fruitsFactory;

        int width = 20;
        int height = 20;

        internal void Init()
        {
            snake.Clear();

            snake.Init(width, height);
        }
        internal void StartGame()
        {
            var head = snake.GetHead();

            Map.Instance.InitMap(width, height);
            //init input
            inputHandler.Init(head);
        
            //init fruits pool, and send the snake to subscribe the fruit collision trigger
            //FruitsPool.Instance.InitPool(new IObserver[] { snake, this });
            fruitsFactory.init(new IObserver[] { snake, this });
            //start factory
            fruitsFactory.ResumeProduction();
            StartCoroutine(FruitFactoryCoroutine());
            //activate snake
            head.enabled = true;
            snake.ResumeMovement();
        }

        private IEnumerator FruitFactoryCoroutine()
        {
            while(fruitsFactory.getIsActive())
            {
                //every fruit has its own wait time, set default as 1 second now, but later fruits factory will decide 
                int waitTime = 1;
                //ask factory to create a fruit, show on map in a valid location, and return wait time
                fruitsFactory.CreateFruit(  width,
                                            height, 
                                            snake.GetHead(),
                                            Map.Instance.GetValidPosition(),
                                            out waitTime
                                            );
                //wait until the fruit disappear before loop again
                yield return new WaitForSeconds(waitTime);
            }
        }

        //got a message from the fruit factory
        public void Notify()
        {
            StopAllCoroutines();
            StartCoroutine(FruitFactoryCoroutine());
        }

        public void NotifyWith(Vector3 position1, Vector3 position2) { }

        internal void Pause()
        {
            //Time.timeScale = 0;
            fruitsFactory.StopProduction();
            snake.StopMovement();
        }
        internal void Resume()
        {
            //Time.timeScale = 1;
            fruitsFactory.ResumeProduction();
            snake.ResumeMovement();
        }

        //stop function update of game objects, halt factories, and clean the pool !
        internal void GameOver()
        {
            //disable snake behaviors
            snake.enabled = false;
            snake.GetHead().enabled = false;
            
            //disable user input
            inputHandler.enabled = false;
            
            StopCoroutine(FruitFactoryCoroutine());
            //stop production will pause pool behaviors
            fruitsFactory.StopProduction();
        }

    }
}