using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Time;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance()
    {
        return _singleton;
    }

    private static GameManager _singleton;

    public static float timeValue = 90;
    public TMP_Text textToDisplayTime;
    public TMP_Text hitmanScoreToDisplay;
    public TMP_Text jokerScoreToDisplay;
    public GameObject winEndMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        DisplayTime(timeValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                DisplayTime(timeValue);

                if (BulletController.jokerScore + BulletController.hitmanScore > 0)
                {
                    EndGame();
                }
            }
            else
            {
                EndGame();
            }

            hitmanScoreToDisplay.text = BulletController.hitmanScore.ToString();
            jokerScoreToDisplay.text = BulletController.jokerScore.ToString();
        }
        
    }


    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textToDisplayTime.text = $"{minutes:00}:{seconds:00}";
    }

    void EndGame()
    {
        timeValue = 0;
        winEndMenu.SetActive(true);
        BulletController.hitmanScore = 0;
        BulletController.jokerScore = 0;
        Time.timeScale = 0f;
    }

}
