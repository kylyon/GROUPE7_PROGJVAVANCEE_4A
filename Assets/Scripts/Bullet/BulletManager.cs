using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance()
    {
        return _singleton;}

    private static BulletManager _singleton;

    public static new Dictionary<Transform, (Vector3, Vector3)> bullets;

    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        bullets = new Dictionary<Transform, (Vector3, Vector3)>();
    }

    // Update is called once per frame
    void Update()
    {
        var bulletsList = new List<Transform>(bullets.Keys);
        foreach (var bullet in bulletsList)
        {
            var pos = bullets[bullet].Item1;
            pos += bullets[bullet].Item2 * (speed * Time.deltaTime);
            bullet.position = pos;
            bullets[bullet] = (pos, bullets[bullet].Item2);
        }
    }


    public void AddBullet(Transform transform,Vector3 bulletPosition, Vector3 direction)
    {
        bullets.Add(transform,(bulletPosition, direction));
    }
    
    public void RemoveBullet(Transform bullet)
    {
        bullets.Remove(bullet);
    }
    
}
