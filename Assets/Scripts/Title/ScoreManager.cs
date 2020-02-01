using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> scoreTextList = new List<GameObject>();     // Textオブジェクトリスト
    //public List<int> scoreNumList = new List<int>();                  // スコア変数リスト

    public List<Person> personList = new List<Person>();                // パーソンリスト
    public int[] scoreForDebug = new int[10];                           // For Debug
    public 
    int num = 0;

    public struct Person
    {
        public string name;
        public int score;
    }

    public void AddRunking(int score , string name)
    {
        Person p = new Person();
        p.name = name;
        p.score = score;

        //パーソンリストに今回のパーソンを追加
        personList.Add(p);

        //降順にソート
        personList.Sort((a, b) => b.score - a.score);

        for (int i = 0; i < 5; i++)
        {
            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();

            if (personList.Count > i)
            {
                // テキストの表示を入れ替える
                score_text.text = "Score:" + personList[i].score;

                switch (i)
                {
                    case 0:
                        PlayerPrefs.SetInt("Score1", personList[i].score);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("Score2", personList[i].score);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("Score3", personList[i].score);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("Score4", personList[i].score);
                        break;
                    case 4:
                        PlayerPrefs.SetInt("Score5", personList[i].score);
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
        //Resultから戻ってきてハイスコア更新した場合、そのデータを入れる
        AddRunking(PlayerPrefs.GetInt("Score",0) , PlayerPrefs.GetString("Name" , "no name"));

        //PlayerPrefsのScoreデータをscoreNumListに入れる
        for (int i = 0; i < 5; i++)
        {
            Person p = new Person();

            switch (i)
            {
                case 0:
                    p.score = PlayerPrefs.GetInt("Score1", 0);
                    p.name = PlayerPrefs.GetString("Name1", "no name");
                    break;
                case 1:
                    p.score = PlayerPrefs.GetInt("Score1", 0);
                    p.name = PlayerPrefs.GetString("Name1", "no name");
                    break;
                case 2:
                    p.score = PlayerPrefs.GetInt("Score1", 0);
                    p.name = PlayerPrefs.GetString("Name1", "no name");
                    break;
                case 3:
                    p.score = PlayerPrefs.GetInt("Score1", 0);
                    p.name = PlayerPrefs.GetString("Name1", "no name");
                    break;
                case 4:
                    p.score = PlayerPrefs.GetInt("Score1", 0);
                    p.name = PlayerPrefs.GetString("Name1", "no name");
                    break;
            }

            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();

            // シーン開始時のスコアを表示
            score_text.text = "Score:" + personList[i].score;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddRunking(scoreForDebug[num] , "no name");
            num++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // 保存データの全てを削除する
            PlayerPrefs.DeleteAll();
        }
    }

    
}
