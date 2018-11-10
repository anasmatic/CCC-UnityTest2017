using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class MainManger : MonoBehaviour {

    public GameManager game;
    public MenuView menu;
    private Command initGame, playGame, pauseGame, resumeGame;

    void Start () {
        initGame = new InitGameCommand();
        playGame = new PlayGameCommand();
        pauseGame = new PauseCommand();
        resumeGame = new ResumeCommand();

        EventManager.ListenTo(EventManager.INIT_GAME, OnInitGameHandler);
        EventManager.ListenTo(EventManager.START_GAME, OnStartGameHandler);
        EventManager.ListenTo(EventManager.PAUSE_GAME, OnPauseGameHandler);
        EventManager.ListenTo(EventManager.RESUME_GAME, OnResumeGameHandler);
    }

    private void OnStartGameHandler()
    {
        playGame.Execute(game);
    }

    private void OnInitGameHandler()
    {
        initGame.Execute(game);
    }

    private void OnPauseGameHandler()
    {
        pauseGame.Execute(game);
    }
    private void OnResumeGameHandler()
    {
        resumeGame.Execute(game);
    }
}
