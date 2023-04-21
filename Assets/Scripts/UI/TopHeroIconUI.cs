using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopHeroIconUI : MonoBehaviour
{
    [SerializeField]
    private Image portrait;

    [SerializeField]
    private GameObject respawnPanel;

    [SerializeField]
    private TextMeshProUGUI respawnText;

    private Unit trackedUnit;
    private UnitData trackedData;

    private void Awake()
    {
        respawnPanel.SetActive(false);
    }

    public void SetHeroIcon(Unit unitToTrack)
    {
        trackedUnit = unitToTrack;
        trackedData = unitToTrack.data;

        portrait.sprite = trackedData.unitPortrait;
    }

    private void Update()
    {
        if (!trackedUnit) return;
        if (!trackedUnit.isAlive)
        {
            respawnPanel.SetActive(true);
            respawnText.text = Mathf.CeilToInt(trackedData.respawnTime).ToString();
        } else
        {
            respawnPanel.SetActive(false);
        }

    }
}
