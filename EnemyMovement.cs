using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //プレイヤーの位置情報を取得
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();　//NavMeshAgentのコンポーネントを取得
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // nav.SetDestination(player.position);
    }
}
