using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public int speed;
    public PlayerShoot ps;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        playerTransform.position += new Vector3(x, y) * speed * Time.fixedDeltaTime;

        if (x > 0)
        {
            playerTransform.rotation = Quaternion.Euler(180,0,180);
            ps.ChangeDirection(1);
        }
        
        if (x < 0)
        {
            playerTransform.rotation = Quaternion.Euler(180,0,0);
            ps.ChangeDirection(-1);
        }
    }
}
