using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    private const int maxHp = 100; //敵キャラの最大HP
    private int currentHp; //現在のHP
    public Slider slider; //Sliderを入れる
    // Start is called before the first frame update
    void Start()
    {


        slider.maxValue = maxHp;
        currentHp = maxHp; //初期状態のHP(100)
        slider.value = currentHp; //Sliderの初期状態を設定(HP満タン)
        
        Debug.Log("Start slider.value = " + slider.value);

    }

    void OnTriggerEnter(Collider other)
    {
        /*  Debug.Log("ああ");
         if(other.CompareTag("Shell"))
        {
            Debug.Log("slider.value = " + slider.value);
            currentHp = -10;
            slider.value = currentHp;
            Debug.Log("slider.value = " + slider.value);

            Debug.Log("a");
            Debug.Log("ああ");
            Debug.Log("当たった");
        } */
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
            }
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
