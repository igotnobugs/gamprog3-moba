using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffUI : MonoBehaviour
{
    [Header("Set Up")]
    [SerializeField]
    private Image buffDurationBorder;

    [SerializeField]
    private Image buffIconUI;

    [SerializeField]
    private TextMeshProUGUI buffStackText;

    private Buff buffData;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!buffData) return;

        if (buffData.stacks > 1)
        {
            buffStackText.gameObject.SetActive(true);
            buffStackText.text = buffData.stacks.ToString();
        } else
        {
            buffStackText.gameObject.SetActive(false);
        }

        if (buffData.hasDuration)
        {
            buffDurationBorder.fillAmount = buffData.durationCount / buffData.duration.At(buffData.buffLevel);
        }
    }

    public void Initialize(Buff buffToDisplay)
    {
        buffData = buffToDisplay;

        if (buffToDisplay.HUDIcon)
        {
            buffIconUI.sprite = buffToDisplay.HUDIcon;
        }

        if (buffToDisplay.stacks > 1)
        {
            buffStackText.gameObject.SetActive(true);
            buffStackText.text = buffToDisplay.stacks.ToString();
        } else
        {
            buffStackText.gameObject.SetActive(false);
        }

        if (buffToDisplay.isDetrimental)
        {
            buffDurationBorder.color = Color.red;
        } else
        {
            buffDurationBorder.color = Color.green;
        }
    }
}
