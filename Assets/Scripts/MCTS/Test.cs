using System;
using System.Collections;
using System.Collections.Generic;
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
    public Vector3 destination;
    public Vector3 targetLastPosition;
    
    public Dictionary<Action, (int, int)> allPossibleActionsResult;
    public Dictionary<Action, (int, int)> allPossibleActionsMove;
    
    private Action bestAction;
    // Start is called before the first frame update
    void Start()
    {
        allPossibleActionsResult = new Dictionary<Action, (int, int)>();
        allPossibleActionsResult.Add(Action.Left, (0, 0));
        allPossibleActionsResult.Add(Action.Right, (0, 0));
        allPossibleActionsResult.Add(Action.Up, (0, 0));
        allPossibleActionsResult.Add(Action.Down, (0, 0));
        //allPossibleActionsResult.Add(Action.Shoot, 0);

        allPossibleActionsMove = new Dictionary<Action, (int, int)>();
        allPossibleActionsMove.Add(Action.Left, (-1, 0));
        allPossibleActionsMove.Add(Action.Right, (1, 0));
        allPossibleActionsMove.Add(Action.Up, (0, 1));
        allPossibleActionsMove.Add(Action.Down, (0, -1));
        //allPossibleActionsMove.Add(Action.Shoot, (0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        if (transformTest.position != target.position)
        {
            Debug.Log("Next Step");
            ComputeMCTS();
            transformTest.position += new Vector3(allPossibleActionsMove[bestAction].Item1, allPossibleActionsMove[bestAction].Item2, 0);
        }
        

        //targetLastPosition = target.position;


    }
    
    void ComputeMCTS()
    {
        int numberTry = 2;
        int max = -1;
        
        foreach (var possibleAction in allPossibleActionsMove) //Expansion
        {
            int numberVictory = 0;
            numberVictory = Expansion(numberTry);
            allPossibleActionsResult[possibleAction.Key] = (numberVictory, numberTry * 2); //Retropropagation
            Debug.Log($"{possibleAction.Key} : {numberVictory}");
            if(max < allPossibleActionsResult[possibleAction.Key].Item1) //SimulationResult > -1
            {
                max = allPossibleActionsResult[possibleAction.Key].Item1;
                bestAction = possibleAction.Key;
            }
        }
        
    }

    int Expansion( int nbTry)
    {
        if (nbTry == 0)
        {
            return 0;
        }
        
        int max = -1;
        int numberVictory = 0;
        
        foreach (var possibleAction in allPossibleActionsMove) //Expansion
        {
            
            for (int i = 0; i < 2; i++)
            {
                numberVictory += SimulateResult(possibleAction.Key); //Simulation (a faire plusieurs fois !)
            }
            //allPossibleActionsResult[possibleAction.Key] = (allPossibleActionsResult[possibleAction.Key].Item1 + numberVictory, allPossibleActionsResult[possibleAction.Key] + 30); //Retropropagation
            

        }

        return numberVictory + Expansion(nbTry--);
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
            
        }*/
        
        
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
    }

}
