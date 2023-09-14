using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
//    private MoveEnemy moveEnemy;
    // Start is called before the first frame update
    void Start()
    {
      //  moveEnemy = GetComponentInParent<MoveEnemy>();
    }

    void OnTriggerStay(Collider col){
        if(col.tag == "Player"){
            Debug.Log("Player検知");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
