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

    // Update is called once per frame
    public override void animate()
    {
        transform.localPosition = startPos + new Vector3(0.0f, Mathf.Sin(Time.time)*.5f, 0.0f);
        base.animate();
    }

}
