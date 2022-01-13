using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject parentOfStack;
    public static int numberOfBalls = 0;

    private void Start()
    {
        numberOfBalls = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "collectable")
        {
            AddToStack(other.gameObject);
        }
    }
    public void AddToStack(GameObject collectableObject)//add to stack
    {
        Scores.AddToScore(1);
        numberOfBalls++;
        collectableObject.tag = "objectOfStack";
        collectableObject.SetActive(false);
        collectableObject.transform.SetParent(parentOfStack.transform);
    }

    public void RemoveFromStack(GameObject collectableObject)// remove from stack
    {
        numberOfBalls--;
        collectableObject.transform.parent = null;
        collectableObject.SetActive(true);//make active

    }
    public GameObject lastChild()
    {
        return parentOfStack.transform.GetChild(0).gameObject;
    }


}
