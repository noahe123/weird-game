using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderPathFinder : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdatePath", 2.0f, 0.3f);
    }
    void UpdatePath()
    {
        agent.SetDestination(player.transform.position);
    }
}
