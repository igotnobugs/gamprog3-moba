using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Actor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectable
{
    [Header("Actor")]
    [SerializeField, Tooltip("Faction or team of the entity")]
    private Faction _faction;

    [SerializeField, Min(0.01f), Tooltip("Width of the object for targeting. In Dota Values.")]
    private float _width = 50.0f;

    public Faction faction { get { return _faction; } set { _faction = value; } }
    public float width { get { return _width; } }

    private Outline outliner;

    protected virtual void Awake()
    {
        outliner = GetComponentInChildren<Outline>();
        if (outliner == null)
        {
            //Debug.Log("No Outline Component placed in Mesh.", gameObject);
            return;
        }
        outliner.OutlineColor =  new Color(0 + (int)faction, 1 - (int)faction, 0, 1);
    }

    #region Overrides
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        outliner.OutlineMode = Outline.Mode.OutlineAll;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        outliner.OutlineMode = Outline.Mode.OutlineHidden;
    }

    public virtual void OnSelected()
    {

    }

    public virtual void OnUnSelected()
    {

    }
    #endregion

}

public enum Faction
{
    Radiant, Dire
}

