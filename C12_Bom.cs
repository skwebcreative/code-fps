using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C12_Bom : MonoBehaviour
{
    private C92_Sound	c92_sound;
    public GameObject prefab_HitEffect2;
    private int			bom_explosion_sound = 2;		// 手榴弾の爆発音No.
    [SerializeField]GameObject exprosion;
    // Start is called before the first frame update
    void Start()
    {
        c92_sound = GameObject.Find("Sound").GetComponent< C92_Sound >();
        StartCoroutine("bom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator bom()
    {
        yield return new WaitForSeconds(2.5f);

        GameObject effect = Instantiate(exprosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 1.0f);
        c92_sound.SendMessage("soundRings" , bom_explosion_sound);		// 手榴弾を爆発音を鳴らす.
        bomAttack();

        Destroy(gameObject);
    }
    private void bomAttack(){
        Collider[] targets = Physics.OverlapSphere(transform.position , 1.0f);	// 自分自身を中心に、半径0.7以内にいるColliderを探し、配列に格納.
		foreach(Collider obj in targets){		// targets配列を順番に処理 (その時に仮名をobjとする)
			if(obj.tag == "targetTag"){				// タグ名がEnemyなら
				Destroy(obj.gameObject);		// そのゲームオブジェクトを消滅させる。
            }
        }
    }
}
