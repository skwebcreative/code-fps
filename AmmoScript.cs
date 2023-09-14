using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public int ammo = 30;
    public BulletScript bulletscript; 

    
    // Start is called before the first frame update
    void Start()
    {
       // bulletobj = GameObject.Find("Bullet");
        
    }

    // Update is called once per frame
    void Update()
    {
        //bulletScript = bulletobj.GetComponent<BulletScript>();

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            Debug.Log("Ammoがプレイヤーと当たった1");
            ammo = bulletscript.PickUpAmmo(ammo);
            Debug.Log("Ammoがプレイヤーと当たった2");

            if(ammo <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
