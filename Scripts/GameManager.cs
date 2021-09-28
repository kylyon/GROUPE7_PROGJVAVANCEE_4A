using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance()
    {
        return _singleton;
    }

    private static GameManager _singleton;

    private int playerHitman;
    private int playerJoker;
    
    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
