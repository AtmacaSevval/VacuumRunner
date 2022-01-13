using UnityEngine;
using Twenty.Managers;
public class InputManager : MonoBehaviour
{
    private float currentMousePos;
    private float previousMousePos;
    public static float DeltaX { get; set; }
    private float ScreenWidth;

    private bool swipe;
    void Start()
    {
        ScreenWidth = Screen.width;
    }

    private void OnGUI()
    {
        if (!swipe && DeltaX != 0)
        {
            GameManager.GameStart();
            swipe = true;
        }
    }
    void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = currentMousePos = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            previousMousePos = currentMousePos;

            currentMousePos = Input.mousePosition.x;

            DeltaX = (currentMousePos - previousMousePos) / ScreenWidth;
        }
        if (Input.GetMouseButtonUp(0))
        {
            DeltaX = 0;
        }
    }
}
