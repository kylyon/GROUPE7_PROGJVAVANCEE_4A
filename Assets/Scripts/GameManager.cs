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

    private int playerHitman;
    private int playerJoker;

    public float timeValue = 90;
    public TMP_Text textToDisplayTime;
    public TMP_Text hitmanScoreToDisplay;
    public TMP_Text jokerScoreToDisplay;
    public GameObject winEndMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        textToDisplayTime.text = timeValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                DisplayTime(timeValue);
            }
            else
            {
                timeValue = 0;
                winEndMenu.SetActive(true);
                Time.timeScale = 0f;
            }

            hitmanScoreToDisplay.text = BulletController.hitmanScore.ToString();
            jokerScoreToDisplay.text = BulletController.jokerScore.ToString();
        }
        
    }

    public bool SetPlayerHitman(int player)
    {
        if (playerHitman != 0)
        {
            return false;
        }

        playerHitman = player;
        return true;
    }
    
    public bool SetPlayerJoker(int player)
    {
        if (playerJoker != 0)
        {
            return false;
        }

        playerJoker = player;
        return true;
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

}
