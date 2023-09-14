using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追いかける対象")]
    private GameObject player;
    

    //private Cat chaser = null;
    public GameObject target;
    //UnityEngine.AI.NavMeshAgent agent;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = player.transform.position;
        //agent.SetDestination = target.transform.position;
         //this.chaser.Agent.SetDestination(this.cat.transform.position);
    }
}
