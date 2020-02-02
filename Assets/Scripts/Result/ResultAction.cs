using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultAction : MonoBehaviour
{
    //Enter an arbutrary name
    public InputField PlayerName;
    public Text PlayerNameTextN;
    public GameObject backToTitle;

    //Score Display
    int TotalScore = 0;
    int GreatScore = 0;
    int OkScore = 0;
    int MissScore = 0;
    public Text RankScore;
    public Text RankScore1;
    public Text TotalScoreText;
    public Text GreatScoreText;
    public Text OkScoreText;
    public Text MissScoreText;
    public int Score = 0;
    public int Score1 = 0;
    public int Score2 = 0;
    public int Score3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = SessionInfo.score;
        Score1 = SessionInfo.greatCount;
        Score2 = SessionInfo.okCount;
        Score3 = SessionInfo.missCount;

        //PlayerName.text = "no name";

        //Score Display
        TotalScore = Score;
        GreatScore = Score1;
        OkScore = Score2;
        MissScore = Score3;
        TotalScoreText.text = "" + TotalScore;
        GreatScoreText.text = "" + GreatScore;
        OkScoreText.text = "" + OkScore;
        MissScoreText.text = "" + MissScore;

        if (TotalScore >= 0 && TotalScore <= 3000)
        {
            RankScore.text = "D";
            RankScore.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
            RankScore1.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        }
        else if (TotalScore >= 3001 && TotalScore <= 7000)
        {
            RankScore.text = "C";
            RankScore.color = new Color(50f / 255f, 60f / 255f, 230f / 255f);
            RankScore1.color = new Color(50f / 255f, 60f / 255f, 230f / 255f);
        }
        else if (TotalScore >= 7001 && TotalScore <= 10000)
        {
            RankScore.text = "B";
            RankScore.color = new Color(5f / 255f, 150f / 255f, 15f / 255f);
            RankScore1.color = new Color(5f / 255f, 150f / 255f, 15f / 255f);
        }
        else if (TotalScore >= 10001 && TotalScore <= 12000)
        {
            RankScore.text = "A";
            RankScore.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
            RankScore1.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        }
        else if (TotalScore >= 12001 && TotalScore <= 13000)
        {
            RankScore.text = "S";
            RankScore.color = new Color(230f / 255f, 210f / 255f, 0f / 255f);
            RankScore1.color = new Color(230f / 255f, 210f / 255f, 0f / 255f);
        }
        else if (TotalScore >= 13001)
        {
            RankScore.text = "SS";
            RankScore.color = new Color(255f / 255f, 255f / 255f, 0.0f / 255f);
            RankScore1.color = new Color(255f / 255f, 255f / 255f, 0.0f / 255f);
        }
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        PlayerNameTextN.text = PlayerName.text;
    }

    public void Submitname()
    {
        //PlayerName.gameObject.SetActive(false);
        //backToTitle.SetActive(true);

        if (string.IsNullOrEmpty(PlayerName.text))
        {
            PlayerName.text = "no name";
        }
    }

    public void Titlename()
    {
        if (string.IsNullOrEmpty(PlayerName.text))
        {
            PlayerName.text = "no name";
        }

        //backToTitle.SetActive(false);
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetString("Name", PlayerNameTextN.text);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
