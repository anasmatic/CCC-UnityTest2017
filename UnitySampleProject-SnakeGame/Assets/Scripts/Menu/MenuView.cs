using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    //manage ui
    public class MenuView : MonoBehaviour
    {
        public Button playButton;
        public Button pauseButton;
        public GameObject pausePanel;
        public Button resumeButton;
        public Text statusText;

        void Awake()
        {
            //setup ui, initial status
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
        }

        void Start()
        {
            //init default values
            statusText.text = "";

            //assign button listeners, every listener will trigger an action that will be received at MenuController
            playButton.onClick.AddListener(OnPlayBtnClick);
            pauseButton.onClick.AddListener(OnPauseBtnClick);
            resumeButton.onClick.AddListener(OnResumeBtnClick);
        }

        void OnPlayBtnClick()
        {
            //send signal to MenuController
            if (ActionPlayBtn != null)//if action was assigned
                ActionPlayBtn();//fire event

            //handle view consequences 
            playButton.gameObject.SetActive(false);
        }
        void OnPauseBtnClick()
        {
            //send signal to MenuController
            if (ActionPauseBtn != null)//if action was assigned
                ActionPauseBtn();//fire event

            //handle view consequences 
            pauseButton.gameObject.SetActive(false);
            pausePanel.SetActive(true);
        }
        void OnResumeBtnClick()
        {
            //send signal to MenuController
            if (ActionResumeBtn != null)//if action was assigned
                ActionResumeBtn();//fire event

            //handle view consequences 
            pauseButton.gameObject.SetActive(true);
            pausePanel.SetActive(false);
        }

        //view utilities, support controller to change view on events
            //Gameover event view changes
        internal void OnGameOverHandler()
        {
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
            //InitGame event view changes, (init game is when the game is setup and ready to run, but didn't start the play yet)
        internal void OnInitGameHandler()
        {
            statusText.text = "";
            pauseButton.gameObject.SetActive(true);
        }
        
        //view utility called from a loop at MenuController, to update text
        internal void UpdateCountDown(string state)
        {
            statusText.text = state;
        }

        ///<Summary>Actions used to transfare button click from view to controller</Summary>
        internal Action ActionPlayBtn, ActionPauseBtn, ActionResumeBtn;
    }
}