using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI unitGoldText;

    public void Update()
    {
        if (!GameManager.Instance.selectedUnit) return;

        SetGoldText(GameManager.Instance.selectedUnit.data.gold);
    }

    public void SetGoldText(float value)
    {
        unitGoldText.text = $"{value}";
    }
}
