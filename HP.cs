using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int  hp = 5;
    private Slider _slider;
    public GameObject slider;

    public int hitPoint = 100; //HP
    //private int damege　= 50;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = slider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = hp;

        //HPが0になった時に敵を破壊する
        if(hitPoint <= 0){
            Destroy(gameObject);
        }
    }

/*     void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Shell")
        {
            hp -= 1;
        }
        if(hp <= 0)
        {
            print("GameOver");
        }
    } */

    public void Damage(int damege)
    {
        Debug.Log(hitPoint);
        hitPoint -= damege;
        Debug.Log(hitPoint);
    }
}
