using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyPart : BaseBodyPart {

    
	// Use this for initialization
	void Awake () {
		
	}

    /*
    @param bool now : means set it now or wait to next tick,
                      if true , we need a direction,
                      if false, it should be not visibale and set over the last part position.
    */
    internal void SetInitPosition(bool now, string direction)
    {
        
    }

    public override void MovementHandler()
    {
        //take over the position of the part infront of you
        lastPossition = transform.localPosition;
        transform.localPosition = newPossition = previousPart.lastPossition;
        base.MovementHandler();
    }

}
