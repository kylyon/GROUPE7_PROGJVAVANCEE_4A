using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;
    public int playerNumber;
    public int speed;
    public PlayerShoot ps;
    
    public Dictionary<Action, (int, int)> allPossibleActionsMove;
    List<Action> actions;

    private float x, y;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag.Equals("Hitman"))
        {
            playerNumber = GameData.getPlayerHitman();
        }
        if (this.gameObject.tag.Equals("Joker"))
        {
            playerNumber = GameData.getPlayerJoker();
        }
        
        Debug.Log($"{gameObject.tag} : {playerNumber}");

        x = 0;
        y = 0;
        
        allPossibleActionsMove = new Dictionary<Action, (int, int)>();
        allPossibleActionsMove.Add(Action.Left, (-1, 0));
        allPossibleActionsMove.Add(Action.Right, (1, 0));
        allPossibleActionsMove.Add(Action.Up, (0, 1));
        allPossibleActionsMove.Add(Action.Down, (0, -1));
        actions = new List<Action>(allPossibleActionsMove.Keys);
    }

    private void Update()
    {
        x = 0;
        y = 0;

        float newX = 0, newY = 0;

        if (playerNumber == 0 )
        {
            int selectedAction = Random.Range(0, 4);

            newX = allPossibleActionsMove[actions[selectedAction]].Item1;
            newY = allPossibleActionsMove[actions[selectedAction]].Item2;
        }

        if (playerNumber == 1 || playerNumber == 2)
        {
            newX = Input.GetAxis("P" + playerNumber + "_Horizontal");
            newY = Input.GetAxis("P" + playerNumber + "_Vertical");
        }
        
        Move(newX, newY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log($"{x}, {y}");
        
        playerController.Move(new Vector3(x, -2 * Time.deltaTime, y).normalized * (speed * Time.fixedDeltaTime));

        if (x > 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,180);
            ps.ChangeDirection(new Vector3(1,0,0));
        }
        
        if (x < 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,0);
            ps.ChangeDirection(new Vector3(-1,0,0));
        }
        
        if (y > 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,90);
            ps.ChangeDirection(new Vector3(0,0,1));
        }
        
        if (y < 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,270);
            ps.ChangeDirection(new Vector3(0,0,-1));
        }
    }

    public void Move(float newX, float newY)
    {
        x = newX;
        y = newY;
    }


}
