using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField]
    private bool startHidden = true;

    [Header("Radiant Side")]
    [SerializeField]
    private TextMeshProUGUI radiantTeamKillsText;

    [SerializeField]
    public HeroScorePanelUI[] radiantHeroPanels;


    [Header("Dire Side")]
    [SerializeField]
    private TextMeshProUGUI direTeamKillsText;

    [SerializeField]
    public HeroScorePanelUI[] direHeroPanels;


    private void Awake()
    {
        for (int i = 0; i < radiantHeroPanels.Length; i++)
        {
            radiantHeroPanels[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < direHeroPanels.Length; i++)
        {
            direHeroPanels[i].gameObject.SetActive(false);
        }

        GameManager.gameEvents.OnRadiantJoined.AddListener(UpdateRadiantPanel);
        GameManager.gameEvents.OnDireJoined.AddListener(UpdateDirePanel);
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(!startHidden);
    }

    // Update is called once per frame
    private void Update()
    {
        radiantTeamKillsText.text = GameManager.radiantKills.ToString();
        direTeamKillsText.text = GameManager.direKills.ToString();
    }


    private void UpdateRadiantPanel(Hero joinedHero, int order)
    {
        radiantHeroPanels[order - 1].gameObject.SetActive(true);
        radiantHeroPanels[order - 1].SetTrackedData(joinedHero.data);
    }

    private void UpdateDirePanel(Hero joinedHero, int order)
    {
        direHeroPanels[order - 1].gameObject.SetActive(true);
        direHeroPanels[order - 1].SetTrackedData(joinedHero.data);
    }
}
