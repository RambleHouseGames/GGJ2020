using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked(int num)
    {
        //Debug.Log("clicked");
        //Load game scene
        
        SessionInfo.stageNum = num;
        SceneManager.LoadScene("GameScene");
    }
}
