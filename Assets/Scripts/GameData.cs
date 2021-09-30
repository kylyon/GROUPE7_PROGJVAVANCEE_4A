using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
 
    private static int playerHitman = 0;
    private static int playerJoker = 3;

    public static void setPlayerHitman(int player)
    {
        playerHitman = player;
    }
    
    public static void setPlayerJoker(int player)
    {
        playerJoker = player;
    }
    
    public static int getPlayerHitman()
    {
        return playerHitman;
    }
    
    public static int getPlayerJoker()
    {
        return playerJoker;
    }
    
}
