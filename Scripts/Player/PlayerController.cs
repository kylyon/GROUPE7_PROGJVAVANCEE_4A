using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController playerController;
    public int playerNumber;
    public int speed;
    public PlayerShoot ps;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var x = Input.GetAxis("P" + playerNumber + "_Horizontal");
        var y = Input.GetAxis("P" + playerNumber + "_Vertical");

        playerController.Move(new Vector3(x, -2 * Time.deltaTime, y).normalized * (speed * Time.fixedDeltaTime));

        if (x > 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,180);
            ps.ChangeDirection(new Vector3(1,0,0));
        }
        
        if (x < 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,0);
            ps.ChangeDirection(new Vector3(-1,0,0));
        }
        
        if (y > 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,90);
            ps.ChangeDirection(new Vector3(0,0,1));
        }
        
        if (y < 0)
        {
            playerController.transform.rotation = Quaternion.Euler(-90,0,270);
            ps.ChangeDirection(new Vector3(0,0,-1));
        }
    }
}