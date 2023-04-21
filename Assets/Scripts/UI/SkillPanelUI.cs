using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Show buttons, manage ui related skills

public class SkillPanelUI : MonoBehaviour
{
    [SerializeField]
    private SkillButton QSkill;

    [SerializeField]
    private SkillButton WSkill;

    [SerializeField]
    private SkillButton ESkill;

    [SerializeField]
    private SkillButton RSkill;

    private void Awake()
    {
        GameManager.gameEvents.OnSelectedUnit.AddListener(DisplaySkill);
    }

    public void DisplaySkill(Unit selectedUnit)
    {
        if (!selectedUnit) return;

        QSkill.SetSkillButton(selectedUnit.data.qSkill, "Q");
        WSkill.SetSkillButton(selectedUnit.data.wSkill, "W");
        ESkill.SetSkillButton(selectedUnit.data.eSkill, "E");
        RSkill.SetSkillButton(selectedUnit.data.rSkill, "R");
    }

    public void PressQSkillButton(Action onSuccess = null)
    {
        QSkill.PressButton(onSuccess);
    }

    public void PressWSkillButton(Action onSuccess = null)
    {
        WSkill.PressButton(onSuccess);
    }

    public void PressESkillButton(Action onSuccess = null)
    {
        ESkill.PressButton(onSuccess);
    }

    public void PressRSkillButton(Action onSuccess = null)
    {
        RSkill.PressButton(onSuccess);
    }

}
