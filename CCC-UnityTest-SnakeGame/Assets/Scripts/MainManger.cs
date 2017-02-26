using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManger : MonoBehaviour {

    public Game game;
    public Menu menu;
    private Command initGame, playGame, pause;
    
    // Use this for initialization
    void Start () {
        initGame = new InitGameCommand();
        playGame = new PlayGameCommand();
        pause = new PauseCommand();

        EventManager.ListenTo(EventManager.INIT_GAME, OnInitGameHandler);
        EventManager.ListenTo(EventManager.START_GAME, OnStartGameHandler);
    }

    private void OnStartGameHandler()
    {
        playGame.Execute(game);
    }

    private void OnInitGameHandler()
    {
        initGame.Execute(game);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
