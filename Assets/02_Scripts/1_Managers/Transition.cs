using Twenty.Managers;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private void Awake()
    {
        SceneController.InitiateThisClass();
        //AnalyticsManager.InitiateThisClass();
    }

    private void Start()
    {
        StartCoroutine(SceneController.CurrentLevel());
    }
}
