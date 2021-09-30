using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            optionPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }
}
