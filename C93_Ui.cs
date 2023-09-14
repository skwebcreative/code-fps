using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C93_Ui : MonoBehaviour
{
    public Text text_GameOver;

    public Text text_playerHP;     // プレイヤーのHP表示用
    private C13_Status c13_status; // プレイヤーのステータス参照用.
    
    private const float maxHp = 100f; //敵キャラの最大HP
    private float currentHp; //現在のHP
    public Slider slider; //Sliderを入れる

    // Start is called before the first frame update
    public void enableText_GameOver()
    {
        Debug.Log("テキスト表示");
        text_GameOver.enabled = true;
    }


    void Start()
    {
        slider.maxValue = maxHp;
        currentHp = maxHp;
        slider.value = currentHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //テキスト初期化用
    public void initialize(C13_Status status){
        c13_status = status;
        changeText_PlayerHP();
    }
    public void changeText_PlayerHP(){
        if(text_playerHP != null){
            //FPS19
            text_playerHP.text = "体力："　+ c13_status.getHP();
            slider.value = c13_status.getHP();
            Debug.Log(c13_status.getHP());
        }
    }
}
