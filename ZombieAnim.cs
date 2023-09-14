
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieAnim : MonoBehaviour
{
    private C13_Status c13_Status;
    int ATK = 10;
    public float _attackPoint = 10.0f;
    private const int maxHp = 100; //敵キャラの最大HP
    private int currentHp; //現在のHP
    public Slider slider; //Sliderを入れる

    public enum EnemyState {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze
    };

    [SerializeField]
    private float freezeTime = 0.5f;


    public bool moveEnabled = true;

    [SerializeField]
    Player player;
//    int maxHp = 3;
    int hp;
    float moveSpeed = 5.1f;
     [SerializeField]
    int attackInterval = 1;
    [SerializeField]
    string targetTag = "Player";
    [SerializeField]
    float deadTime = 3;

    bool attacking = false;

    Animator animator;
    Rigidbody rigidBody;
    NavMeshAgent agent;
    Transform target;

    //スコア
    public Score S_score;
    public int A_score = 100;
    bool score = false;

    // Start is called before the first frame update
    void Start()
    {
        c13_Status = GetComponent<C13_Status>();
        slider.maxValue = maxHp;
        currentHp = maxHp; //初期状態のHP(100)
        slider.value = currentHp; //Sliderの初期状態を設定(HP満タン)
        
        Debug.Log("Start slider.value = " + slider.value);

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        player=GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FirstPersonGunController>();

        S_score = GameObject.Find("Score_Text").GetComponent<Score>();

    }

    // Update is called once per frame
    void Update()
    {
        if(moveEnabled)
        {
            Move();
        }else
        {
            Stop();
        } 
    }
    void Move()
    {
        agent.speed = moveSpeed;
        animator.SetFloat("Speed", agent.speed, 10.1f, Time.deltaTime);
        //Debug.Log("ゾンビアニメーター検知");

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

            animator.SetTrigger("Attack");
            //インターフェース実装 player継承
            var playerScript = player.GetComponent<IDamageable>();
            if(playerScript != null)
            {
                Debug.Log("playerm、IDamageable：インターフェース実装");
                player.GetComponent<IDamageable>().AddDamage(_attackPoint);
            }
            //player.Ammo -= ammoDamage;
            yield return new WaitForSeconds(attackInterval);

            attacking = false;
            moveEnabled = true;
        }
        yield return null;
    }
    void OnTriggerEnter(Collider other)
    {
        //パターン2
        //インターフェースを呼び出す
        IDamageable damageable = other.GetComponent<IDamageable>();
        //damageableにnull値が入っていないかチェック
        if(damageable != null)
        {
            damageable.Damage(ATK);
            Debug.Log("プレイヤーに攻撃:：インターフェース");
        }
        

         if (other.gameObject.tag == "Shell")
        {
            currentHp -=5;
            slider.value = currentHp;
            Debug.Log("slider.value = " + slider.value);
            slider.value--;
            
            if(slider.value <= 0)
            {
                Destroy(gameObject);
                Destroy(GameObject.Find("Slider"));
                 if (score == false)
            {
                S_score.AddScore(A_score);
                score = true;
            }
            }
        }
    } 
    // ■■■攻撃を受けた時に呼び出される関数■■■   C05_Enemy
    //damage()関数を、atkDamage(C13_Status atk_status)に変更
    public void atkDamage(C13_Status atk_status)
    {        
        c13_Status.damage(atk_status);
        if(c13_Status.getHP() == 0){
            Debug.Log("攻撃！！！！！！！");
        }
    }
}
