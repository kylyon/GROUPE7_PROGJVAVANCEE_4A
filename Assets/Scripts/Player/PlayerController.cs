using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;
    public int playerNumber;
    public int speed;
    public PlayerShoot ps;
    
    public Dictionary<Action, (int, int)> allPossibleActionsMove;
    List<Action> actions;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        /*if (this.gameObject.tag.Equals("Hitman"))
        {
            playerNumber = GameData.getPlayerHitman();
        }
        if (this.gameObject.tag.Equals("Joker"))
        {
            playerNumber = GameData.getPlayerJoker();
        }*/
        
        
        Debug.Log(GameData.getPlayerHitman());
        Debug.Log(playerNumber);

        allPossibleActionsMove = new Dictionary<Action, (int, int)>();
        allPossibleActionsMove.Add(Action.Left, (-1, 0));
        allPossibleActionsMove.Add(Action.Right, (1, 0));
        allPossibleActionsMove.Add(Action.Up, (0, 1));
        allPossibleActionsMove.Add(Action.Down, (0, -1));
        actions = new List<Action>(allPossibleActionsMove.Keys);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float x = 0;
        float y = 0;

        if (playerNumber == 0)
        {
            int selectedAction = Random.Range(0, 4);
            
            x = allPossibleActionsMove[actions[selectedAction]].Item1;
            y = allPossibleActionsMove[actions[selectedAction]].Item2;
        }

        if (playerNumber == 1 || playerNumber == 2)
        {
            x = Input.GetAxis("P" + playerNumber + "_Horizontal");
            y = Input.GetAxis("P" + playerNumber + "_Vertical");
        }
        
        

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
}
