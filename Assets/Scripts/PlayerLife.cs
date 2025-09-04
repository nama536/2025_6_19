using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private int playerHitPoint = 3;//プレイヤーのHP管理変数(容易に触れないようにprivateで)
    public List<GameObject> playerLifeUI;//プレイヤーのライフ表示処理(ライフの表示＝UIを変動させる)
    public bool hitFlag = false;//プレイヤーが敵に当たった時の処理


    public void PlayerHitPointDown()//プレイヤーのライフを減らす処理(外部から呼び出せるようにpublicで)
    {
        playerHitPoint--;//playerHitPoint = playerHitPoint - 1 の略
        Debug.Log(playerHitPoint);
    }

    void Update()
    {
        if (hitFlag == true)//プレイヤーが敵に当たったらHPを減らす処理を呼ぶ
        {
            PlayerHitPointDown();

            playerLifeUI[playerHitPoint].SetActive(false);//playerLifeUIのplayerHitPoint番目のオブジェクトを消す　HPの数に応じてハート表示

            hitFlag = false;
        }

        
    }
}
