using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;
    public Transform equipePosition;
    public float distance = 300f;

    public GameObject currentWeapon;
    GameObject wp;
    bool canGrab = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkweapons();
        if(canGrab)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                 Debug.Log("E押された");
                //武器を持っているとき
                if(currentWeapon != null)
                {
                    Debug.Log("ドロップ&ゲット前");
                    Drop();
                    PickUp();
                }
            }
        }
        //武器を持っているとき
        if(currentWeapon != null)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
        }
    }
    private void checkweapons()
    {
        RaycastHit hit;

        if(Physics.Raycast(targetCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, distance))
        {
            //Debug.Log("ray");
            //Debug.Log("if文動かないよ");
/* 
            if (hit.transform.tag == "CanGrab")
            {
                canGrab = true;
                wp = hit.transform.gameObject;
            } */
            if(hit.collider.CompareTag("CanGrab")){
                    Debug.Log("アイテムだよ：ItemPicker");
                    canGrab = true;
                wp = hit.transform.gameObject;
            }
        }else
        {
            canGrab = false;
        }

    }
    private void PickUp()
    {
        currentWeapon = wp;
        currentWeapon.transform.position = equipePosition.position;
        currentWeapon.transform.parent = equipePosition;
        currentWeapon.transform.localEulerAngles = new Vector3(0, 0, 0);
        currentWeapon.transform.eulerAngles = new Vector3( 0f, 0f, -310f); // 同上
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
        currentWeapon.GetComponent<BoxCollider>().enabled = false;
    }
    private void Drop()
    {
        currentWeapon.transform.parent = null;
        //currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        
       // currentWeapon.GetComponent<BoxCollider>().enabled = true;
        currentWeapon = null;
        
        
    }
}
