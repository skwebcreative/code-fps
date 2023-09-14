using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //敵プレハブ

    [SerializeField]GameObject enemyPrefab;
    //適性性時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    public float minTime = 2f;
    public float maxTime = 5f;

    public int count = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        //敵プレハブ出現の時間間隔
        interval = GetRandomTime();
        StartCoroutine("enemyCount");
    }
    IEnumerator enemyCount()
    {
        for(int i = 5; i>0; i--)
            {
                yield return new WaitForSeconds(0.5f);
               /*  GameObject enemy = Instantiate(enemyPrefab);

                enemy.transform.position = new Vector3(0, 10, 20);

                time = 0f;
                Debug.Log(time); */

                //次に出現する時間間隔
                //interval = GetRandomTime();
            }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        for(int i = 0; i<5; i++)
        {
           if(time > interval && count > 0)
           {
            if(count<1)
            return;

            enemy();
            
            if(count<1)
            return; 

            count --;
            Debug.Log(count + ":カウント");
            } 
        }
        
    }
    public void  enemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);

                enemy.transform.position = new Vector3(0, 10, 20);

                time = 0f;
                Debug.Log(time);

                //次に出現する時間間隔
                //interval = GetRandomTime();
    }
    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }
}
