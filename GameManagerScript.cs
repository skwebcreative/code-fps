using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public enum GAME_STATUS{Play, Clear, Pause, GameOver};
    public static GAME_STATUS status;

    [SerializeField]
    GameObject clearUI, gameOverUI;
    
/*     public void GameOver()
    {
        CurrentGameParam.State = GameState.End;
        if(currentFieldEnemies.Count > 0)
        {
         Debug.Log("a");   
        }
    } */
    // Start is called before the first frame update
    void Start()
    {
        status = GAME_STATUS.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if(status == GAME_STATUS.Clear)
        {
            Debug.Log("フィールド上のゾンビ撃破：クリア");
        }else if(status == GAME_STATUS.GameOver)
        {
            Invoke("ShowGameOverUI", 3f);
            enabled = false;
            return;
        }
    }
    private void ShowGameOverUi()
    {
        gameOverUI.SetActive(true);
    }
}