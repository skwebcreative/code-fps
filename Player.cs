using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public C93_Ui c93_Ui;
    [SerializeField] private ZombieAnim zombieanim;
    int HP = 100;
    
    public float _HitPoint = 100.0f;
    //private const int maxHp = 100; //敵キャラの最大HP
    private int currentHp; //現在のHP
    public Slider slider; //Sliderを入れる

    [SerializeField]
    int maxHp = 100;

    PlayerHP playerhp;
/*     [SerializeField] 
    private float hp = 5; */
    int hp;

    private Transform cameraTransform;
    [SerializeField]public Camera FPSCamera;
    [SerializeField] private CurveControlledBob headBob_ = new CurveControlledBob();
    public GameObject currentWeapon;
    public Transform equipePosition;
    [SerializeField] SmallArms weapon;
    [SerializeField] Text centerText;
    bool foundItem;
    public bool isArea;

    public float speed = 3.0f;
    public float gravity = 9.81f;
    public float JumpPower = 10;
    
    Vector3 moveDirection = Vector3.zero; //X軸、Y軸、Z軸（移動方向）を保持するための関数
    
    CharacterController controller;
    GameObject wp;

    

    public int Hp
    {
        get{return hp;}
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);

            if(hp <= 0)
            {
                Death();
            }
        }
    }
    
    public void AddDamage(float damage)
    {
        _HitPoint -= damage;
        Debug.Log("add: " + damage + "hp: " + _HitPoint);

        if (_HitPoint <= 0)
        {
            Debug.Log("Enemyを倒した");
        }
    } 

    public void Damage(int value)
    {
        // ここに具体的なダメージ処理
        Debug.Log("パターン2：インターフェース");
        hp -= value;
        Debug.Log("damage:" + hp);

        HP -= value;
        slider.value = HP;

        if(HP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        // ここに具体的な死亡処理
        Debug.Log("death");
    }

    public void SetWeapon(SmallArms w)
    {
        weapon = w;
        //武器を非アクティブにする
        w.gameObject.SetActive(false);
        currentWeapon.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //テキストの初期化
        c93_Ui.initialize(GetComponent<C13_Status>());
       // cameraTransform = FPSCamera.transform;
        headBob_.Setup(FPSCamera, 1.0f);
        controller = GetComponent<CharacterController>();
        //centerText.text = "";

        slider.maxValue = maxHp;
        currentHp = maxHp; //初期状態のHP(100)
        slider.value = currentHp; //Sliderの初期状態を設定(HP満タン)
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if(moveDirection.magnitude > 0.1f){
                Debug.Log("動いた");
                Vector3 handBob_ = headBob_.DoHeadBob(1.0f);
                FPSCamera.transform.localPosition = handBob_;

            }
            

            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = JumpPower;
            }
            
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


        //武器を拾う
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 300f))
        {
            //centerText.text = hit.collider.tag;
            //Debug.Log("レイ飛んだ");
            wp = hit.transform.gameObject;

            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E押された");
                if(hit.collider.tag == "Item")
                {
                    Debug.Log("タグ認識");
                    //落ちている武器から武器スクリプトを取得
                    SmallArms weapon = hit.collider.GetComponent<SmallArms>();
                    //取得出来たらセット
                    if(weapon != null)
                     SetWeapon(weapon);
                    foundItem = true;
                    PickUp();
                    
                }
            }
        }
    }

    // ■■■ダメージオブジェクトにぶつかった時に呼び出される関数■■■
    public void clashDamage(C13_Status e_status){
        Debug.Log("ダメージオブジェクトにぶつかった");
        GetComponent<C13_Status>().damage(e_status);
        c93_Ui.changeText_PlayerHP();
    }

    private void PickUp()
    {
        Debug.Log("拾った:PickUp関数");
        currentWeapon = wp;
        currentWeapon.transform.position = equipePosition.position;
        currentWeapon.transform.parent = equipePosition;
        currentWeapon.transform.localEulerAngles = new Vector3(0, 90, 0);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "DangerArea" )
        {
            isArea = true;
        }
    }
 
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DangerArea" )
        {
            isArea = false;
        }
    }

    //HP減り続ける
     /* void OnCollisionStay(Collision collision){
        if(collision.gameObject.tag == "targetTag")
        {
            Debug.Log("aaaaaaa");

            currentHp -=5;
            Debug.Log("現在のHP" + currentHp);
            slider.value = currentHp;
            Debug.Log("slider.value = " + slider.value);
            slider.value--;
            //hp -= other.gameObject.GetComponent<zombie>().powerEnemy;
            
            
        }
        if (hp <= 0)     
        {
            //Destroy(gameObject);  //ゲームオブジェクトが破壊される
        }
    } */


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "targetTag")
        {           
/*          Debug.Log("パターン1：aaaaaaa");   
            currentHp -=5;
            Debug.Log("現在のプレイヤーのHP" + currentHp);
            slider.value = currentHp;
            Debug.Log("slider.value = " + slider.value);
            slider.value--; */
        }
        if (hp <= 0)     
        {
            //Destroy(gameObject);  //ゲームオブジェクトが破壊される
        }
/*         if(other.gameObject.tag == "smallgun")
        {
            GameObject handgrip = transform.Find("BulletRuncher").gameObject;
            GameObject weapon = Instantiate(Resources.Load("Prefabs/SmallGun"), handgrip.transform.position, handgrip.transform.rotation) as GameObject;
            weapon.transform.parent = handgrip.transform;

            //bulletRuncher.GetWeapon();

        } */
    }
}
