using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class Manabar : MonoBehaviour
{
    [SerializeField, Required]
    private Image manabarFill;

    [SerializeField, Required]
    private TextMeshProUGUI manabarText;

    [SerializeField]
    private TextMeshProUGUI manaRegenText;

    public void SetManaFill(float scale)
    {
        manabarFill.fillAmount = Mathf.Clamp(scale, 0, 1);
    }

    public void SetManaText(float current, float max)
    {
        manabarText.text = $"{current} / {max}";
    }

    public void ShowManaRegenText(float current, float max, float regenValue)
    {
        if (regenValue <= 0)
        {
            manaRegenText.gameObject.SetActive(false);
            return;
        }
            
        if (current < max)
        {
            manaRegenText.gameObject.SetActive(true);
        }
        else
        {
            manaRegenText.gameObject.SetActive(false);
        }
    }

    public void SetManaRegenText(float manaRegen)
    {
        manaRegenText.text = $"+{Mathf.Floor(manaRegen * 10f) / 10f}";
    }
}
