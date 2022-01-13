using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField]private Transform vacuumTransform;
    Stack stack;
    bool collected;

    
    float counter;
    Vector3 targetScale;
    Vector3 startScale;
    void Start()
    {

        stack = FindObjectOfType<Stack>();
        targetScale = new Vector3(0.2f, 0.2f, 0.2f);
        startScale = transform.localScale;
    }
    

 
    // Update is called once per frame
    void Update()
    {
        if (!collected && transform.position.z <= stack.transform.position.z)
        {
            Destroy(GetComponent<GhostScript>());
        }

        float distance = Vector3.Distance(transform.position, vacuumTransform.position);
        if (distance < 0.1f)
        {
            
            stack.AddToStack(gameObject);
            transform.localScale = startScale;
            Destroy(GetComponent<GhostScript>());
        }
        else if (distance < 2f)
        {
            collected = true;

            

            //Debug.Log(heading + " " + heading.normalized);

            transform.position = Vector3.MoveTowards(transform.position,vacuumTransform.position,12f * Time.deltaTime);

            

            counter += Time.deltaTime;
            float shrinkFactor = counter / 5f;
            Shrink(shrinkFactor, targetScale);
        }

        
    }


    void Shrink(float shrinkFactor, Vector3 targetScale)
    {
        Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale, shrinkFactor);
        transform.localScale = newScale;
    }
}
