using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> scoreTextList = new List<GameObject>();     // ScoreTextオブジェクトリスト 
    public List<GameObject> nameTextList = new List<GameObject>();      // NameTextオブジェクトリスト 

    public List<Person> personList = new List<Person>();                // パーソンリスト
    //public int[] scoreForDebug = new int[10];                           // For Debug
    //public string[] nameForDebug = new string[10];
    //public int num = 0;

    public struct Person
    {
        public string name;
        public int score;
    }

    public void AddRanking(int score , string name)
    {
        Person p = new Person();
        p.name = name;
        p.score = score;

        //パーソンリストに今回のパーソンを追加
        personList.Add(p);

        //スコア降順にソート
        personList.Sort((a, b) => b.score - a.score);

        //ソートしたデータを保存する
        for (int i = 0; i < 5; i++)
        {
            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();
            Text name_text = nameTextList[i].GetComponent<Text>();

            //ランキング登録者数の回数だけname/scoreを書き直す
            if (personList.Count > i)
            {
                // テキストの表示を入れ替える
                score_text.text = "Score:" + personList[i].score;
                name_text.text = personList[i].name;

                switch (i)
                {
                    case 0:
                        PlayerPrefs.SetInt("Score1", personList[i].score);
                        PlayerPrefs.SetString("Name1", personList[i].name);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("Score2", personList[i].score);
                        PlayerPrefs.SetString("Name2", personList[i].name);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("Score3", personList[i].score);
                        PlayerPrefs.SetString("Name3", personList[i].name);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("Score4", personList[i].score);
                        PlayerPrefs.SetString("Name4", personList[i].name);
                        break;
                    case 4:
                        PlayerPrefs.SetInt("Score5", personList[i].score);
                        PlayerPrefs.SetString("Name5", personList[i].name);
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
        //タイトルシーンになったら
        //PlayerPrefsのScoreデータ1～5をpersonList.scoreに入れる
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
                    p.score = PlayerPrefs.GetInt("Score2", 0);
                    p.name = PlayerPrefs.GetString("Name2", "no name");
                    break;
                case 2:
                    p.score = PlayerPrefs.GetInt("Score3", 0);
                    p.name = PlayerPrefs.GetString("Name3", "no name");
                    break;
                case 3:
                    p.score = PlayerPrefs.GetInt("Score4", 0);
                    p.name = PlayerPrefs.GetString("Name4", "no name");
                    break;
                case 4:
                    p.score = PlayerPrefs.GetInt("Score5", 0);
                    p.name = PlayerPrefs.GetString("Name5", "no name");
                    break;
            }
            personList.Add(p);

            // オブジェクトからTextコンポーネントを取得
            Text score_text = scoreTextList[i].GetComponent<Text>();
            Text name_text = nameTextList[i].GetComponent<Text>();

            // シーン開始時のスコアを表示
            score_text.text = "Score:" + personList[i].score;
            name_text.text = personList[i].name;
        }

        //Resultから戻ってきてハイスコア更新した場合、そのデータを入れる
        //"Score"と"Name"はResult画面で更新されている
        AddRanking(PlayerPrefs.GetInt("Score", 0), PlayerPrefs.GetString("Name", "no name"));

        //"Score"と"Name"を登録したのでリセット
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Name");

        //PlayerPrefs.SetInt("Score",0);
        //PlayerPrefs.SetString("Name", "no name");
        //PlayerPrefs.Save();   

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AddRanking(scoreForDebug[num] , nameForDebug[num]);
            //num++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // 保存データの全てを削除する
            //PlayerPrefs.DeleteAll();
        }
    }

    
}
