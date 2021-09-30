using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int numberVictory = 0;
    public int numberTry = 0;
    public List<Node> childrens = new List<Node>();
    public Action action;
    public Node parent;

    public Node()
    {
        numberVictory = 0;
        numberTry = 0;
        childrens = new List<Node>();
    }
    
    public void AddVictories(int nbVictory, int nbTry)
    {
        numberVictory += nbVictory;
        numberTry += nbTry;
    }

    public void AddChildren(Node children)
    {
        childrens.Add(children);
    }

    public void SetAction(Action action)
    {
        this.action = action;
    }
    
    public Action GetAction()
    {
        return action;
    }


    public int GetVictories()
    {
        return numberVictory;
    }
    
    public int GetTry()
    {
        return numberTry;
    }
    
    public List<Node> GetChildrens()
    {
        return childrens;
    }

    public void UpdateNode()
    {
        if (parent == null)
        {
            return;
        }
        foreach (var child in childrens)
        {
            numberVictory += child.GetVictories();
            numberTry += child.GetTry();
        }
        parent.UpdateNode();
    }

    public Node GetBestChild()
    {
        float max = -1;
        Node bestChild = null;
        foreach (var child in this.GetChildrens())
        {
            if(max < (float)child.GetVictories()/(float)child.GetTry()) //SimulationResult > -1
            {
                max = (float)child.GetVictories()/(float)child.GetTry();
                bestChild = child;
            }
        }

        return bestChild;
    }
}
