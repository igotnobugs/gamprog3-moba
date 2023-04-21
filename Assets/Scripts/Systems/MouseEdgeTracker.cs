using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseEdgeTracker : MonoBehaviour
{
    private bool enableTracker = false;
    private float screenEdgeToDetectHeight = 0.0f;
    private float screenEdgeToDetectWidth = 0.0f;
    private bool mouseInEdge = false;
    private InputActions playerAction;

    public UnityEvent<Vector2> OnEdgeEnter = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnEdgeExit = new UnityEvent<Vector2>();

    private void Awake()
    {
        playerAction = new InputActions();
    }

    private void OnEnable()
    {
        playerAction.Enable();
    }

    private void OnDisable()
    {
        playerAction.Disable();
    }

    private void LateUpdate()
    {
        if (!enableTracker) return;

        DetectMouseOnEdge();
    }

    public void SetTrackerEnabled(bool state)
    {
        enableTracker = state;
    }

    public void SetEdgeRatio(float scale)
    {
        screenEdgeToDetectHeight = Screen.height * scale;
        screenEdgeToDetectWidth = Screen.width * scale;
    }

    private void DetectMouseOnEdge()
    {
        Vector2 mousePosition = playerAction.Player.MousePosition.ReadValue<Vector2>();
        if ((mousePosition.x >= Screen.width - screenEdgeToDetectWidth) ||
             (mousePosition.x <= screenEdgeToDetectWidth) ||
             (mousePosition.y >= Screen.height - screenEdgeToDetectHeight) ||
             (mousePosition.y <= screenEdgeToDetectHeight))
        {
            if (!mouseInEdge)
            {
                mouseInEdge = true;
                OnEdgeEnter?.Invoke(mousePosition);
            }
        } else
        {
            if (mouseInEdge)
            {
                mouseInEdge = false;
                OnEdgeExit?.Invoke(mousePosition);
            }
        }
    }
}
