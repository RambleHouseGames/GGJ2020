using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> scoreTextList = new List<GameObject>();     // Textオブジェクトリスト
    public List<int> scoreNumList = new List<int>();                    // スコア変数リスト
    public int[] scoreForDebug = new int[10];                            // For Debug
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

                switch (i)
                {
                    case 0:
                        PlayerPrefs.SetInt("Score1", scoreNumList[i]);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("Score2", scoreNumList[i]);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("Score3", scoreNumList[i]);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("Score4", scoreNumList[i]);
                        break;
                    case 4:
                        PlayerPrefs.SetInt("Score5", scoreNumList[i]);
                        break;

                }

                //スコアを保存
                PlayerPrefs.Save();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefsのScoreデータをscoreNumListに入れる
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    scoreNumList.Add(PlayerPrefs.GetInt("Score1", 0));
                    break;
                case 1:
                    scoreNumList.Add(PlayerPrefs.GetInt("Score2", 0));
                    break;
                case 2:
                    scoreNumList.Add(PlayerPrefs.GetInt("Score3", 0));
                    break;
                case 3:
                    scoreNumList.Add(PlayerPrefs.GetInt("Score4", 0));
                    break;
                case 4:
                    scoreNumList.Add(PlayerPrefs.GetInt("Score5", 0));
                    break;
            }

            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();

            // シーン開始時のスコアを表示
            score_text.text = "Score:" + scoreNumList[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRunking(scoreForDebug[num]);
            num++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // 保存データの全てを削除する
            PlayerPrefs.DeleteAll();
        }
    }

    
}
