using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Action
{
   Left, Right, Up, Down
}

public class Test : MonoBehaviour
{
    public Transform transformTest;
    public Vector3 destination;
    
    public Dictionary<Action, int> allPossibleActionsResult;
    public Dictionary<Action, (int, int)> allPossibleActionsMove;
    // Start is called before the first frame update
    void Start()
    {
        allPossibleActionsResult = new Dictionary<Action, int>();
        allPossibleActionsResult.Add(Action.Left, 0);
        allPossibleActionsResult.Add(Action.Right, 0);
        allPossibleActionsResult.Add(Action.Up, 0);
        allPossibleActionsResult.Add(Action.Down, 0);

        allPossibleActionsMove = new Dictionary<Action, (int, int)>();
        allPossibleActionsMove.Add(Action.Left, (-1, 0));
        allPossibleActionsMove.Add(Action.Right, (1, 0));
        allPossibleActionsMove.Add(Action.Up, (0, 1));
        allPossibleActionsMove.Add(Action.Down, (0, -1));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transformTest.position != destination)
        {
            ComputeMCTS();
        }
    }
    
    void ComputeMCTS()
    {
        int max = -1;
        Action bestAction = Action.Left;
        foreach (var possibleAction in allPossibleActionsMove) //Expansion
        {
            int numberVictory = 0;
            for (int i = 0; i < 100; i++)
            {
                numberVictory += SimulateResult(allPossibleActionsMove[possibleAction.Key]); //Simulation (a faire plusieurs fois !)
            }
            allPossibleActionsResult[possibleAction.Key] = numberVictory; //Retropropagation
            Debug.Log($"{possibleAction.Key} : {numberVictory}");
            if(max < allPossibleActionsResult[possibleAction.Key]) //SimulationResult > -1
            {
                max = allPossibleActionsResult[possibleAction.Key];
                bestAction = possibleAction.Key;
            }
        }
        transformTest.position += new Vector3(allPossibleActionsMove[bestAction].Item1, allPossibleActionsMove[bestAction].Item2, 0);
    }
    
    int SimulateResult((int, int) possibleAction)
    {
        List<Action> actions = new List<Action>(allPossibleActionsMove.Keys);
        var positionTemp = transformTest.localPosition;
        
        positionTemp += new Vector3(possibleAction.Item1, possibleAction.Item2, 0);
        
        var result = 1;
        while (positionTemp != destination) //Attention votre jeu doit être fini !
        {
            //List<Action> actions = Game.GetNextPossibleAction(possibleAction);
            int selectedAction = Random.Range(0, 4);
            
            positionTemp += new Vector3(allPossibleActionsMove[actions[selectedAction]].Item1, allPossibleActionsMove[actions[selectedAction]].Item2, 0);

            /*if ()
            {
                result++;
                return result;
            }*/
        }
        return result; //0 si perdu 1 si win
    }

}
