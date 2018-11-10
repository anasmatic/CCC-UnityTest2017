using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Ui;

public class MainManger : MonoBehaviour {

    public GamePlayManager game;

    private Command initGameCommand, playGameCommand, pauseGameCommand, resumeGameCommand, gameOverCommand;

    void Awake () {
        //setup Commands
        initGameCommand = new InitGameCommand();
        playGameCommand = new PlayGameCommand();
        pauseGameCommand = new PauseCommand();
        resumeGameCommand = new ResumeCommand();
        gameOverCommand = new CameOverCommand();
        
        //listen to main events in the game
        EventManager.ListenTo(EventManager.INIT_GAME, OnInitGameHandler);
        EventManager.ListenTo(EventManager.START_GAME, OnStartGameHandler);
        EventManager.ListenTo(EventManager.PAUSE_GAME, OnPauseGameHandler);
        EventManager.ListenTo(EventManager.RESUME_GAME, OnResumeGameHandler);
        EventManager.ListenTo(EventManager.GAME_OVER, OnGameOverHandler);
    }


    //Events Listeners : they handle events by executing the corrolated Command
    //all commands are handled in GamePlayManager 
    private void OnStartGameHandler()
    {
        playGameCommand.Execute(game);
    }
    private void OnInitGameHandler()
    {
        initGameCommand.Execute(game);
    }
    private void OnPauseGameHandler()
    {
        pauseGameCommand.Execute(game);
    }
    private void OnResumeGameHandler()
    {
        resumeGameCommand.Execute(game);
    }
    private void OnGameOverHandler()
    {
        gameOverCommand.Execute(game);
    }
}
