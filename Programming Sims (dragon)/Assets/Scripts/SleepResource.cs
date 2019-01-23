using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepResource : StatFiller {

    public override void AddValue() //this makes the script give the ai a certain amount of resource
    {
        float amount;
        amount = regenValue * Time.deltaTime;
        dS.sleep += amount;
        Mathf.Clamp(dS.sleep, 0, 100);
    }

    public override void Update()
    {
        base.Update();
        if (inRange)
        {
            dS.an.SetBool("Sleeping", true); //this here makes the sleep animation play when the ai is within range
        }
        else
        {
            dS.an.SetBool("Sleeping", false);
        }
    }
}
