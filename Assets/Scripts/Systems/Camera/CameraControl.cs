using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

//[RequireComponent(typeof(MouseTracker))]
public class CameraControl : MonoBehaviour
{
    [SerializeField, Tooltip("The object to follow when constraint is activated.")]
    private GameObject trackedObject;

    [Header("Camera Settings")]
    [SerializeField, Min(0), Tooltip("How far the camera can move from the center.")] 
    private Vector2 cameraBounds = new Vector2(200.0f, 200.0f);

    [SerializeField, Tooltip("Moving the cursor to the edge will move the camera.")] 
    private bool enableMousePan = true;

    [SerializeField, Tooltip("Ignore mouse panning while moving the camera with the keyboard.")] 
    private bool prioritizeKeyboard = true;

    [SerializeField, Range(1.0f, 40.0f), Tooltip("Speed of camera panning.")] 
    private float cameraSpeed = 20.0f;

    [SerializeField, Range(0.0f, 0.4f), Tooltip("Edge ratio of the screen to begin panning with the mouse.")]
    private float mouseEdgeRatio = 0.05f;

    [SerializeField, Range(0.0f, 1.0f), Tooltip("Time before mouse panning starts.")] 
    private float timeBeforeMousePan = 0.25f;

    [SerializeField, Tooltip("Enable camera to follow the height of the terrain.")]
    private bool maintainTerrainHeight = true;

    [SerializeField, Tooltip("Enable camera to follow hero.")]
    private bool followHero = false;

    [Header("Set Up")]
    [SerializeField]
    private CursorSet cursorSet;

    private bool isMovingWithKeyboard = false;
    private bool startMousePan = false;
    private InputActions playerAction;
    private Coroutine coroutine_MousePanCountdown;
    private Vector2 midScreen = new Vector2(Screen.width / 2, Screen.height / 2);
    private PositionConstraint posConstraint;
    private int numberOfTaps = 0;
    private MouseEdgeTracker mouseEdgeTracker;

    private void Awake() 
    {
        playerAction = new InputActions();
        posConstraint = GetComponent<PositionConstraint>();
        mouseEdgeTracker = GetComponent<MouseEdgeTracker>();
    }

    private void OnEnable() 
    {
        playerAction.Player.ArrowKeys.performed += _ => ArrowKeysPressed(true);
        playerAction.Player.ArrowKeys.canceled += _ => ArrowKeysPressed(false);
        playerAction.Player.SelectHero.performed += _ => SelectHero_performed();

        mouseEdgeTracker.OnEdgeEnter.AddListener(MouseTracker_OnEdgeEnter);
        mouseEdgeTracker.OnEdgeExit.AddListener(MouseTracker_OnEdgeExit);

        playerAction.Enable();
    }

    private void OnDisable() 
    {
        playerAction.Player.ArrowKeys.performed -= _ => ArrowKeysPressed(true);
        playerAction.Player.ArrowKeys.canceled -= _ => ArrowKeysPressed(false);
        playerAction.Player.SelectHero.performed -= _ => SelectHero_performed();

        mouseEdgeTracker.OnEdgeEnter.RemoveListener(MouseTracker_OnEdgeEnter);
        mouseEdgeTracker.OnEdgeExit.RemoveListener(MouseTracker_OnEdgeExit);

        playerAction.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {     
        mouseEdgeTracker.SetTrackerEnabled(true);
        mouseEdgeTracker.SetEdgeRatio(mouseEdgeRatio);
        SetTrackedObject(trackedObject);
    }

    // Update is called once per frame
    private void Update()
    {
        KeyboardPanning();
 
        MousePanning();        
    }

    private void LateUpdate()
    {
        MaintainHeight();
    }

    public void GoToPosition(float x, float y)
    {
        posConstraint.constraintActive = false;
        transform.position = new Vector3(x, transform.position.y, y);
    }

    public void SetTrackedObject(GameObject newTarget, bool setActive = true)
    {
        ConstraintSource constraintSource = new ConstraintSource
        {
            sourceTransform = newTarget.transform,
            weight = 1
        };
        posConstraint.SetSource(0, constraintSource);
        posConstraint.constraintActive = followHero;
    }


    private void MaintainHeight()
    {
        if (!maintainTerrainHeight) return;

        Vector3 skyAbove = transform.position + (Vector3.up * 2000); 
        if (Physics.Raycast(skyAbove, Vector3.down, out RaycastHit hit, 90000.0f))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                transform.position = new Vector3(
                    transform.position.x,
                    hit.point.y,
                    transform.position.z
                    );
            }
        }
    }

    private void SelectHero_performed()
    {
        numberOfTaps++;
        if (numberOfTaps >= 2)
        {
            numberOfTaps = 0;
            transform.position = GameManager.Instance.mainHero.transform.position;
            posConstraint.constraintActive = followHero;
        }
    }

    private void MouseTracker_OnEdgeEnter(Vector2 mousePosition)
    {
        if (!enableMousePan) return;

        cursorSet.onEdge.SetCursorTexture();
        this.RestartCoroutine(MouseOnEdgeCountdown(), ref coroutine_MousePanCountdown);
    }

    private void MouseTracker_OnEdgeExit(Vector2 mousePosition)
    {
        if (!enableMousePan) return;

        startMousePan = false;
        cursorSet.normal.SetCursorTexture();
        this.TryStopCoroutine(ref coroutine_MousePanCountdown);
    }

    private void ArrowKeysPressed(bool state) 
    {
        isMovingWithKeyboard = state;
        posConstraint.constraintActive = false;
    }

    private void KeyboardPanning() 
    {
        if (!isMovingWithKeyboard) return;

        Vector2 moveCamera2 = playerAction.Player.ArrowKeys.ReadValue<Vector2>();
        moveCamera2 = moveCamera2 * Time.deltaTime * cameraSpeed;

        if (IsReachingBounds(moveCamera2)) return;

        transform.Translate(moveCamera2.x, 0, moveCamera2.y, Space.World);   
    }

    private IEnumerator MouseOnEdgeCountdown()
    {
        yield return new WaitForSeconds(timeBeforeMousePan);   

        startMousePan = true;
        posConstraint.constraintActive = false;

        cursorSet.onEdgeMoving.SetCursorTexture();

        yield return null;
    }

    private void MousePanning() 
    {
        if (!enableMousePan || !startMousePan 
            || (isMovingWithKeyboard && prioritizeKeyboard)) return;

        Vector2 mousePosition = playerAction.Player.MousePosition.ReadValue<Vector2>();       
        Vector3 mouseDirection = new Vector3(
            mousePosition.x - midScreen.x, 0, mousePosition.y - midScreen.y
            );   

        Vector3 moveCamera3 = mouseDirection.normalized * Time.deltaTime * cameraSpeed;

        if (IsReachingBounds(moveCamera3)) return;

        transform.Translate(moveCamera3.x, 0, moveCamera3.z, Space.World);
    }

    private bool IsReachingBounds(Vector2 deltaPosition) 
    {
        return (transform.position.x + deltaPosition.x > cameraBounds.x 
            || transform.position.x + deltaPosition.x < -cameraBounds.x 
            || transform.position.z + deltaPosition.y > cameraBounds.y 
            || transform.position.z + deltaPosition.y < -cameraBounds.y);
    }

    private bool IsReachingBounds(Vector3 deltaPosition) 
    {
        return (transform.position.x + deltaPosition.x > cameraBounds.x 
            || transform.position.x + deltaPosition.x < -cameraBounds.x 
            || transform.position.z + deltaPosition.z > cameraBounds.y 
            || transform.position.z + deltaPosition.z < -cameraBounds.y);
    }
}
