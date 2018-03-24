using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour {

    public float deadzone = 2f;

    private Transform player;
    private enemydetection enemydetection;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;
    private animatorsetup animsetup;
    


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(tags.player).transform;
        enemydetection = GetComponent<enemydetection>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<HashIDs>();

        nav.updateRotation = false;
        animsetup = new animatorsetup(anim, hash);

        //anim.SetLayerWeight(1, 1f);

        deadzone *= Mathf.Deg2Rad;
    }
    void Update()
    {
        NavAnimSetup();
    }

    void NavAnimSetup()

    {
        float speed;
        float angle;
        if(enemydetection.seenplayer)
        {
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);

        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < deadzone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }
        animsetup.setup(speed, angle);
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
            return 0f;
        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
}
