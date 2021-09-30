using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToOptions : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;
    
    public void Action()
    {
        optionPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    
}
