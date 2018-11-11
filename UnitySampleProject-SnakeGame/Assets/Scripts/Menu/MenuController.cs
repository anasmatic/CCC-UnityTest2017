using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    //manage ui
    public class MenuController : MonoBehaviour
    {
        public MenuView view;
        //messages to appear at screen top, while the games start is delayed
        Stack<string> countDownStrings;
        
        //setup
        void Awake()
        {
            //avoid null error
            if (view == null)//if view was not assigned from editor
                view = GetComponent<MenuView>();//find it dynamicly 

            //play button handlation :
                //assing trigger comming from view
            view.ActionPlayBtn += HandlePlayBtnAction;
                //listen to the responce coming from game manager (after game was initialized and ready to show that at game menu)
            EventManager.ListenTo(EventManager.INIT_GAME, OnInitGameHandler);

            //GameOver handlation : listen to event comming from game manager after gameOver signal is triggered
            EventManager.ListenTo(EventManager.GAME_OVER, OnGameOverHandler);

            //Pause and resume :
            view.ActionPauseBtn += HandlePauseBtnAction;
            view.ActionResumeBtn += HandleResumeBtnAction;
        }

        //Actions Handlers (actions come from MenuView, mostly triggered by buttons clicks)
            ///<Summary>this function will trigger INIT_GAME event, received at MainManger to init (setup) game (using command pattern) </Summary>
        private void HandlePlayBtnAction()
        {
            //fire Event to game manager
            EventManager.TriggerEvent(EventManager.INIT_GAME);
        }
            ///<Summary>this function will trigger PAUSE_GAME event, received at MainManger to pause game (using command pattern) </Summary>
        private void HandlePauseBtnAction()
        {
            EventManager.TriggerEvent(EventManager.PAUSE_GAME);
        }
            ///<Summary>this function will trigger RESUME_GAME event, received at MainManger to resume game (using command pattern) </Summary>
        private void HandleResumeBtnAction()
        {
            EventManager.TriggerEvent(EventManager.RESUME_GAME);
        }

        //Listeners : events comming from game manager and controllers
        private void OnInitGameHandler()
        {
            //fill model -again- every time after game initialization is done
            countDownStrings = new Stack<string>(new string[] { "GO!", "SET!", "READY!" });//notice : the stack is revised
            //loop to and wait display the count down stack of messages 
            StartCoroutine(CountDownToStart());
        }
        private IEnumerator CountDownToStart()
        {
            //loop until the stack is empty
            while (countDownStrings.Count > 0)
            {
                //pop the stack, and send the result to view
                view.UpdateCountDown(countDownStrings.Pop());
                //wait for one second
                yield return new WaitForSeconds(1);
            }
            //when the stack is empty , we should handle game start
            CountDownDone();
        }

        private void CountDownDone()
        {
            //send event, that preparations are done, and game is ready to start
            EventManager.TriggerEvent(EventManager.START_GAME);
            //clear view countdown messages
            view.UpdateCountDown("");
            //show and hide related view items
            view.OnInitGameHandler();
            //stop the coroutine
            StopCoroutine(CountDownToStart());
        }

        private void OnGameOverHandler()
        {
            //show and hide related view items
            view.OnGameOverHandler();
        }
    }
}