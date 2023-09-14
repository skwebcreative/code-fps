using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    //GameObject target = transform.Find("Cube").gameObject;
    private GameObject target;
    public float speed = 10f;

    public Transform[] points;
   // [SerializeField] int destPoint = 0;
    [SerializeField] int destPoint = 0;
    private NavMeshAgent agent;

    Vector3 playerPos;
    GameObject player;
    float distance;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] bool tracking = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        target = GameObject.Find("Player");

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target + "ターゲットはプレイヤーですよ。");
        
         if(target.GetComponent<Player>().isArea == true)
           // if(target.GetComponent<CubeMove>().isArea == true) {
        {
            transform.LookAt(target.transform);
            transform.position += transform.forward * speed;

             agent.destination = target.transform.position;
        }  

        //プレイヤーとこのオブジェクトの距離を測る
        playerPos = player.transform.position;
        distance = Vector3.Distance(this.transform.position, playerPos);

        if(tracking)
        {
            //追跡の時、quitRangeより距離が離れたら中止
            if(distance > quitRange)
            {
                tracking = false;

                //Playerを目標とする
                agent.destination = playerPos;
            }
            else
            {
                //PlayerがtrackingRangeより近づいたら追跡開始
                if(distance < trackingRange)
                {
                    tracking = true;
                }
            }

            //エージェントが現在地点に近づいてきたら
            //次の目標地点を選択します
            if(!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if(points.Length == 0)
        return;

        //エージェントが現在設定された目標地点に行くように設定します。
        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void OnDrawGizmosSelected()
    {
        
    }
}
