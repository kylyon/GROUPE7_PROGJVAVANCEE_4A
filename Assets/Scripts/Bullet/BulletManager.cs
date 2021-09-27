using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance()
    {
        return _singleton;}

    private static BulletManager _singleton;

    private Dictionary<Transform, int> bullets;

    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        bullets = new Dictionary<Transform, int>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bullet in bullets)
        {
            bullet.Key.position += new Vector3(bullet.Value, 0, 0) * Time.fixedDeltaTime;
        }
    }


    public void AddBullet(Transform bullet, int direction)
    {
        bullets.Add(bullet, direction);
    }
    
    public void RemoveBullet(Transform bullet)
    {
        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }
    
}
