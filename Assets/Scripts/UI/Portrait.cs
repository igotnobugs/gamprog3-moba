using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class Portrait : MonoBehaviour
{
    [SerializeField]
    private RawImage renderUnitPortrait;

    [SerializeField, Required]
    private TextMeshProUGUI unitNameText;

    [SerializeField]
    private TextMeshProUGUI unitAttackText;

    [SerializeField]
    private TextMeshProUGUI unitArmorText;

    [SerializeField]
    private TextMeshProUGUI unitSpeedText;

    [SerializeField]
    private TextMeshProUGUI unitStrText;

    [SerializeField]
    private TextMeshProUGUI unitAgiText;

    [SerializeField]
    private TextMeshProUGUI unitIntText;


    public void SetNameText(string unitName)
    {
        unitNameText.text = unitName;
    }

    public void SetAttackText(float unitAttack, float unitAttackMod)
    {
        if(unitAttackMod > 0)
            unitAttackText.text = $"{Mathf.Floor(unitAttack)}<color=green>+{Mathf.Floor(unitAttackMod)}</color>";
        else if(unitAttackMod < 0)
            unitAttackText.text = $"{Mathf.Floor(unitAttack)}<color=red>{Mathf.Floor(unitAttackMod)}</color>";
        else
            unitAttackText.text = $"{Mathf.Floor(unitAttack)}";
    }

    public void SetArmorText(float unitArmor, float unitArmorMod)
    {
        if (unitArmorMod > 0)
            unitArmorText.text = $"{Mathf.Floor(unitArmor)}<color=green>+{Mathf.Floor(unitArmorMod)}</color>";
        else if(unitArmorMod < 0)
            unitArmorText.text = $"{Mathf.Floor(unitArmor)}<color=red>{Mathf.Floor(unitArmorMod)}</color>";
        else
            unitArmorText.text = $"{Mathf.Floor(unitArmor)}";
    }

    public void SetSpeedText(float unitSpeed, float unitSpeedMod)
    {
        if (unitSpeedMod > 0)
            unitSpeedText.text = $"{Mathf.Floor(unitSpeed)}<color=green>+{Mathf.Floor(unitSpeedMod)}</color>";
        else if(unitSpeedMod < 0)
            unitSpeedText.text = $"{Mathf.Floor(unitSpeed)}<color=red>{Mathf.Floor(unitSpeedMod)}</color>";
        else
            unitSpeedText.text = $"{Mathf.Floor(unitSpeed)}";
    }

    public void SetStrText(float unitStr)
    {
        unitStrText.text = $"{unitStr}";
    }

    public void SetAgiText(float unitAgi)
    {
        unitAgiText.text = $"{unitAgi}";
    }

    public void SetIntText(float unitInt)
    {
        unitIntText.text = $"{unitInt}";
    }
}
