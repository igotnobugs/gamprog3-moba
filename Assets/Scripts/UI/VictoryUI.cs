using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class VictoryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI victoryTextTmp;
    [SerializeField]
    private GameObject victoryPanel;

    private void OnEnable()
    {
        Ancient.OnAncientDestroy.AddListener(ShowVictoryPanel);
    }

    private void OnDisable()
    {
        Ancient.OnAncientDestroy.RemoveListener(ShowVictoryPanel);
    }

    public void ShowVictoryPanel(string winner)
    {
        victoryTextTmp.text = winner + " VICTORY!";
        victoryPanel.SetActive(true);
    }
}
