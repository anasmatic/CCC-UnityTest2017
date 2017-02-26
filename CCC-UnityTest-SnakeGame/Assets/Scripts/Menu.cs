using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button playButton;
    public Text statusText;
    Stack<string> countDownStrings;
    // Use this for initialization
    void Start () {
        statusText.gameObject.SetActive(false);
        EventManager.ListenTo(EventManager.INIT_GAME, OnInitGameHandler);
        EventManager.ListenTo(EventManager.GAME_OVER, OnGameOverHandler);
    }

    private void OnGameOverHandler()
    {
        statusText.gameObject.SetActive(true);
        statusText.text = "Game Over";

        playButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void OnInitGameHandler() {
        countDownStrings = new Stack<string>(new string[] { "GO!", "SET!", "READY!" });
        statusText.gameObject.SetActive(true);
        StartCoroutine(CountDownToStart());
    }

    private IEnumerator CountDownToStart()
    {
        if (countDownStrings.Count > 0)
        {
            statusText.text = countDownStrings.Pop();
            yield return new WaitForSeconds(1);
            StartCoroutine(CountDownToStart());
        }
        else
        {
            StopCoroutine(CountDownToStart());
            statusText.text = "";

            statusText.gameObject.SetActive(false);
            EventManager.TriggerEvent(EventManager.START_GAME);
        }
    }
}
