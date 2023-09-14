using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timeGUI;
    private float time = 51f;
    float countdown = 3f;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeGUI.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time -= 1f * Time.deltaTime;
        timeGUI.text = ((int)time).ToString();

    }
}
