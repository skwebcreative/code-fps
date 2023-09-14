using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    // Update is called once per frame
    void Update()
    {
        if(targetCamera){
            RaycastHit hit;
            
            if(Physics.Raycast(targetCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 100.0f)){
                if(hit.collider.CompareTag("Item")){
                    Debug.Log("アイテムだよ：ItemPicker");
                }
            }
        }
    }
}
