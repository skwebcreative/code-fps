using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C11_Weapon
{
    private int type	= 0;			// 武器タイプ
	private int num		= 2;			// 武器の種類数
    // Start is called before the first frame update
    void Start()
    {
          
    }
    public void changeWeapon()
    {
        type = (type + 1) % num;
        Debug.Log("現在の武器：" + type);
    }
    public int getType(){
		return type;
	}

    // Update is called once per frame
    void Update()
    {

    }
}
