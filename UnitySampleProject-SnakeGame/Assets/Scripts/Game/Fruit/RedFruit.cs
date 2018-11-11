using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFruit : BaseFruit {

    protected override void OnEnable()
    {
        //set life time then call your father !
        lifeTime = 5;
        base.OnEnable();
    }

    // unique animation of the red fruit !
    public override void animate()
    {
        //transform.localPosition = startPos + new Vector3(0.0f, Mathf.Sin(Time.time)*0.5f, 0.0f);//up and down
        transform.Rotate (0,0,45*Time.deltaTime);//rotation 
        base.animate();
    }

}
