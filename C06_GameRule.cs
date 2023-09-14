using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C06_GameRule : MonoBehaviour
{
    private Player playerScript;
    private C93_Ui c93_Ui;
    private GameObject player;
    private C13_Status c13_status;

    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        c93_Ui		= GameObject.Find("GameRoot").GetComponent< C93_Ui >();
		player		= GameObject.FindGameObjectWithTag("Player") as GameObject;
		c13_status	= player.GetComponent< C13_Status >();
        //playerScript	= player.GetComponent< Player >();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            return;
        }

        player_DeadCheak();
    }
    

    // ■■■ゲームオーバー処理■■■
	public void gameOver(){
        Debug.Log("// ■■■ゲームオーバー処理■■■");
		isGameOver = true;				// ゲームオーバーフラグをtrueに
		c93_Ui.enableText_GameOver();	// ゲームオーバーのテキストを表示させる。 WebGLで表示されない為シーンマネージャー実行
        
	}

	// ■■■isGmaeOverの値を返す■■■
	public bool getIsGameOver(){ return isGameOver; }
///エラーが出るためコメントアウト

// ■■■プレイヤーのHPを確認し、0なら、ゲームオーバー■■■
void player_DeadCheak()
    {
        if(c13_status.getHP() == 0)
        {
            Debug.Log("体力0チェック");
            SceneManager.LoadScene("GameOver");
            gameOver();
            player.GetComponent<Player>().enabled = false;
        }
    }
}
