using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
	
	// Update is called once per frame
	void OnButtonClick() {
        EventManager.TriggerEvent(EventManager.INIT_GAME);
        this.gameObject.SetActive(false);
	}
}
