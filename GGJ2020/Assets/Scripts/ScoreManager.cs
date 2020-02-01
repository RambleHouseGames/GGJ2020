using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> scoreTextList = new List<GameObject>();     // Textオブジェクトリスト
    public List<int> scoreNumList = new List<int>();                    // スコア変数リスト
    public int[] scoreForDebug = new int[5];                            // For Debug
    int num = 0;

    public void AddRunking(int score)
    {
        //スコアリストに今回のスコアを追加
        scoreNumList.Add(score);

        //降順にソート
        scoreNumList.Sort((a, b) => b - a);

        for (int i = 0; i < 5; i++)
        {
            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();

            if (scoreNumList.Count > i)
            {
                // テキストの表示を入れ替える
                score_text.text = "Score:" + scoreNumList[i];
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRunking(scoreForDebug[num]);
            num++;
        }
    }

    
}
