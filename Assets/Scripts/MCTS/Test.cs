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
        
        Debug.Log(target.position);
        
        
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

        if (Vector3.Distance(transformTest.position , target.position) > 2)
        {
            //Debug.Log("Next Step");
            ComputeMCTS(root, 5);

            bestAction = this.root.GetBestChild().GetAction();
            
            //Debug.Log($"Best Action :{bestAction} : {root.GetBestChild().GetVictories()}/{root.GetBestChild().GetTry()}");

            
            
            var x = allPossibleActionsMove[bestAction].Item1;
            var y = allPossibleActionsMove[bestAction].Item2;
            
            transformTest.gameObject.GetComponent<PlayerController>().Move(x, y);

            root = new Node();
            //UnityEditor.EditorApplication.isPlaying = false;
        }

    }
    
    void ComputeMCTS(Node root, int height)
    {
        if (height < 0)
        {
            return;
        }
        
        int numberTry = 15;
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
        }
        
        ComputeMCTS(bestChild, height - 1);

    }
    
    int SimulateResult(Action possibleAction, float deltaTimeConstant)
    {
        List<Action> actions = new List<Action>(allPossibleActionsMove.Keys);
        var positionPlayerTemp = transformTest.localPosition;
        //timeMCTS = GameManager.timeValue;
        
        //Dictionary<Transform, Vector3> positionBulletTemp = new Dictionary<Transform, Vector3>();

        positionPlayerTemp += new Vector3(allPossibleActionsMove[possibleAction].Item1,0 , allPossibleActionsMove[possibleAction].Item2);

        /*if (BulletManager.bullets.Count > 0)
        {
            foreach (var b in BulletManager.bullets)
            {
                positionBulletTemp.Add(b.Key, b.Value);
            }
        }*/

        var result = 1;
        while (Math.Abs( Vector3.Distance(positionPlayerTemp , target.position)) > 2) //Attention votre jeu doit être fini !
        {
            int selectedAction = Random.Range(0, 4);
            
            positionPlayerTemp += new Vector3(allPossibleActionsMove[actions[selectedAction]].Item1, 0, allPossibleActionsMove[actions[selectedAction]].Item2);
            
            
            /*foreach (var b in positionBulletTemp)
            {
                b.Key.position += b.Value;
            }*/

            if (-26 > positionPlayerTemp.x || positionPlayerTemp.x > 26)
            {
                result = 0;
                return result;
            }
            
            if (-38 > positionPlayerTemp.z || positionPlayerTemp.z > 15)
            {
                result = 0;
                return result;
            }

            //timeMCTS -= deltaTimeConstant;

        }
        
        
        return result; //0 si perdu 1 si win
    }

}
