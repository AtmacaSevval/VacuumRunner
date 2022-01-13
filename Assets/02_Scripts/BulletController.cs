using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    public Vector3 Target { get; set; }//target point of bullet


    private void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gameObject.tag = "bullet"; // change tag as a "bullet"
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * speed);//move from spawn point to target point
        
        if(Vector3.Distance(transform.position, Target) < 0.01f)//if bullet comes target point, then it will be destroyed.
        {
            
            Destroy(gameObject);
        }
    }

}
