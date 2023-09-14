using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public GameObject[]gun;
    private int number;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< gun.Length; i++)
        {
            if(i == number)
            {
                gun[i].SetActive(true);
            }
            else
            {
                gun[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            number = (number + 1) % gun.Length;
            for(int i= 0; i < gun.Length; i++)
            {
                if(i == number)
                {
                    gun[i].SetActive(true);
                }
                else
                {
                    gun[i].SetActive(false);
                }
            }
        }
    }
}
