using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance()
    {
        return _singleton;}

    private static BulletManager _singleton;

    public static List<(Vector3, Vector3)> bullets;

    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _singleton = this;
        bullets = new List<(Vector3, Vector3)>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var pos = bullets[i].Item1;
            pos += bullets[i].Item2 * (speed * Time.deltaTime);
            bullets[i] = (pos, bullets[i].Item1);
        }
    }


    public int AddBullet(Vector3 bullet, Vector3 direction)
    {
        bullets.Add((bullet, direction));
        return bullets.Count - 1;
    }
    
    public void RemoveBullet(int bullet)
    {
        bullets.RemoveAt(bullet);
    }
    
}
