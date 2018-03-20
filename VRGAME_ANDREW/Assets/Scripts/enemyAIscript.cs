using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAIscript : MonoBehaviour {

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    private enemydetection enemydetection;
    private NavMeshAgent nav;
    private Transform Player;
    private lastplayersighting lastplayersighting;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

	// Use this for initialization
    void Awake()
    {
        enemydetection = GetComponent<enemydetection>();
        nav = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag(tags.player).transform;
        lastplayersighting = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<lastplayersighting>();

    }
	
	// Update is called once per frame
	void Update () {
        if (enemydetection.seenplayer)
            attacking();
        else if (enemydetection.PersonalLastSighting != lastplayersighting.resetPosition)
            Chasing();
        else
            Patroling();
	}
    void attacking()
    {
        Debug.Log("<color=red> attacking </color>");
        nav.isStopped = true;
    }
    void Chasing()
    {
        nav.isStopped = false;
       
        Vector3 sightingDeltaPos = enemydetection.PersonalLastSighting - transform.position;
        
        if (sightingDeltaPos.sqrMagnitude > 4.0f)
        {

            nav.destination = enemydetection.PersonalLastSighting;
            Debug.Log(nav.destination);
            Debug.Log("<color=red>chasing </color>");
        }
        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseWaitTime)
            {
                lastplayersighting.position = lastplayersighting.resetPosition;
                enemydetection.PersonalLastSighting = lastplayersighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }

    void Patroling()
    {
        nav.isStopped = false;
        Debug.Log("<color=green> patrolling </color>");
        nav.speed = patrolSpeed;
        if (nav.destination == lastplayersighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                patrolTimer = 0f;

            }
        }
        else
            patrolTimer = 0f;
        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
