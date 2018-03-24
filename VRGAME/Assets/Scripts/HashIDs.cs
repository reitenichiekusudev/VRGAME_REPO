using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour {

    public int dyingState;
    public int deadBool;
    public int locomotionState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int playerInSightBool;
    public int attackFloat;
    public int attackWeightFloat;
    public int angularSpeedFloat;
    public int openBool;
	
	// Update is called once per frame
	void Awake () {
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        speedFloat = Animator.StringToHash("Speed");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        attackFloat = Animator.StringToHash("Attack");
        playerInSightBool = Animator.StringToHash("PlayerInSight");

	}
}
