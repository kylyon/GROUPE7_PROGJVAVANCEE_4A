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

    public GameObject random;
    public GameObject mcts;

    private int p1Position = 0;
    private int p2Position = 0;

    public GameObject errorPlayer;
    public GameObject mainPanel;
    public GameObject playerSelectionPanel;

    public Animation camTransition;
    
    private float waitingTime = 0f;

    private void Start()
    {
        GameData.setPlayerHitman(0);
        GameData.setPlayerJoker(3);
    }


    // Update is called once per frame
    void Update()
    {
        waitingTime -= Time.deltaTime;
        
        if (waitingTime <= 0)
        {
            if (Input.GetAxis("P1_Horizontal") < 0 && p1Position >= 0)
            {
                p1Position--;
            }
            
            if (Input.GetAxis("P1_Horizontal") > 0 && p1Position <= 0)
            {
                p1Position++;
            }
            
            if (Input.GetAxis("P2_Horizontal") < 0 && p2Position >= 0)
            {
                p2Position--;
            }
            
            if (Input.GetAxis("P2_Horizontal") > 0 && p2Position <= 0)
            {
                p2Position++;
            }
            waitingTime = 0.3f;
        }

        p1Position = Mathf.Clamp(p1Position, -1, 1);
        p2Position = Mathf.Clamp(p2Position, -1, 1);

        if ( (p1Position == 1 && p2Position == 0) || (p2Position == 1 && p1Position == 0))
        {
            random.SetActive(true);
        }
        else
        {
            random.SetActive(false);
        }
        
        if ( (p1Position == -1 && p2Position == 0) || (p2Position == -1 && p1Position == 0))
        {
            mcts.SetActive(true);
        }
        else
        {
            mcts.SetActive(false);
        }
        
        
        p1Controller.rectTransform.anchoredPosition = new Vector2(p1Position * 400 ,p1Controller.rectTransform.anchoredPosition.y) ;
        p2Controller.rectTransform.anchoredPosition = new Vector2(p2Position * 400, p2Controller.rectTransform.anchoredPosition.y);

        if (Input.GetAxis("Cancel") > 0)
        {
            playerSelectionPanel.SetActive(false);
            mainPanel.SetActive(true);
            camTransition["PlayerSelection"].speed = -1;
            camTransition["PlayerSelection"].time = camTransition["PlayerSelection"].length;
            camTransition.Play();
        }

        if (Input.GetAxis("Action") > 0)
        {
            if (p1Position != p2Position)
            {
                switch (p1Position)
                {
                    case -1:
                        GameData.setPlayerHitman(1);
                        //Debug.Log(GameData.getPlayerHitman());
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
