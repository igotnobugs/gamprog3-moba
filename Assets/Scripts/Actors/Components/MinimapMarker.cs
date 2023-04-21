using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMarker : MonoBehaviour
{
    [SerializeField]
    private float markerScale = 10.0f;

    [SerializeField]
    private float yOffSet = 40.0f;

    private Unit owner;
    private SpriteRenderer minimapIcon;
    

    private void Awake()
    {
        owner = GetComponentInParent<Unit>();
        minimapIcon = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        transform.position += Vector3.up * yOffSet;
        transform.localScale = Vector3.one * markerScale;

        SetMinimapIconColor();
    }

    private void SetMinimapIconColor()
    {
        if (owner.faction == Faction.Dire)
            minimapIcon.color = Color.red;
        else
            minimapIcon.color = Color.blue;

    }
}
