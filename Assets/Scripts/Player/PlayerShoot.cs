using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;

    public Transform shooterPosition;

    public int shootDirection = 1;

    private float fireRate = 0.6f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeDirection(int direction)
    {
        shootDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var newBullet = Instantiate(bullet, new Vector3(shooterPosition.position.x, shooterPosition.position.y, shooterPosition.position.z), Quaternion.Euler(0,0,0));
            BulletManager.Instance().AddBullet(newBullet.transform, shootDirection);
        }
    }
}