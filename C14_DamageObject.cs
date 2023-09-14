using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C14_DamageObject : MonoBehaviour
{
    //ダメージを与えるオブジェクト　ゾンビに持たせるプログラム
    private C13_Status c13_Status;
    // Start is called before the first frame update
    void Start()
    {
        c13_Status = GetComponent<C13_Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HitArea"){
            other.transform.root.GetComponent<Player>().clashDamage(c13_Status);
        }
    }
}
