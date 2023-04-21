using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitName = "Unknown";

    [SerializeField, ShowAssetPreview(64, 64), FormerlySerializedAs("heroPortrait")]
    public Sprite unitPortrait;

    [SerializeField]
    public UnitType unitType;

    #region Basic
    [BoxGroup("Base Stats")]
    public Health health;

    [BoxGroup("Base Stats")]
    public bool hasMana;

    [BoxGroup("Base Stats"), ShowIf("hasMana")]
    public Health mana;
    #endregion

    #region Defences
    [BoxGroup("Defences")]
    public Stat armor = new Stat(0.0f);

    [BoxGroup("Defences")]
    public ArmorType armorType;

    [BoxGroup("Defences")]
    public Stat evasion = new Stat(0);

    [BoxGroup("Defences")]
    public Stat physicalResistance = new Stat(0.0f);

    [BoxGroup("Defences"), Range(0f, 1.0f)]
    public float baseMagicResistance = 0.25f;

    [BoxGroup("Defences"), Range(0f, 1.0f)]
    public float totalMagicResistance = 0.25f;
    #endregion

    #region Attacking
    [BoxGroup("Attacking")]
    public Skill autoAttack;

    [BoxGroup("Attacking")]
    public Stat attackDamage = new Stat(10.0f);

    [BoxGroup("Attacking")]
    public Stat attackRange = new Stat(150.0f);

    [BoxGroup("Attacking")]
    public bool isRange = false;

    [BoxGroup("Attacking")]
    public AttackType attackType;

    [BoxGroup("Attacking")]
    public Stat attackSpeed = new Stat(100.0f, true, 20, 700);

    [BoxGroup("Attacking")]
    public float attackRate = 0f;

    [BoxGroup("Attacking")]
    public float baseAttackTime = 1.7f;

    [BoxGroup("Attacking")]
    public float totalAttackTime = 1.7f;

    [BoxGroup("Attacking")]
    public float sightRange = 750;
    #endregion

    #region Skill Set
    [BoxGroup("Skill Set")]
    public Skill qSkill;
    
    [BoxGroup("Skill Set")]
    public Skill wSkill;
    
    [BoxGroup("Skill Set")]
    public Skill eSkill;

    [BoxGroup("Skill Set")]
    public Skill rSkill;
    #endregion

    #region Moving
    [BoxGroup("Moving")]
    [SerializeField, Range(0.01f, 3600.0f), Tooltip("Degrees Per Second")]
    public float turnRateSpeed = 1440.0f;

    [BoxGroup("Moving")]
    [SerializeField, Tooltip("In Dota Values")]
    public Stat moveSpeed = new Stat(350.0f);
    #endregion

    #region Attributes
    [BoxGroup("Attributes"), ShowIf("unitType", UnitType.Hero)]
    public AttributeData attributeData;

    #endregion

    #region Bounty
    [BoxGroup("Bounty")]
    public float goldBounty = 0;

    [BoxGroup("Bounty"), ShowIf("unitType", UnitType.Structure)]
    public float teamGoldBounty = 0;

    [BoxGroup("Bounty")]
    public float experienceBounty = 0;
    #endregion

    #region Non-Hero Growth
    [BoxGroup("Non-Hero Growth"), HideIf("unitType", UnitType.Hero)]
    public float healthGrowth = 0;

    [BoxGroup("Non-Hero Growth"), HideIf("unitType", UnitType.Hero)]
    public float attackGrowth = 0;

    [BoxGroup("Non-Hero Growth"), HideIf("unitType", UnitType.Hero)]
    public float bountyGrowth = 0;

    [BoxGroup("Non-Hero Growth"), HideIf("unitType", UnitType.Hero)]
    public float experienceGrowth = 0;

    public void ScaleCreep()
    {
        if (unitType != UnitType.Creep) return;

        //Debug.Log("Scaling creep!");

        health.Modify(healthGrowth);

        attackDamage.Grow();
        goldBounty += bountyGrowth;
        experienceBounty += experienceGrowth;
    }
    #endregion

    #region Hero Growth

    [BoxGroup("Hero Growth"), ShowIf("unitType", UnitType.Hero)]
    public HeroLevelDatabase levelDatabase;

    [BoxGroup("Hero Growth"), ShowIf("unitType", UnitType.Hero)]
    public HeroLevelData levelData;

    [BoxGroup("Hero Growth"), ShowIf("unitType", UnitType.Hero)]
    public int level = 1;

    [BoxGroup("Hero Growth"), ShowIf("unitType", UnitType.Hero)]
    public float experience;

    [BoxGroup("Hero Growth"), ShowIf("unitType", UnitType.Hero)]
    public int skillPoints = 1;
    #endregion

    #region Hero Gold
    [BoxGroup("Hero Gold"), ShowIf("unitType", UnitType.Hero)]
    public float gold = 600;
    #endregion

    #region Modifiers
    public delegate void ModifyAction(float value);
    public ModifyAction OnModifyAtkSpeed { set; get; }

    [BoxGroup("Modifiers")]
    public List<float> magicResModifiers;

    [BoxGroup("Modifiers")]
    public List<float> magicResReductionModifiers;

    #endregion

    [BoxGroup("Unit State")]
    public List<Buff> buffsApplied = new List<Buff>();

    [BoxGroup("Unit State")]
    public float stunState = 0;

    [BoxGroup("Unit State")]
    public float disarmState = 0;

    [BoxGroup("Unit State")]
    public float physicalImmuneState = 0;

    [BoxGroup("Unit State")]
    public float respawnTime = 0;

    [BoxGroup("Statistics")]
    public int kills = 0;

    [BoxGroup("Statistics")]
    public int deaths = 0;

    public virtual void Initialize(Unit unit)
    {
        //unitOwner = unit;

        if (autoAttack)
        {
            autoAttack = Instantiate(autoAttack);
            autoAttack.Initialize(unit);
        }

        if (qSkill)
        {
            qSkill = Instantiate(qSkill);
            qSkill.Initialize(unit);
        }

        if (wSkill)
        {
            wSkill = Instantiate(wSkill);
            wSkill.Initialize(unit);
        }

        if (eSkill)
        {
            eSkill = Instantiate(eSkill);
            eSkill.Initialize(unit);
        }

        if (rSkill)
        {
            rSkill = Instantiate(rSkill);
            rSkill.Initialize(unit);
        }

        if (unitType != UnitType.Hero)
        {
            skillPoints = 0;
        } else
        {
            SetLevelData(level.ToString());
            attributeData.DeriveStats(this);
        }
    }

    public void UpdateSkills(float timeDelta)
    {
        if (qSkill) qSkill.UpdateSkill(timeDelta);
        if (wSkill) wSkill.UpdateSkill(timeDelta);
        if (eSkill) eSkill.UpdateSkill(timeDelta);
        if (rSkill) rSkill.UpdateSkill(timeDelta);
    }

    public void UpdateUnitStats(float timeDelta)
    {
        UpdateSkills(timeDelta);

        for (int i = 0; i < buffsApplied.Count; i++)
        {
            buffsApplied[i].UpdateBuff(timeDelta);
        }
    }

    #region Experience and Level
    public void GainExp(float value, bool justEnough = false)
    {
        if (level >= 25) return;

        if (justEnough) experience = levelData.requiredExp;
        else experience += value;

        //Allow continuous level up if gained more than required
        while (levelData.WillLevelUp(experience))
        {
            if (level >= 25)
            {
                experience = 0;
            } else
            {
                experience -= levelData.requiredExp;
                LevelUp();
            }         
        }
    }

    private void LevelUp()
    {
        int nextLevel = level + 1;

        if (nextLevel > 25) return;

        level = nextLevel;
        skillPoints++;
        attributeData.GrowAttributes();
        attributeData.DeriveStats(this);

        SetLevelData(level.ToString());
    }

    private void SetLevelData(string newLevelData)
    {
        levelData = levelDatabase.GetData(newLevelData);
    }

    public float GetExpPercentage()
    {
        return experience / levelData.requiredExp;
    }
    #endregion

    public float GetTotalMagicResistance()
    {
        // Calculate resistance modifiers
        float totalResistanceMod = 1 - baseMagicResistance;
        List<float> resModifiers = new List<float>();

        for (int i = 0; i < magicResModifiers.Count; i++)
        {
            resModifiers.Add(1 - magicResModifiers[i]);
        }

        for (int i = 0; i < resModifiers.Count; i++)
        {
            totalResistanceMod *= resModifiers[i];
        }

        // Calculate reduction modifiers
        float totalReductionMod = 1f;
        List<float> reductionModifiers = new List<float>();

        for (int i = 0; i < magicResReductionModifiers.Count; i++)
        {
            reductionModifiers.Add(1 + magicResReductionModifiers[i]);
        }
        for (int i = 0; i < reductionModifiers.Count; i++)
        {
            totalReductionMod *= reductionModifiers[i];
        }

        float finalResistance = 1 - (totalResistanceMod * totalReductionMod);

        return totalMagicResistance = finalResistance;
    }

    public float GetTotalMagicResPercentage()
    {
        float totalMagicResPercentage = totalMagicResistance * 100f;
        return totalMagicResPercentage;
    }

    public void UpdateTotalAttackSpeed()
    {

        attackRate = attackSpeed.GetTotal() / (100 * baseAttackTime);

        totalAttackTime = 1 / attackRate;
    }

    [Button("Refresh")]
    private void UpdateValues()
    {
        if (autoAttack == null)
        {
            attackRange.normal = 0;
        } else
        {
            attackRange.normal = autoAttack.range.At(0);
            attackDamage.normal = autoAttack.baseDamage.At(0);
        }
    }

}

public enum StatType
{
    Health, 
    Armor, 
    MagicRes, 
    AttackDamage, 
    AttackRange, 
    AttackSpeed, 
    MoveSpeed, 
    StunState
}

public enum MainAttribute
{
    Strength, 
    Agility, 
    Intelligence
}

public enum UnitType
{
    Creep, 
    Hero, 
    Structure
}