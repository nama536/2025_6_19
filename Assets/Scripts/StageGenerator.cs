using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public Transform character;//ターゲットキャラクターの保持用変数
    public GameObject[] stageChip;//ステージのPrefabを配列で管理する変数
    public List<GameObject> generateStageList = new List<GameObject>();//Sceneに実体化させたステージのPrefabを管理する配列
    public int preInstance = 5;//ステージ数をカウントするインデックス
    public int currentChipIndex;//

    void Start()
    {
        
    }

    void Update()
    {
        //キャラクターの現在位置から現在のステージチップのインデックスを計算
        int characterPositionIndex = (int)(character.position.z / 30f);//キャラが30進んだら1増える変数
        Debug.Log(characterPositionIndex);

        //キャラクターが進んだらステージチップを追加で生成する
        for (int i = preInstance + characterPositionIndex; i >= preInstance; i++)
        {
            //最初に作ったステージ数+自分の通過したステージ数だけステージを生成する
            if (generateStageList.Count > preInstance + characterPositionIndex)
            {
                return;
            }

            int randomValue = Random.Range(0,stageChip.Length);//乱数を生成する
            Debug.Log(randomValue);

            GameObject stageObject = Instantiate(stageChip[randomValue]);//ここで作るステージをランダムに変更する
            stageObject.transform.position = new Vector3(0, 0, currentChipIndex * 30f);
            
            generateStageList.Add(stageObject);//生成したステージチップを管理リストに追加
            currentChipIndex++;
        }

            //次のステージチップに入ったらステージの最新処理を行う
    }
}
