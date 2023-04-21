using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroScorePanelUI : MonoBehaviour
{
    private UnitData trackedData;

    [SerializeField]
    private Image portrait;

    [SerializeField]
    private TextMeshProUGUI unitDescription;

    [SerializeField]
    private TextMeshProUGUI killsText;

    [SerializeField]
    private TextMeshProUGUI deathsText;

    [SerializeField]
    private TextMeshProUGUI goldText;

    // Update is called once per frame
    private void Update()
    {
        if (!trackedData) return;

        unitDescription.text = $"Lvl {trackedData.level} \n{trackedData.unitName}";
        killsText.text = trackedData.kills.ToString();
        deathsText.text = trackedData.deaths.ToString();
        goldText.text = trackedData.gold.ToString();
    }

    public void SetTrackedData(UnitData unitdata)
    {
        trackedData = unitdata;
        portrait.sprite = trackedData.unitPortrait;
    }
}
