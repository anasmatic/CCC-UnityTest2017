using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manage ui
public class MenuController : MonoBehaviour
{
    public MenuView view;
    Stack<string> countDownStrings;
    // Use this for initialization
    void Awake()
    {
        //avoid null error
        if (view == null)//if view was not assigned from editor
            view = GetComponent<MenuView>();//find it dynamicly 

        //play button handlation :
        //assing triggers comming from view
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
    private void HandlePlayBtnAction()
    {
        //fire Event to game manager
        EventManager.TriggerEvent(EventManager.INIT_GAME);
    }
    private void HandlePauseBtnAction()
    {
        EventManager.TriggerEvent(EventManager.PAUSE_GAME);
    }
    private void HandleResumeBtnAction()
    {
        EventManager.TriggerEvent(EventManager.RESUME_GAME);
    }

    //Listeners : events comming from game manager and controllers
    private void OnInitGameHandler()
    {
        //fill model again
        countDownStrings = new Stack<string>(new string[] { "GO!", "SET!", "READY!" });

        StartCoroutine(CountDownToStart());
        
    }
    private IEnumerator CountDownToStart()
    {
        while (countDownStrings.Count > 0)
        {
            view.UpdateCountDown(countDownStrings.Pop());
            yield return new WaitForSeconds(1);
        }
        CountDownDone();
    }

    private void CountDownDone()
    {
        EventManager.TriggerEvent(EventManager.START_GAME);
        view.UpdateCountDown("");
        view.OnInitGameHandler();
        StopCoroutine(CountDownToStart());
    }

    private void OnGameOverHandler()
    {
        view.OnGameOverHandler();
    }
}
