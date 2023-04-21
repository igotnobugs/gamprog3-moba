using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class ZoomControl : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector cameraZoomDirector;
    [SerializeField, Range(0.01f, 0.1f)]
    private float zoomSpeed = 0.025f;
    

    private InputActions playerAction;

    private void Awake()
    {
        playerAction = new InputActions();
    }

    private void OnEnable()
    {
        playerAction.Player.Zoom.performed += Zoom_performed;
    }

    private void OnDisable()
    {
        playerAction.Player.Zoom.performed -= Zoom_performed;
    }

    private void Start()
    {
        playerAction.Enable();
    }

    private void Zoom_performed(InputAction.CallbackContext obj)
    {
        Vector2 mouseScroll = playerAction.Player.Zoom.ReadValue<Vector2>();

        if (cameraZoomDirector.time <= 1.0f && cameraZoomDirector.time >= 0)
        {
            cameraZoomDirector.time += mouseScroll.y * zoomSpeed;
            if (cameraZoomDirector.time < 0) cameraZoomDirector.time = 0;
            else if (cameraZoomDirector.time > 1) cameraZoomDirector.time = 1;
            cameraZoomDirector.Evaluate();
        }
    }
}
