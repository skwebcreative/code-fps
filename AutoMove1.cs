using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove1 : MonoBehaviour
{
    public Transform[] points;　//巡回地点オブジェクトを格納する配列
    private int destPoint = 0; //巡回地点のオブジェクトの数
    private UnityEngine.AI.NavMeshAgent agent; //NavMeshAgentを格納する変数
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false; //エージェントは目的地に近づいても減速しない
        destPoint = Random.Range(0, points.Length);
        GotoNextPoint(); //次の巡回地点の処理を実行
    }

    void GotoNextPoint()
    {
        //地点がなにも設定されていないときに返します
        if(points.Length == 0)
        return;

        agent.destination = points[destPoint].position;
        //次の位置を目的地点に設定
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        GotoNextPoint();
    }
}
