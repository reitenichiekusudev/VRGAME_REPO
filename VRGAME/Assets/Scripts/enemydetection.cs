using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemydetection : MonoBehaviour
{
    public float FoVangle = 110f;
    public bool seenplayer;
    public Vector3 PersonalLastSighting;
    private NavMeshAgent navmeshagent;
    
    private SphereCollider sphere;
    private lastplayersighting lastplayersighting;
    private Animator anim;
    private GameObject player;
    private Animator playeranim;
    private Vector3 previousSighting;


    void Awake()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        sphere = GetComponent<SphereCollider>();
        lastplayersighting = GameObject.FindGameObjectWithTag(tags.gameController).GetComponent<lastplayersighting>();
        player = GameObject.FindGameObjectWithTag(tags.player);

        PersonalLastSighting = lastplayersighting.resetPosition;
        previousSighting = lastplayersighting.resetPosition;

    }
    void Update()
    {
        if (lastplayersighting.position != previousSighting)
            PersonalLastSighting = lastplayersighting.position;
        previousSighting = lastplayersighting.position;
    }

    void OnTriggerStay(Collider other)
    {

   
        if (other.gameObject == player)
        {
            seenplayer = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            
            if (angle < FoVangle * 0.5f)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphere.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        Debug.Log("<color=red>Fatal error:</color> player seen");
                        seenplayer = true;
                        PersonalLastSighting = player.transform.position;
                    }

                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            seenplayer = false;
        Debug.Log("<color=green>Fatal error:</color> player not seen");
    }
}
