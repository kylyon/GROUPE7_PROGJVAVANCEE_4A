using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public static Canvas canvaMenu;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseGame("Pause");
    }

    public static void PauseGame(string State)
    {
        List<GameObject> menuUIGameObject = new List<GameObject>();

        foreach (var transformChild in canvaMenu.GetComponentsInChildren<Transform>())
        {
            menuUIGameObject.Add(transformChild.gameObject);
        }

        switch (State)
        {
            case "Pause":
                menuUIGameObject[0].SetActive(false);
                menuUIGameObject[1].SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
                break;
            
            case "WindEnd":
                menuUIGameObject[0].SetActive(false);
                menuUIGameObject[2].SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
                break;

            default:
                break;
        }

    }

    public void Quitter()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


}