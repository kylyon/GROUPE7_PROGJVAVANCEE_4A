using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance()
    {
        return _singleton;}

    private static BulletManager _singleton;

    private Dictionary<Transform, Vector3> bullets;

    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        bullets = new Dictionary<Transform, Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bullet in bullets)
        {
            bullet.Key.position += bullet.Value * Time.fixedDeltaTime;
        }
    }


    public void AddBullet(Transform bullet, Vector3 direction)
    {
        bullets.Add(bullet, direction);
    }
    
    public void RemoveBullet(Transform bullet)
    {
        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }
    
}
