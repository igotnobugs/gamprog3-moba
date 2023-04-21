using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    [SerializeField]
    private Skill skillShown;

    [Header("Set Up")]
    [SerializeField]
    private Image skillImage;

    [SerializeField]
    private TextMeshProUGUI manaText;

    [SerializeField]
    private Image cooldownAlpha;

    [SerializeField]
    private TextMeshProUGUI cooldownText;

    [SerializeField]
    private GameObject hotKeyPanel;

    [SerializeField]
    private TextMeshProUGUI hotKeyText;

    [SerializeField]
    private Image selectedBorder;

    [SerializeField]
    private Button skillUpButton;

    [SerializeField]
    private Image skillLevelBarPrefab;

    [SerializeField]
    private GameObject skillLevelBarPanel;

    private Button button;
    public Action onButtonClick;
    public Action onActivateAction;

    private List<Image> skillLevelBars = new List<Image>();


    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (!skillShown) return;
        button.interactable = skillShown.GetOwner() == GameManager.Instance.mainHero;
        button.interactable = skillShown.skillLevel >= 1;
    }

    private void OnDisable()
    {
        for (int i = 0; i < skillLevelBars.Count; i++)
        {
            Destroy(skillLevelBars[i].gameObject);   
        }
        skillLevelBars.Clear();
    }

    private void Start()
    {
        selectedBorder.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!skillShown) return;

        manaText.text = $"{skillShown.manaConsumption.At(skillShown.skillLevel)}";

        if (skillShown.activeCooldown > 0)
        {
            cooldownAlpha.gameObject.SetActive(true);
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = Math.Ceiling(skillShown.activeCooldown).ToString();
            cooldownAlpha.fillAmount = skillShown.activeCooldown / skillShown.coolDown.At(skillShown.skillLevel);
        } else {
            cooldownAlpha.gameObject.SetActive(false);
            cooldownText.gameObject.SetActive(false);
        }
                 
        if (skillShown.GetOwner() == GameManager.Instance.mainHero)
        {
            //button.interactable = skillShown.GetOwner().isAlive;

            if (skillShown.HasEnoughMana())
            {
                skillImage.color = Color.white;

            } else
            {
                skillImage.color = Color.red;
            }

            selectedBorder.gameObject.SetActive(skillShown.GetOwner().selectedSkill == skillShown);

            if (skillShown.IsUpgradeable())
            {
                skillUpButton.gameObject.SetActive(skillShown.GetOwner().data.skillPoints > 0);
            } else
            {
                skillUpButton.gameObject.SetActive(false);
            }
        }  else
        {
            skillUpButton.gameObject.SetActive(false);
            button.interactable = false;
        }
    }


    public void SetSkillButton(Skill skillToSet, string hotkey = "")
    {
        if (skillToSet)
        {
            gameObject.SetActive(true);
        } else
        {
            gameObject.SetActive(false);
            return;
        }

        skillShown = skillToSet;
        skillImage.sprite = skillToSet.skillIcon;
        

        if (skillToSet.skillType == SkillType.Active)
        {
            hotKeyPanel.SetActive(true);
            hotKeyText.text = hotkey;
            button.interactable = skillShown.skillLevel >= 1;
        } else
        {
            hotKeyPanel.SetActive(false);
            button.interactable = false;
        }

        CreateLevelBars();
    }

    // Clicking on screen;
    public void PressButton()
    {
        if (skillShown.skillType == SkillType.Passive) return;
        if (!skillShown.GetOwner().isAlive) return;
        skillShown.Activate();
    }

    public void PressButton(Action onSuccess)
    {
        if (skillShown.skillType == SkillType.Passive) return;
        if (!skillShown.GetOwner().isAlive) return;
        skillShown.Activate();
    }

    public void PressSkillUpButton()
    {
        skillShown.UpgradeSkill();
        button.interactable = skillShown.skillLevel >= 1;
        UpdateLevelBars();
    }

    public void UpdateLevelBars()
    {
        if (skillShown.levelAvailability.Length <= 1) return;
        if (skillLevelBars.Count <= 0) return;
        

        Color newColor = skillLevelBars[skillShown.skillLevel - 1].color;
        newColor.a = 1.0f;
        skillLevelBars[skillShown.skillLevel - 1].color = newColor;
    }

    public void CreateLevelBars()
    {
        for (int i = 0; i < skillLevelBars.Count; i++)
        {
            Destroy(skillLevelBars[i].gameObject);
        }
        skillLevelBars.Clear();

        if (skillShown.levelAvailability.Length <= 1) return;
        //Debug.Log(skillShown.skillName);
        for (int i = 0; i < skillShown.levelAvailability.Length; i++)
        {
            Image newBar = Instantiate(skillLevelBarPrefab, skillLevelBarPanel.transform);
            skillLevelBars.Add(newBar);
            Color newColor = newBar.color;
            if (i < skillShown.skillLevel)
            {
                newColor.a = 1.0f;
            } else
            {
                newColor.a = 0.5f;
            }
            newBar.color = newColor;
        }
    }

}
