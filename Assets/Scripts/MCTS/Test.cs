using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum Action
{
   Left, Right, Up, Down, Shoot
}

public class Test : MonoBehaviour
{
    public Transform mctsTransform;
    public Transform target;

    public Dictionary<Action, (int, int)> allPossibleActionsResult;
    public Dictionary<Action, (int, int)> allPossibleActionsMove;

    private List<Action> actionsToDo;

    private Node root;

    public int height = 9999;

    private float time = 5f;

    //public float timeMCTS = GameManager.timeValue;
    
    private Action bestAction;
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(.)target.position);
        
        
        /*allPossibleActionsResult = new Dictionary<Action, (int, int)>();
        allPossibleActionsResult.Add(Action.Left, (0, 0));
        allPossibleActionsResult.Add(Action.Right, (0, 0));
        allPossibleActionsResult.Add(Action.Up, (0, 0));
        allPossibleActionsResult.Add(Action.Down, (0, 0));
        //allPossibleActionsResult.Add(Action.Shoot, 0);*/

        allPossibleActionsMove = new Dictionary<Action, (int, int)>();
        allPossibleActionsMove.Add(Action.Left, (-1, 0));
        allPossibleActionsMove.Add(Action.Right, (1, 0));
        allPossibleActionsMove.Add(Action.Up, (0, 1));
        allPossibleActionsMove.Add(Action.Down, (0, -1));
        allPossibleActionsMove.Add(Action.Shoot, (0, 0));

        actionsToDo = new List<Action>();

        root = new Node();
    }

    // Update is called once per frame
    void Update()
    {

        if (mctsTransform.gameObject.GetComponent<PlayerController>().playerNumber == 3)
        {
            if (GameManager.timeValue > 0)
            {
                //Debug.Log(.)"Next Step");

                for (int i = 0; i < height; i++)
                {
                    ComputeMCTS(GetBestAction());
                }
            

                bestAction = this.root.GetBestChild().GetAction();
            
                //Debug.Log(.)$"Best Action :{bestAction} : {root.GetBestChild().GetVictories()}/{root.GetBestChild().GetTry()}");

                /*foreach (var c in root.childrens)
                {
                    //Debug.Log(.)$"{c.action} : {c.GetVictories()}/{c.GetTry()}");
                }*/
            
                var x = allPossibleActionsMove[bestAction].Item1;
                var y = allPossibleActionsMove[bestAction].Item2;

                if (GameData.getPlayerJoker() == 3)
                {
                    mctsTransform.gameObject.GetComponent<PlayerController>().Move(x, y);

                }
            
                root = new Node();
                //UnityEditor.EditorApplication.isPlaying = false;
                //Debug.Log(.)"====================================");
            }
        }
        

    }
    
    void ComputeMCTS(Node root)
    {
        int numberTry = 30;

        foreach (var possibleAction in allPossibleActionsMove) //Expansion
        {
            Node temp = new Node();
            temp.parent = root;
            temp.SetAction(possibleAction.Key);
        
            int numberVictory = 0;
        
            for (int i = 0; i < numberTry; i++)
            {
                numberVictory += SimulateResult(possibleAction.Key, Time.deltaTime); //Simulation (a faire plusieurs fois !)
            }
        
            temp.AddVictories(numberVictory, numberTry); //Retropropagation

            root.AddChildren(temp);
        }
        root.UpdateNode();

    }

    Node GetBestAction()
    {
        Node bestChild = root;
        List<Node> childs = new List<Node>(root.GetChildrens());

        while (childs.Count > 0)
        {
            bestChild = bestChild.GetBestChild();
            if (bestChild.HasChildrens())
            {
                childs = new List<Node>(bestChild.GetChildrens());
            }
        }

        return bestChild;
    }
    
    int SimulateResult(Action possibleAction, float deltaTimeConstant)
    {
        List<Action> actions = new List<Action>(allPossibleActionsMove.Keys);
        var timeMCTS = GameManager.timeValue;
        
        var positionPlayerTemp = new Vector3(mctsTransform.localPosition.x, mctsTransform.localPosition.y, mctsTransform.localPosition.z);
        var positionTargetTemp = new Vector3(target.transform.position.x, target.position.y, target.transform.position.z);
        
        positionPlayerTemp += new Vector3(allPossibleActionsMove[possibleAction].Item1,0 , allPossibleActionsMove[possibleAction].Item2);
        int randomActionTarget = Random.Range(0, 4);

        positionTargetTemp += new Vector3(allPossibleActionsMove[actions[randomActionTarget]].Item1, 0,
            allPossibleActionsMove[actions[randomActionTarget]].Item2);

        List<(Vector3, Vector3)> positionBulletTemp = new List<(Vector3, Vector3)>();

        if (BulletManager.bullets.Count > 0)
        {
            foreach (var b in BulletManager.bullets)
            {
                positionBulletTemp.Add((b.Item1, b.Item2));
            }
        }
        
        var directionMCTS = allPossibleActionsMove[possibleAction];
        var directionTarget = allPossibleActionsMove[actions[randomActionTarget]];

        var result = 0;
        
        while (timeMCTS > 0) //Attention votre jeu doit être fini !
        {
            int selectedAction = Random.Range(0, 5);
            
            positionPlayerTemp += new Vector3(allPossibleActionsMove[actions[selectedAction]].Item1, 0, allPossibleActionsMove[actions[selectedAction]].Item2);

            if (actions[selectedAction] == Action.Shoot)
            {
                var posShootBull =
                    new Vector3(
                        positionPlayerTemp.x + (-0.015f * directionMCTS.Item1), 0,
                        positionPlayerTemp.z + (-0.014f * directionMCTS.Item2));
                positionBulletTemp.Add((posShootBull, new Vector3(directionMCTS.Item1, 0, directionMCTS.Item2)));
            }
            
            randomActionTarget = Random.Range(0, 5);

            positionTargetTemp += new Vector3(allPossibleActionsMove[actions[randomActionTarget]].Item1, 0, allPossibleActionsMove[actions[randomActionTarget]].Item2);
            
            if (actions[randomActionTarget] == Action.Shoot)
            {
                var posShootBull =
                    new Vector3(
                        positionTargetTemp.x + (-0.015f * directionTarget.Item1), 0,
                        positionTargetTemp.z + (-0.014f * directionTarget.Item2));
                positionBulletTemp.Add((posShootBull, new Vector3(directionTarget.Item1, 0, directionTarget.Item2)));
            }

            for (int i = 0; i < positionBulletTemp.Count; i++)
            {
                var tempNewPosBull = positionBulletTemp[i].Item1;
                tempNewPosBull += positionBulletTemp[i].Item2;
                positionBulletTemp[i] = (tempNewPosBull, positionBulletTemp[i].Item2);
                
                if (Vector3.Distance(positionBulletTemp[i].Item1, positionPlayerTemp) < 0.5)
                {
                    result = 0;
                    return result;
                }
                
                if (Vector3.Distance(positionBulletTemp[i].Item1, positionTargetTemp) < 0.5)
                {
                    result = 1;
                    return result;
                }
            }

            timeMCTS -= 1;
            directionMCTS = allPossibleActionsMove[actions[selectedAction]];
            directionTarget = allPossibleActionsMove[actions[randomActionTarget]];
        }
        
        return result; //0 si perdu 1 si win
    }

}
