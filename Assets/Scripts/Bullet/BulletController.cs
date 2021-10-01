using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static int hitmanScore;
    public static int jokerScore;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            //Debug.Log("Ground");
            BulletManager.Instance().RemoveBullet(this.transform);
            Destroy(this.gameObject);

            
        }
        
        if ((other.gameObject.tag.Equals("Hitman") || other.gameObject.tag.Equals("Joker")) && other.gameObject != gameObject)
        {
            if (other.gameObject.name == "hitman")
            {
                jokerScore += 1;
            }

            if (other.gameObject.name == "joker")
            {
                hitmanScore += 1;
            }
            BulletManager.Instance().RemoveBullet(this.transform);
            Destroy(this.gameObject);
            
        }
    }
}
