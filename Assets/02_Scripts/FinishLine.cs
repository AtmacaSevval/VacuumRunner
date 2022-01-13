using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twenty.Managers;
public class FinishLine : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.GameSuccess();
        }
    }
}
