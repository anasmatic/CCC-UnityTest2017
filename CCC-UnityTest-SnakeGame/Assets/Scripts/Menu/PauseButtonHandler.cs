using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnPauseClick);
    }
	
	// Update is called once per frame
	void OnPauseClick() {
        EventManager.TriggerEvent(EventManager.PAUSE_GAME);
        this.gameObject.SetActive(false);
	}
}
