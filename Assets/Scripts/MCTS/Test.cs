using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Action
{
   Left, Right, Up, Down, Shoot
}

public class Test : MonoBehaviour
{
    public Transform transformTest;
    public Transform target;


    public Collider redCube;

    public Collider bullet;

    public PlayerShoot ps;
    
    
    public Vector3 destination;
    public Vector3 targetLastPosition;
    
    public Dictionary<Action, (int, int)> allPossibleActionsResult;
    public Dictionary<Action, (int, int)> allPossibleActionsMove;

    private List<Action> actionsToDo;

    private Node root;

    private float time = 5f;

    public float timeMCTS = GameManager.timeValue;
    
    private Action bestAction;
    // Start is called before the first frame update
    void Start()
    {
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
        //allPossibleActionsMove.Add(Action.Shoot, (0, 0));

        actionsToDo = new List<Action>();

        root = new Node();
    }

    // Update is called once per frame
    void Update()
    {

        if (transformTest.position != target.position)
        {
            Debug.Log("Next Step");
            ComputeMCTS(root, 5);
            Debug.Log($"{bestAction} : {root.GetVictories()}/{root.GetTry()}");
            transformTest.position += new Vector3(allPossibleActionsMove[bestAction].Item1, allPossibleActionsMove[bestAction].Item2, 0);

            //actionsToDo = new List<Action>();
            
            root = new Node();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
        

        //targetLastPosition = target.position;


    }
    
    void ComputeMCTS(Node root, int height)
    {
        if (height < 0)
        {
            return;
        }
        
        int numberTry = 30;
        float max = -1;
        
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
            
            //allPossibleActionsResult[possibleAction.Key] = (numberVictory, numberTry); //Retropropagation

            root.AddChildren(temp);
        }
        root.UpdateNode();

        Node bestChild = null;
        
        foreach (var child in root.GetChildrens())
        {
            if(max < (float)child.GetVictories()/(float)child.GetTry()) //SimulationResult > -1
            {
                max = (float) child.GetVictories() / (float) child.GetTry();
                bestChild = child;
            }
            
            //Debug.Log($"{child.GetAction()} : {child.GetVictories()}/{child.GetTry()}");
            
            //Debug.Log($"{height}");
        }
        //actionsToDo.Add(bestAction);
        ComputeMCTS(bestChild, height - 1);
        bestAction = bestChild.GetAction();
        
        //Debug.Log($"{bestAction} : {max}");
        //Debug.Log("============================");
    }
    
    int SimulateResult(Action possibleAction, float deltaTimeConstant)
    {
        List<Action> actions = new List<Action>(allPossibleActionsMove.Keys);
        var positionPlayerTemp = transformTest.localPosition;
        Dictionary<Transform, Vector3> positionBulletTemp = new Dictionary<Transform, Vector3>();
        
        /*var isBulletShot = 0;
        List<Vector3> positionBulletTemp = new List<Vector3>();

        var positionEnemyTemp = redCube.transform.localPosition;*/
        
        positionPlayerTemp += new Vector3(allPossibleActionsMove[possibleAction].Item1, allPossibleActionsMove[possibleAction].Item2, 0);

        if (BulletManager.bullets.Count > 0)
        {
            foreach (var b in BulletManager.bullets)
            {
                positionBulletTemp.Add(b.Key, b.Value);
            }
        }

        /*if (possibleAction == Action.Shoot)
        {
            isBulletShot++;
            positionBulletTemp.Add(transformTest.transform.localPosition + new Vector3(1,0,0));
        }*/
        
        var result = 1;
        while (timeMCTS > 0) //Attention votre jeu doit être fini !
        {
            //List<Action> actions = Game.GetNextPossibleAction(possibleAction);
            int selectedAction = Random.Range(0, 4);
            
            positionPlayerTemp += new Vector3(allPossibleActionsMove[actions[selectedAction]].Item1, allPossibleActionsMove[actions[selectedAction]].Item2, 0);
            Debug.Log(positionBulletTemp);
            foreach (var b in positionBulletTemp)
            {
                b.Key.position += b.Value;
            }
            Debug.Log(positionBulletTemp);
            
            /*if (actions[selectedAction] == Action.Shoot)
            {
                isBulletShot++;
                positionBulletTemp.Add(transformTest.transform.localPosition + new Vector3(1,0,0));
            }

            for (var i = 0; i < positionBulletTemp.Count; i++)
            {
                positionBulletTemp[i] += new Vector3(1, 0, 0);
                
                if (-20 > positionBulletTemp[i].x || positionBulletTemp[i].x > 20)
                {
                    isBulletShot--;
                }
            
                if (-20 > positionBulletTemp[i].y || positionBulletTemp[i].y > 20)
                {
                    isBulletShot--;
                }
                
            }*/
            
            if (-29 > positionPlayerTemp.x || positionPlayerTemp.x > 29)
            {
                result = 0;
                return result;
            }
            
            if (-41 > positionPlayerTemp.y || positionPlayerTemp.y > 18)
            {
                result = 0;
                return result;
            }

            timeMCTS -= deltaTimeConstant;

        }
        
        
        return result; //0 si perdu 1 si win
    }
    
    
    
    /*void ComputeMCTS()
    {
        int numberTry = 30;
        int max = -1;
        
        foreach (var possibleAction in allPossibleActionsMove) //Expansion
        {
            int numberVictory = 0;
            for (int i = 0; i < numberTry; i++)
            {
                numberVictory += SimulateResult(possibleAction.Key); //Simulation (a faire plusieurs fois !)
            }
            allPossibleActionsResult[possibleAction.Key] = (numberVictory, numberTry); //Retropropagation
            Debug.Log($"{possibleAction.Key} : {numberVictory}");
            if(max < allPossibleActionsResult[possibleAction.Key].Item1) //SimulationResult > -1
            {
                max = allPossibleActionsResult[possibleAction.Key].Item1;
                bestAction = possibleAction.Key;
            }
        }
        
    }
    
    int SimulateResult(Action possibleAction)
    {
        List<Action> actions = new List<Action>(allPossibleActionsResult.Keys);
        var positionTemp = transformTest.localPosition;

        /*if (possibleAction == Action.Shoot)
        {
            var tH = Math.Abs(positionTemp.x - target.position.x);
            var tPH = Math.Abs(positionTemp.x - target.position.x) / 5;
            
            var xH = 1 * Math.Cos(0) * tH + positionTemp.x;
            var yH = 1 * Math.Sin(0) * tH + positionTemp.y;
            
            var xTH = 5 * Math.Cos(0) * tPH;
            var yTH = 5 * Math.Sin(0) * tPH;
            
            var tV = Math.Abs(positionTemp.y - target.position.y);
            var tPV = Math.Abs(positionTemp.y - target.position.y) / 5;

            var xTV = 5 * 0 * tPV + target.position.x;
            var yTV = 5 * 1 * tPV + target.position.y;
            
            var xV = 1 * 0 * tV;
            var yV = 1 * 1 * tV;
            Debug.Log($"({xH}, {yH}) - ({xTV}, {yTV})");
            
            Debug.Log(xH == xTV && yH == yTV);
        }
        else
        {
            
        }
        
        
        positionTemp += new Vector3(allPossibleActionsMove[possibleAction].Item1, allPossibleActionsMove[possibleAction].Item2, 0);
        
        var result = 1;
        while (positionTemp != target.position) //Attention votre jeu doit être fini !
        {
            //List<Action> actions = Game.GetNextPossibleAction(possibleAction);
            int selectedAction = Random.Range(0, 4);
            
            positionTemp += new Vector3(allPossibleActionsMove[actions[selectedAction]].Item1, allPossibleActionsMove[actions[selectedAction]].Item2, 0);

            if (-40 > positionTemp.x || positionTemp.x > 40)
            {
                result = 0;
                return result;
            }
            
            if (-40 > positionTemp.y || positionTemp.y > 40)
            {
                result = 0;
                return result;
            }
        }
        return result; //0 si perdu 1 si win
    }*/

}
