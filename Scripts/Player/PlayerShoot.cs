using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;

    public Transform shooterPosition;

    public Vector3 shootDirection = new Vector3(1,0,0);

    public int playerNumber;

    private float fireRate = 0.6f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangeDirection(Vector3 direction)
    {
        shootDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        fireRate -= Time.deltaTime;
        Debug.Log(fireRate);
        if (Input.GetAxis("P" + playerNumber + "_Shoot") > 0 && fireRate < 0)
        {
            var newBullet = Instantiate(bullet, new Vector3(shooterPosition.position.x, shooterPosition.position.y, shooterPosition.position.z), Quaternion.Euler(0,0,0));
            BulletManager.Instance().AddBullet(newBullet.transform, shootDirection);
            fireRate = 0.6f;
        }
        
    }
}