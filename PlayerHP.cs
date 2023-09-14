using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    Slider hpSlider; 
    [SerializeField] 
    private float hp = 200f;
    private float nowhp; 
    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
