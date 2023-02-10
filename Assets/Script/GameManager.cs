using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    //    public Button restartButton;

    public Sprite gameOver;
    public Sprite gameClear;

    Image titleImage;

    string oldGameState;

    public GameObject timeBar;
    public GameObject timeText;
    TimeCtrl timeCnt;

    public GameObject scoreText;        // 점수 텍스트
    public static int totalScore;       // 점수 총합
    public int stageScore = 0;          // 스테이지 점수

    public void Start()
    {
                Invoke("InactiveImage", 1.0f);

        oldGameState = PlayerCtrl.gameState;

        timeCnt = GetComponent<TimeCtrl>();
        if(timeCnt != null)
        {
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(true);
            }
        }

        UpdateScore();   
    }
    private void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);


    }

    //// Update is called once per frame
    void Update()
    {
        //if (oldGameState != PlayerCtrl.gameState)
        //{
        //    Debug.Log(PlayerCtrl.gameState);
        //}

        if (PlayerCtrl.gameState == "gameClear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button btRestart = restartButton.GetComponent<Button>();
            btRestart.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClear;
            PlayerCtrl.gameState = "gameClear";

            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;

                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }

            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();

        }
        else if (PlayerCtrl.gameState == "gameOver")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button btNext = nextButton.GetComponent<Button>();
            btNext.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOver;
            PlayerCtrl.gameState = "gameOver";

            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }
        } 

        else if (PlayerCtrl.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();

            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<Text>().text = time.ToString();

                    if(time == 0)
                    {
                        playerCtrl.GameStop();
                    }
                }
            }

            if (playerCtrl.score !=0)
            {
                stageScore += playerCtrl.score;

                playerCtrl.score = 0;
                UpdateScore();
            }
        }
    }
}
