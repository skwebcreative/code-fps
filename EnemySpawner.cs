using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int maxEnemies = 10;
    [SerializeField]
    float minPositionX = -3;
    [SerializeField]
    float maxPositionX = 3;
    public bool spawnEnabled = false;
    float minSpawnInterval = 1;
    float maxSpawnInterval = 3;

    [SerializeField]
    public GameObject[]enemyPrefabs;


    bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnEnabled)
        {
            StartCoroutine(SpawnTimer());
        }

        enemyPrefabs = GameObject.FindGameObjectsWithTag("targetTag");
        if(enemyPrefabs.Length == 0){
            SceneManager.LoadScene("GameClear");
        }

    }
    IEnumerator SpawnTimer()
    {
        if(!spawning)
        {
            if(SpawnEnemy())
            {
                spawning = true;

                float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
                yield return new WaitForSeconds(interval);

                spawning = false;
            }
            else
            {
                yield return null;
            }
        }
        yield return null;
    }
    //ランダムに生成される場所
    bool SpawnEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("targetTag");
        if (enemies.Length >= maxEnemies)
        {
        return false;
        }
        else
        {
        int choosedIndex = Random.Range(0, enemyPrefabs.Length);
        float diffPositionX = Random.Range(minPositionX, maxPositionX);
        Vector3 position = new Vector3(transform.position.x + diffPositionX, transform.position.y, transform.position.z);
        Instantiate(enemyPrefabs[choosedIndex], position, Quaternion.identity);
        return true;
        }
    }
}
