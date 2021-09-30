using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    private float waitingTime = 0f;

    private int menuSelect;

    public GameObject mainPanel;

    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        
        menuSelect = -1;
    }

    // Update is called once per frame
    void Update()
    {

        waitingTime -= Time.deltaTime;
        
        if (waitingTime <= 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                menuSelect++;
            }
            
            if (Input.GetAxis("Horizontal") < 0)
            {
                menuSelect--;
            }

            menuSelect = Mathf.Clamp(menuSelect, 0, buttons.Length - 1);
            waitingTime = 0.2f;
        }

        if (Input.GetAxis("Action") > 0)
        {
            buttons[menuSelect].onClick.Invoke();
        }
        
        if (menuSelect >= 0)
        {
            buttons[menuSelect].Select();
        }
        else
        {
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }
}
