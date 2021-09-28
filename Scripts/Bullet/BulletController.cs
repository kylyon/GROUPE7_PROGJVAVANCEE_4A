using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Test");
            BulletManager.Instance().RemoveBullet(this.gameObject.transform);
            
        }
        
        if (other.gameObject.tag.Equals("Player") && other.gameObject != gameObject)
        {
            Debug.Log("Test");
            BulletManager.Instance().RemoveBullet(this.gameObject.transform);
            
        }
    }
}
