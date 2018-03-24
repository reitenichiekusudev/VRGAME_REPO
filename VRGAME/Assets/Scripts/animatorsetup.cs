using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorsetup
{

    public float SpeedDampTime = 0.1f;
    public float AngularSpeedDamptime = 0.7f;
    public float AngleResponseTime = 0.6f;

    private Animator anim;
    private HashIDs hash;
    
    public animatorsetup(Animator animator, HashIDs hashIDs)
    {
        anim = animator;
        hash = hashIDs;
    }

    public void setup(float speed, float angle)
    {
        float angularspeed = angle / AngleResponseTime;
        anim.SetFloat(hash.speedFloat, speed, SpeedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularspeed, AngularSpeedDamptime, Time.deltaTime);
    }
}
