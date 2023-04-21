using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Minimap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, Tooltip("The camera to use to capture the map and render it in the UI")]
    private Camera minimapCamera;

    [SerializeField, Tooltip("Camera to move when clicking on the Minimap")] 
    private CameraControl activeCamera;

    [SerializeField, Tooltip("The size of the total map to cover")]
    private Vector2 mapSize = new Vector2(150, 150);
    
    private InputActions playerAction;
    private RectTransform minimapButtonRect;
    private bool isHoldingOverMap = false;

    public static UnityEvent<Vector3, bool> OnMinimapLeftClick = new UnityEvent<Vector3, bool>();
    public static UnityEvent<Vector3, bool> OnMinimapRightClick = new UnityEvent<Vector3, bool>();

    private void Awake()
    {
        minimapButtonRect = GetComponent<RectTransform>();
        playerAction = new InputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAction.Enable();

        if (minimapCamera != null)
        {
            minimapCamera.orthographicSize = mapSize.x;
        } else
        {
            Debug.LogError("Minimap camera is not set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingOverMap)
        {
            Vector2 mousePosition = playerAction.Player.MousePosition.ReadValue<Vector2>();
            Vector2 worldPosition = ConvertToWorldPosition(mousePosition);

            activeCamera.GoToPosition(worldPosition.x, worldPosition.y);
        }
    }

    private Vector2 ConvertToWorldPosition(Vector2 mousePosition)
    {
        Vector2 relativePos = minimapButtonRect.transform.InverseTransformPoint(mousePosition);
        Rect rect = minimapButtonRect.rect;

        Vector2 relativePositionNorm = new Vector2(relativePos.x / rect.x, relativePos.y / rect.y);
        relativePositionNorm *= -1;

        return relativePositionNorm * mapSize;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("clicked");

        Vector2 mousePosition = playerAction.Player.MousePosition.ReadValue<Vector2>();
        Vector2 worldPosition = ConvertToWorldPosition(mousePosition);

        //Move y to z
        Vector3 convVector3 = new Vector3(worldPosition.x, 0, worldPosition.y);

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isHoldingOverMap = true;

            if (activeCamera != null)
            {
                activeCamera.GoToPosition(worldPosition.x, worldPosition.y);
            } else
            {
                Debug.LogWarning("Active Camera not assigned.");
            }
            OnMinimapLeftClick?.Invoke(convVector3, true);

        } else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnMinimapRightClick?.Invoke(convVector3, true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHoldingOverMap = false;
    }
}
