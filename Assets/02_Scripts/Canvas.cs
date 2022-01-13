using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Twenty.Managers;
public class Canvas : MonoBehaviour
{
    public GameObject failCanvas;
    public TMP_Text startText;
    public GameObject finishCanvas;


    private void OnEnable()
    {
        GameManager.onLevelStart += StartCanvas;
        GameManager.onLevelFail += FailCanvas;
        GameManager.onLevelSuccess += SuccessCanvas; 
    }

    private void OnDisable()
    {
        GameManager.onLevelStart -= StartCanvas;
        GameManager.onLevelFail -= FailCanvas;
        GameManager.onLevelSuccess -= SuccessCanvas;
    }

    public void StartCanvas()
    {
        startText.enabled = false;
    }

    public void FailCanvas()
    {
        failCanvas.SetActive(true);
    }
    public void SuccessCanvas()
    {
        finishCanvas.SetActive(true);
    }
    
}
