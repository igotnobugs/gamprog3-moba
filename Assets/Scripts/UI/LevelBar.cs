using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class LevelBar : MonoBehaviour
{
    [SerializeField, Required]
    private Image levelbarFill;

    [SerializeField, Required]
    private TextMeshProUGUI levelbarText;

    [SerializeField, Required]
    private TextMeshProUGUI levelText;

    public void SetLevelFill(float scale)
    {
        levelbarFill.fillAmount = Mathf.Clamp(scale, 0, 1);
    }

    public void SetLevelbarText(float current, float max)
    {
        levelbarText.text = $"{current}/{max}";
    }

    public void SetLevelText(int value)
    {
        levelText.text = $"{value}";
    }
}
