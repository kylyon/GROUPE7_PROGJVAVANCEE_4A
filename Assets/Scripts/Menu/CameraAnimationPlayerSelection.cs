using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAnimationPlayerSelection : MonoBehaviour
{
    public Animation camAnim;

    public GameObject MainPanel;
    public GameObject PlayerSelectionPanel;

    public Transform hitman;
    public Transform joker;
    
    // Start is called before the first frame update
    public void Action()
    {
        /*BulletController.hitmanScore = 0;
        BulletController.jokerScore = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/

        MainPanel.SetActive(false);
        PlayerSelectionPanel.SetActive(true);
        camAnim["PlayerSelection"].speed = 1;
        camAnim["PlayerSelection"].time = 0;
        camAnim.Play();
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
