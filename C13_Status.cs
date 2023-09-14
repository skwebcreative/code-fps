using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C13_Status : MonoBehaviour
{
    public float HP = 10;
    public float ATK = 1;

    public void setHP(int hp){HP = hp;}
    public float getHP(){return HP;}
    public void setATK(int atk){ATK = atk;}
    public float getATK(){return ATK;}

    public void damage(C13_Status status)
    {
        HP = Mathf.Max(0, HP - status.getATK());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
