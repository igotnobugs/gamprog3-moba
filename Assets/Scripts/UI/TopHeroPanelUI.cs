using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHeroPanelUI : MonoBehaviour
{

    [SerializeField]
    private TopHeroIconUI topHeroIconUIPrefab;

    [SerializeField]
    private Transform radiantSidePanel;

    [SerializeField]
    private Transform direSidePanel;

    private void Awake()
    {
        GameManager.gameEvents.OnHeroJoined.AddListener(AddHeroToTopPanel);
    }

    private void AddHeroToTopPanel(Hero addedHero)
    {
        TopHeroIconUI newHeroIconUI = Instantiate(topHeroIconUIPrefab, transform);
        if (addedHero.faction == Faction.Radiant)
        {
            newHeroIconUI.transform.SetParent(radiantSidePanel.transform);
        } else
        {
            newHeroIconUI.transform.SetParent(direSidePanel.transform);
        }
        newHeroIconUI.SetHeroIcon(addedHero);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
