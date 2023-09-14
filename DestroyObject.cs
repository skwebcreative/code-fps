using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int damege; //あたった部位の毎ダメージ
    private GameObject enemy; //敵オブジェクト
    private HP hp; //HPクラス

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");　//敵情報を取得
        hp = enemy.GetComponent<HP>(); //HP情報を取得
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DestroyObject.cs：当たった");
        //ぶつかったオブジェクトのTagにShellという名前が書いてあったならば(条件)。
        if(other.CompareTag("Shell"))
        {
            //HPクラスのDamage関数を呼び出す
            hp.Damage(damege);

            //ぶつかってきたオブジェクトを破壊する
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
