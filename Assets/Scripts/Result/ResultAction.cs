using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultAction : MonoBehaviour
{
    //Enter an arbutrary name
    public InputField PlayerName;
    public Text PlayerNameText;

    //Score Display
    public Text[] TotalScoreRank = new Text [5];
    public Text[] HitScore = new Text[5];
    public Text[] MissScore = new Text[5];
    float Elapsed = 0.0f;
    public string Score = "00000";
    public string Score1 = "00000";
    public string Score2 = "00000";

    // Start is called before the first frame update
    void Start()
    {
        //Enter an arbutrary name
        PlayerName = PlayerName.GetComponent<InputField>();
        PlayerNameText = PlayerNameText.GetComponent<Text>();

        //Score Display
        for (int idx = 0; idx < 5; idx++) {
            TotalScoreRank[idx].text = Score;
            HitScore[idx].text = Score1;
            MissScore[idx].text = Score2;
            //if (PlayerPrefs.GetFloat("R" + idx) >= float.MaxValue) {
            //    txtRank[idx].text = "_.__s";
            //} else {
            //    txtRank[idx].text = PlayerPrefs.GetFloat("R" + idx).ToString("f2") + "s";
            //}
        }
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        PlayerNameText.text = PlayerName.text;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Fire1"))
        {
            Elapsed += Time.deltaTime; //画面を押し続けている間ずっと   
            if (Elapsed > 3.0f)
            {
                SceneManager.LoadScene("Title");
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Elapsed = 0.0f; //画面押しを離した    
        }
    }
}
