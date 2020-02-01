﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultAction : MonoBehaviour
{
    //Enter an arbutrary name
    public InputField PlayerName;
    public Text PlayerNameTextN;

    //Score Display
    int TotalScore = 0;
    int GreatScore = 0;
    int OkScore = 0;
    int MissScore = 0;
    public Text RankScore;
    public Text TotalScoreText;
    public Text GreatScoreText;
    public Text OkScoreText;
    public Text MissScoreText;
    /*public Text[] TotalScoreRank = new Text [5];
    public Text[] HitScore = new Text[5];
    public Text[] MissScore = new Text[5];*/
    public int Score = 0;
    public int Score1 = 0;
    public int Score2 = 0;
    public int Score3 = 0;
    float Elapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Score = SessionInfo.score;
        Score1 = SessionInfo.greatCount;
        Score2 = SessionInfo.okCount;
        Score3 = SessionInfo.missCount;

        //Enter an arbutrary name
        PlayerName = PlayerName.GetComponent<InputField>();
        PlayerNameTextN = PlayerNameTextN.GetComponent<Text>();

        //Score Display
        TotalScore = Score;
        GreatScore = Score1;
        OkScore = Score2;
        MissScore = Score3;
        TotalScoreText.text = "" + TotalScore;
        GreatScoreText.text = "" + GreatScore;
        OkScoreText.text = "" + OkScore;
        MissScoreText.text = "" + MissScore;

        //for (int idx = 0; idx < 5; idx++) {
        //TotalScoreRank[idx].text = Score;
        //HitScore[idx].text = Score1;
        //MissScore[idx].text = Score2;
        //if (PlayerPrefs.GetFloat("R" + idx) >= float.MaxValue) {
        //    txtRank[idx].text = "_.__s";
        //} else {
        //    txtRank[idx].text = PlayerPrefs.GetFloat("R" + idx).ToString("f2") + "s";
        //}
        //}

        if (TotalScore >= 0 && TotalScore <= 100)
        {
            RankScore.text = "D";
        }
        else if(TotalScore >= 101 && TotalScore <= 500)
        {
            RankScore.text = "C";
        }
        else if (TotalScore >= 501 && TotalScore <= 1000)
        {
            RankScore.text = "B";
        }
        else if (TotalScore >= 1001 && TotalScore <= 2000)
        {
            RankScore.text = "A";
        }
        else if (TotalScore >= 2001)
        {
            RankScore.text = "S";
        }
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        PlayerNameTextN.text = PlayerName.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Elapsed += Time.deltaTime; //画面を押し続けている間ずっと   
            if (Elapsed > 3.0f)
            {
                PlayerPrefs.SetInt("Score", Score);
                PlayerPrefs.SetString("Name", PlayerNameTextN.text);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Title");
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Elapsed = 0.0f; //画面押しを離した    
        }
    }
}
