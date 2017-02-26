
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerHandler : MonoBehaviour {

    private GameObject hero;
    private Vector3 offset;//to keep the same camera view on every frame
	// Use this for initialization
	public void SetHeroToFollow (GameObject whatToFollow) {
        hero = whatToFollow;
        offset = transform.position - hero.transform.position;//even we will use this for the head, what we care about is the global pos
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(hero)
            transform.position = hero.transform.position + offset;
	}
}
