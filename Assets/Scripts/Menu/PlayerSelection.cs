using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public Image p1Controller;
    public Image p2Controller;

    private int p1Position = 0;
    private int p2Position = 0;

    public GameObject errorPlayer;

    private void Start()
    {
        GameData.setPlayerHitman(0);
        GameData.setPlayerJoker(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && p1Position >= 0)
        {
            p1Position--;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && p1Position <= 0)
        {
            p1Position++;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && p2Position >= 0)
        {
            p2Position--;
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) && p2Position <= 0)
        {
            p2Position++;
        }
        
        
        p1Controller.rectTransform.anchoredPosition = new Vector2(p1Position * 400 ,p1Controller.rectTransform.anchoredPosition.y) ;
        p2Controller.rectTransform.anchoredPosition = new Vector2(p2Position * 400, p2Controller.rectTransform.anchoredPosition.y);
        
        Debug.Log(p1Position);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (p1Position != p2Position)
            {
                switch (p1Position)
                {
                    case -1:
                        GameData.setPlayerHitman(1);
                        Debug.Log(GameData.getPlayerHitman());
                        break;
                    case 1:
                        GameData.setPlayerJoker(1);
                        break;
                }
                
                switch (p2Position)
                {
                    case -1:
                        GameData.setPlayerHitman(2);
                        break;
                    case 1:
                        GameData.setPlayerJoker(2);
                        break;
                }
                
                BulletController.hitmanScore = 0;
                BulletController.jokerScore = 0;
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                errorPlayer.SetActive(true);
            }
        }
        
    }
}
