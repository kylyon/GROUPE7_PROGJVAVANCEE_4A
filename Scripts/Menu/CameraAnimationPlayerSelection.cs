using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        MainPanel.SetActive(false);
        PlayerSelectionPanel.SetActive(true);
        camAnim.Play();
    }
}
