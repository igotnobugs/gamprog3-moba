using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField, Required]
    private Image healthbarFill;

    [SerializeField, Required]
    private TextMeshProUGUI healthbarText;

    [SerializeField]
    private TextMeshProUGUI healthRegenText;

    public void SetHealthFill(float scale)
    {
        healthbarFill.fillAmount = Mathf.Clamp(scale, 0, 1);
    }

    public void SetHealthText(float current, float max)
    {
        healthbarText.text = $"{current} / {max}";
    }

    public void ShowHealthRegenText(float current, float max, float regenValue)
    {
        healthRegenText.gameObject.SetActive(true);
        /*
        if (regenValue <= 0)
        {
            healthRegenText.gameObject.SetActive(false);
            return;
        }

        if (current < max)
        {
            healthRegenText.gameObject.SetActive(true);
        }
        else
        {
            healthRegenText.gameObject.SetActive(false);
        }*/
    }

    public void SetHealthRegenText(float healthRegen)
    {
        healthRegenText.text = $"+{Mathf.Floor(healthRegen * 10f)/10f}";
    }
}
