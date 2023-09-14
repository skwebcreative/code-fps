using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearScript : MonoBehaviour
{

    [SerializeField] GameObject[] enemys;
    //次に敵が出現するまでの時間
    [SerializeField] float appearNextTime;
    //この場所から出現する敵の数
    [SerializeField]int maxNum0fEnemys;
    //今何人の敵を出現させたか(総数)
    private int number0fEnemys;
    //待ち時間計測フィールド
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        number0fEnemys = 0;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemys!= null){
            //この場所から出現する最大数を超えてたらその後何もしない
            if(number0fEnemys >= maxNum0fEnemys){
                return;
            }

            //経過時間を足す
            elapsedTime += Time.deltaTime;

            //経過時間が経ったら
            if(elapsedTime > appearNextTime){
                elapsedTime = 0f;

                AppearEnemy();
            }
        }
             
    }
    //敵出現メソッド
    void AppearEnemy(){
        
            //出現させる敵をランダムに選ぶ
            var randomValue = Random.Range(0, enemys.Length);
        // 敵の向きをランダムに設定
            var randomRotationY = Random.value * 360f;
        if(enemys[0]!= null){
            GameObject.Instantiate(enemys[randomValue], transform.position, Quaternion.Euler(0f, randomRotationY, 0f));

            number0fEnemys++;
            elapsedTime = 0f;
        }
    }
}
