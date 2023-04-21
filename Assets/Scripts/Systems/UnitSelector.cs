using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class UnitSelector : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerSelection;

    private InputActions playerAction;

    private void Awake()
    {
        playerAction = new InputActions();
    }

    private void OnEnable()
    {
        playerAction.Player.LeftMouseClick.performed += _ => LeftMouseClick_performed();
        playerAction.Player.SelectHero.performed += _ => SelectHero_performed();
    }


    private void OnDisable()
    {
        playerAction.Player.LeftMouseClick.performed -= _ => LeftMouseClick_performed();
        playerAction.Player.SelectHero.performed -= _ => SelectHero_performed();
    }

    private void Start()
    {
        playerAction.Enable();
        if (!GameManager.Instance)
        {
            Debug.Log("No Gamemanager exists");
            enabled = false;
        }
    }

    private void SelectHero_performed()
    {
        GameManager.Instance.selectedUnit.OnUnSelected();
        GameManager.Instance.mainHero.OnSelected();
    }


    private void LeftMouseClick_performed()
    {
        Vector2 mpos = playerAction.Player.MousePosition.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mpos);
        if (Physics.Raycast(ray, out RaycastHit hit, 90000.0f, layerSelection))
        {
            if (hit.collider.TryGetComponent(out ISelectable selectable))
            {
                if (GameManager.Instance.selectedUnit)
                    GameManager.Instance.selectedUnit.OnUnSelected();

                selectable.OnSelected();
            }
        } 
    }
}
