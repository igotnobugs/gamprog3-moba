using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUIPanel : MonoBehaviour
{

    [Header("Set Up")]
    [SerializeField]
    private BuffUI buffUIPrefab;

    [SerializeField]
    private Transform buffUIContainer;
    
    private Dictionary<Buff, BuffUI> buffUIs = new Dictionary<Buff, BuffUI>();

    private void Awake()
    {
        GameManager.gameEvents.OnSelectedUnit.AddListener(RegisterBuffEvents);
        
    }

    private void RegisterBuffEvents(Unit selectedUnit)
    {
        //Remove all Buff UI
        if (buffUIs.Count > 0)
        {
            foreach (var buffUI in buffUIs)
            {
                Destroy(buffUI.Value.gameObject);
            }
            buffUIs.Clear();
        }

        selectedUnit.stateEvents.OnBuffApplied.AddListener(DisplayBuff);
        selectedUnit.stateEvents.OnBuffRemoved.AddListener(RemoveBuff);

        SetBuffs(selectedUnit);
    }

    private void DisplayBuff(Buff newBuff)
    {
        if (buffUIs.ContainsKey(newBuff)) return;

        BuffUI newBuffUI = Instantiate(buffUIPrefab, buffUIContainer);
        newBuffUI.Initialize(newBuff);
        buffUIs.Add(newBuff, newBuffUI);
    }


    private void RemoveBuff(Buff buffToRemove)
    {
        //Debug.Log(buffToRemove);
        if (buffUIs.TryGetValue(buffToRemove, out BuffUI buffUI))
        {
            Destroy(buffUI.gameObject);
            buffUIs.Remove(buffToRemove);
            //Debug.Log("Removing ui buff");
        } else
        {
            //Debug.Log("Fail to remove ui buff");
        }
    }

    private void SetBuffs(Unit unit)
    {
        if (unit.data.buffsApplied.Count <= 0) return;

        for (int i = 0; i < unit.data.buffsApplied.Count; i++)
        {
            BuffUI newBuffUI = Instantiate(buffUIPrefab, buffUIContainer);
            newBuffUI.Initialize(unit.data.buffsApplied[i]);
            buffUIs.Add(unit.data.buffsApplied[i], newBuffUI);
        }
    }
}
