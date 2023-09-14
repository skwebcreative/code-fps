using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombeAttack : MonoBehaviour
{
    public bool moveEnabled = true;

    [SerializeField]
    int maxHp = 3;
    [SerializeField]
    int ammoDamage = 5;
    [SerializeField]
    int attackInterval = 1;
    [SerializeField]
    string targetTag = "Player";
    [SerializeField]
    float deadTime = 3;

    bool attacking = false;
    int hp;
    float moveSpeed = 80;

    Animator animator;
    BoxCollider boxCollider;
    Rigidbody rigidBody;
    NavMeshAgent agent;
    Transform target;
//    GameManeger gameManeger;
    [SerializeField]
    Player player;
    // Start is called before the first frame update
    
    public int Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);

            if(hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
        get
        {
            return hp;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("targetTag").transform;
//        gameManeger = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManeger>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveEnabled)
        {
            Move();
        }
        else
        {
            Stop();
        }
        
    }
    void InitCharacter()
    {
        Hp = maxHp;
        moveSpeed = agent.speed;
    }
    void Move()
    {
        agent.speed = moveSpeed;
        animator.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);

        agent.SetDestination(target.position);
        rigidBody.velocity = agent.desiredVelocity;
    }
    void Stop()
    {
        agent.speed = 0;
        animator.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
    }
    IEnumerator Dead()
    {
        moveEnabled = false;
        Stop();
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(AttackTimer());
        }

    }

    IEnumerator AttackTimer ()
    {
        if(!attacking)
        {
            attacking = true;
            moveEnabled = false;

//            animator.SetTrigger("Attack");
            //player.Ammo -= ammoDamage;
            yield return new WaitForSeconds(attackInterval);

            attacking = false;
            moveEnabled = true;
        }
        yield return null;
    }

}
