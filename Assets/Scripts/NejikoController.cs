using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    // Start is called before the first frame update

    //1.プレイヤーのキー入力を受け取る
    //2.キー入力の方向に移動する
    //3.移動方向に合わせてアニメーションを再生する
    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    public float speed = 0f;
    Animator animator;
    
    public float jumpPower = 0f;//ジャンプの高さを決める変数
    public float gravityPower = 0f;//重力の強さを決める変数

    int MaxLine = 2;//ラインの数の最大値
    int MinLine = -2;//ラインの数の最小値
    float LineWidth = 1.0f;//ライン間の距離
    int targetLine = 0;//移動先のライン

    float StunTime = 0.5f;//敵キャラクターと当たった時に停止する時間
    float recoverTime;//キャラクターが止まってから動き出すまでの復帰時間
    public int playerHitPoint = 3;//プレイヤーのHP

    bool IsStan()//キャラクターがスタン中か判断するクラス
    {
        return recoverTime > 0.0f;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            //ねじ子がジャンプを行う処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
                animator.SetTrigger("jump");
            }
        }

        if (IsStan() == true)//敵に触れてスタン中なら前進しない 移動量を0に固定する
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
            recoverTime -= Time.deltaTime;//recoverTimeが0を切ったらIsStanがfalseになる
        }

        if (IsStan() == false)//敵に触れてスタン中なら前進しない
        {
            //1フレーム毎に前進する距離の更新
            float movePowerZ = moveDirection.z + (speed * Time.deltaTime);
            //更新した距離と現在地の差分距離の計算
            moveDirection.z = Mathf.Clamp(movePowerZ, 0f, speed);
        }

        //X方向は目標のポジションまでの差分で速度を出す
            float rationX = (targetLine * LineWidth - transform.position.x) / LineWidth;
        moveDirection.x = rationX * speed;
        //右レーン切り替え
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            if (controller.isGrounded && targetLine < MaxLine)
            {
                targetLine = targetLine + 1;
            }
        }
        //左レーン切り替え
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            if (controller.isGrounded && targetLine > MinLine)
            {
                targetLine = targetLine - 1;
            }
        }

        /*
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            //ねじ子が前進する処理
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            moveDirection.z = 0.0f;
        }
        */

        //Horizontal(左右入力)があれば、ねじこを回転させる
        //transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0



        //キャラクターが重力で落下する処理
        moveDirection.y = moveDirection.y - gravityPower * Time.deltaTime;

        //移動量をTransformに変換する
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //Controllerに移動量を流す
        controller.Move(globalDirection * Time.deltaTime);
        //ねじこのアニメーションを最新にする
        animator.SetBool("run", moveDirection.z > 0);

    }

    //敵キャラクターに当たった場合の処理を追加
    void OnControllerColliderHit(ControllerColliderHit hit)//CharacterControllerのコンポーネント持ってるやつが使える
    {
        if (hit.gameObject.tag == "Robo")//ぶつかった相手がRoboのタグを持っていたら
        {
            Debug.Log("敵にぶつかった！");
            recoverTime = StunTime;//recoverTimeの初期化
            playerHitPoint--;//HPマイナス1
            animator.SetTrigger("damage");//ダメージモーション発動
            Destroy(hit.gameObject);//ぶつかった相手を消す
        }
    }
}
