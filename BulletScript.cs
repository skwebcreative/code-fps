using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    public GameObject targetEnemy;
    public ZombieAnim zom;
    [SerializeField] GameObject zombieAnimObje;
    public float rate = 0.075f;
    //現在の弾数
    //private float currentMagazine = 30f;
    
    //次弾
    const int magazineSize = 30;
    //全ての弾
    const int allBullets = 150;

    int currentMagazine = magazineSize; //現在のマガジン数
    //残り・リロード数
    private int remaining;

    public Text bulletText;
    public AmmoTextScript sc_ammoText;

    public GameObject bullet;

    Rigidbody rb;

    public int thrust = 30;
    //武器音
    private C92_Sound c92_sound;
    private int gun_sound = 0;
    private int bom_throw_sound = 1;
    private C11_Weapon	weapon;	// 武器切り替え
    
    public GameObject prefab_bom;

    private bool used_bom = false;

    // Start is called before the first frame update
    void Start()
    {
         zom = gameObject.GetComponent<ZombieAnim>();
         currentMagazine = magazineSize;
         remaining = allBullets - currentMagazine;
         Debug.Log("reming=(150-30):" + remaining);

         //c92_sound = GameObject.Find("Sound").GatComponent<C92_Sound>();
         c92_sound = GameObject.Find("Sound").GetComponent<C92_Sound>();


         bulletText.text = currentMagazine.ToString();
        int v = allBullets - magazineSize - remaining;
        bulletText.text = thrust + "/" + remaining;
        rb = GetComponent<Rigidbody>();
        // シーン上のAmmoオブジェクトにGunScriptをアタッチする。   
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        if(ammos.Length != 0)
        {
            foreach (GameObject ammo in ammos)
            {
                Debug.Log("AmmoオブジェクトにbulletScriptをアタッチ前");
                ammo.GetComponent<AmmoScript>().bulletscript = this;
                Debug.Log("AmmoオブジェクトにbulletScriptをアタッチ後");
            }
        }
        //武器切り替え用メモリを確保と初期化
        weapon = new C11_Weapon(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentMagazine > 0)
        { 
            attack_weapon(); 
        }else if (currentMagazine <= 0)
        {
            return;
        }
        if(Input.GetMouseButtonDown(1))
        { 
            change_weapon(); 
        } 
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R押された");
            if (currentMagazine + remaining > magazineSize) // 全残弾数がmagazineSizeより多いとき
            {
 
                remaining = currentMagazine + remaining - magazineSize; // 全残弾数からmagazineSizeを引く
                currentMagazine = magazineSize; // マガジンは満タン
 
            }
            else
            {// 全残弾数がmagazineSize以下のとき
 
                currentMagazine = currentMagazine + remaining; // 残っている弾を全てマガジンへ
                remaining = 0;
 
            }
 
            bulletText.text = currentMagazine + " / " + remaining; // 弾数を表示
        }   
    }
    private void change_weapon(){
		weapon.changeWeapon();		// 武器タイプを変更
	}
    private void attack_weapon(){
        switch(weapon.getType()){
            case 0:attack01_gun();break;
            case 1:attack02_bom();break;
        }
    }

    // ■■■銃による攻撃■■■
	private void attack01_gun(){
		//StartCoroutine("shoot");

            rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * thrust, ForceMode.Impulse);

            c92_sound.SendMessage("soundRings" , gun_sound);

            currentMagazine --;
            //bulletText.text = currentMagazine.ToString();
            bulletText.text = currentMagazine + " / " + remaining; // 弾数を表示
            Debug.Log("BulletScript前前前前前前前前");
            //zombieAnim.GetComponent< ZombieAnim >().atkDamage(GetComponent< C13_Status >());	// 攻撃した敵が持つ《C05_Enemy》コンポーネントのatkDamege()関数を実行
            
            //「ZombieAnim.atkDamage(C13_Status)」の必須の仮パラメータ「atk_status」に対応する引数が指定されていません
            //targetEnemy.SendMessage("damage"); ↓これを書き変え
            
            if(targetEnemy !=null){
                targetEnemy.GetComponent<ZombieAnim>().atkDamage(GetComponent<C13_Status>());
            }   
            
            Debug.Log("BulletScript後後後後後後後後");
	}
    private void attack02_bom(){
        if(!used_bom){
			Vector3 pos = transform.position + transform.TransformDirection(Vector3.forward);		// プレイヤー位置　+　プレイヤー正面にむけて１進んだ距離
			GameObject bom = Instantiate(prefab_bom , pos , Quaternion.identity) as GameObject;		// 手榴弾を作成
	
			Vector3 bom_speed = transform.TransformDirection(Vector3.forward)  * 5;		// 手榴弾の移動速度。『プレイヤー正面に向けての速度ベクトル』を５。
			bom_speed += Vector3.up * 5;			// 手榴弾の『高さ方向の速度』を加算
			bom.GetComponent<Rigidbody>().velocity = bom_speed;		// 手榴弾の速度を代入
	
			bom.GetComponent<Rigidbody>().angularVelocity = Vector3.forward * 7;	// 手榴弾を回転速度を代入.

			c92_sound.SendMessage("soundRings" , bom_throw_sound);		// 手榴弾を投げる音を鳴らす.

			used_bom = true;
            StartCoroutine("reChargeBom");	
        }
    }
    // ■■■手榴弾の速射管理コルーチン■■■
	IEnumerator reChargeBom(){
		yield return new WaitForSeconds(0.1f);		// 2.5秒、処理を待機.
		used_bom = false;
	}

    public int PickUpAmmo(int ammo)
    {
        int v = allBullets - magazineSize - remaining;

        if(v >= ammo)
        {
            //補充できる数が多い場合0が返ってマガジンが消える
            remaining +=ammo;
            bulletText.text = currentMagazine + " / " + remaining; // 弾数を表示
            sc_ammoText.displayText(ammo.ToString());
            return 0;
        }
        else{
            remaining += v;
            bulletText.text = currentMagazine + " / " + remaining; // 弾数を表示
            if(v > 0) sc_ammoText.displayText(v.ToString()); // 補充した弾数を表示
            return ammo - v;            
        }
    }
    
} 
