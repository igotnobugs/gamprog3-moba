using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public abstract class Skill : ScriptableObject
{
    [SerializeField]
    public string skillName = "Unknown Skill";

    [SerializeField, ShowAssetPreview(64,64)]
    public Sprite skillIcon;

    [SerializeField]
    public SkillType skillType;

    [SerializeField]
    public TargetingType targetingType;

    [SerializeField]
    public AffectType affectType;

    [SerializeField]
    public bool mustBeChannelled = false;

    [SerializeField]
    public DamageType damageType;

    [SerializeField, ResizableTextArea]
    public string description;

    [Header("Skill Leveling")]

    [SerializeField]
    public int skillLevel = 0;

    [SerializeField]
    public int[] levelAvailability = new int[1] { 0 };

    [SerializeField, DisableIf("skillType", SkillType.Auto)]
    public Leveled<float> baseDamage = new Leveled<float>(0);

    [SerializeField]
    public Leveled<float> range = new Leveled<float>(150);

    [SerializeField]
    public Leveled<float> manaConsumption = new Leveled<float>(0);

    [SerializeField]
    public Leveled<float> coolDown = new Leveled<float>(0);

    [SerializeField]
    public Leveled<Buff> buffToApply = new Leveled<Buff>(null);

    protected Unit unitOwner;
    protected List<Buff> buffsApplied = new List<Buff>();

    [HideInInspector]
    public float activeCooldown;

    [HideInInspector]
    public bool isSelected;

    public abstract void OnActivate(Unit target = null);
    public abstract void OnActivate(Vector3 targetPosition);
    public abstract void OnDeactivate();

    public bool isSkillAvailable = false;

    public void Awake()
    {
        activeCooldown = 0.0f;
    }

    public virtual void Initialize(Unit owner)
    {
        unitOwner = owner;
        owner.data.attackRange.normal = range.At(skillLevel);

        if (skillType == SkillType.Passive)
        {
            if (skillLevel > 0)
            {
                OnActivate();
                isSkillAvailable = true;
            }
        }
    }

    public virtual bool Select()
    {
        if (skillLevel <= 0) return false;

        switch (skillType)
        {
            //Check for mana and cooldown
            case (SkillType.Active):
                if (activeCooldown > 0) return false;
                if (!HasEnoughMana()) return false;

                switch (targetingType)
                {
                    case (TargetingType.GroundSphere):
                    case (TargetingType.Ground):                      
                    case (TargetingType.Single):
                        unitOwner.selectedSkill = this;
                        isSelected = true;
                        return true;
                }

                //Just activate if it doesnt need targeting
                OnActivate();
                return true;
        }

        //Auto and Passive? Should have skipped this;
        return true;
    }

    #region Activating
    public virtual bool Activate(Unit target = null)
    {
        if (skillType == SkillType.Active)
        {         
            if (!isSelected) return Select(); // Select first
            if (unitOwner.currentSkillToAttack != this) return Select();

            // Auto and Passive doesnt require cooldown and Mana Check
            // Check if target is valid
            bool canBeActivated = CheckSkillTargetIsValid(target);
            if (!canBeActivated) return false;

            if (activeCooldown > 0) return false;
            if (!HasEnoughMana()) return false;

            unitOwner.ConsumeMana(manaConsumption.At(skillLevel));
            activeCooldown = coolDown.At(skillLevel);
        }


        OnActivate(target);
        return true;
    }

    public virtual bool Activate(Vector3 targetPosition)
    {
        if (skillType == SkillType.Active)
        {
            if (!isSelected) return Select(); // Select first

            // Auto and Passive doesnt require cooldown and Mana Check
            // Check if target is valid
            bool canBeActivated = CheckSkillTargetIsValid(null);
            if (!canBeActivated) return false;

            if (activeCooldown > 0) return false;
            if (!HasEnoughMana()) return false;

            unitOwner.ConsumeMana(manaConsumption.At(skillLevel));
            activeCooldown = coolDown.At(skillLevel);
        }

        if (unitOwner.currentSkillToAttack != this) return false;

        OnActivate(targetPosition);
        return true;
    }
    #endregion

    public virtual void Deactivate()
    {
        OnDeactivate();
    }

    public bool CheckSkillTargetIsValid(Unit target = null)
    {
        //If target is null might be because it is ground targeted or Global
        if (!target)
        {
            switch (targetingType)
            {
                case TargetingType.GroundSphere:
                case TargetingType.Ground:
                case TargetingType.Global:
                    return true;
            }
            return false;
        }
        // Has target
        switch (targetingType)
        {
            case TargetingType.Self:
                return true;

            case TargetingType.Single:
                switch (affectType)
                {
                    case AffectType.All:
                        return true;
                    case AffectType.Enemy:
                        return target.faction != unitOwner.faction;
                    case AffectType.OnlyEnemyHero:
                        return target.faction != unitOwner.faction && target.data.unitType == UnitType.Hero;
                    case AffectType.Ally:
                        return target.faction == unitOwner.faction;
                    case AffectType.OnlyAllyHero:
                        return target.faction == unitOwner.faction && target.data.unitType == UnitType.Hero;
                    case AffectType.HeroOnly:
                        return target.data.unitType == UnitType.Hero;
                    case AffectType.AllExceptSelf:
                        return target != unitOwner;
                    case AffectType.Self:
                        return target == unitOwner;
                }

                Debug.Log("Unknown Affect Type of " + skillName + " used by " + unitOwner.name);
                return false;

            // It is still okay to target ground even with unit I think.
            case TargetingType.Ground:
            case TargetingType.GroundSphere:
                return true;
        }

        Debug.Log("This " + skillName + " used by " + unitOwner.name + " skipped every check?");
        return false;
    }

    public virtual void UpdateSkill(float deltaTime)
    {
        if (skillType == SkillType.Passive) return;

        if (activeCooldown > 0)
        {
            activeCooldown -= deltaTime;
        }
    }

    protected void ApplyBuff(Unit targetUnit)
    {
        if (!buffToApply.At(skillLevel)) return;

        Buff newBuff = Instantiate(buffToApply.At(skillLevel));
        bool isSuccess = newBuff.Initialize(targetUnit, skillLevel, unitOwner);

        if (!isSuccess) return;

        buffsApplied.Add(newBuff);
        newBuff.Activate();

        return;
    }

    public void UpgradeSkill()
    {
        skillLevel++;
        unitOwner.data.skillPoints--;

        if (isSkillAvailable)
        {
            OnUpgradeSkill();
        } else
        {
            if (skillType == SkillType.Passive)
            {
                OnActivate();
            }
            isSkillAvailable = true;
        }
    }

    public virtual void OnUpgradeSkill()
    {

    }

    public bool IsUpgradeable()
    {
        if (skillLevel >= levelAvailability.Length) return false;

        int levelCheck = 0;
        if (levelAvailability.Length < skillLevel)
        {
            levelCheck = levelAvailability.Length - 1;
        } else
        {
            levelCheck = levelAvailability[skillLevel];
        }
        return (unitOwner.data.level >= levelCheck);
    }

    public bool HasEnoughMana()
    {
        return unitOwner.data.mana.current >= manaConsumption.At(skillLevel);
    }

    public Unit GetOwner()
    {
        return unitOwner;
    }

    public void RefreshCooldown()
    {
        activeCooldown = 0;
    }
}

public enum SkillType
{
    Active, Passive, Auto
}

public enum TargetingType
{
    None, Self, Single, GroundSphere, Ground, Global
}

public enum AffectType
{
    Self, Enemy, OnlyEnemyHero, Ally, OnlyAllyHero, HeroOnly, AllExceptSelf, All
}

public enum DamageType
{
    Physical, Magical
}

[System.Serializable]
public class Leveled<T>
{
    public T[] value = new T[1];

    public Leveled(T initialValue)
    {
        value[0] = initialValue ;
    }

    // Already decrements index by one
    public T At(int index)
    {
        return value[Mathf.Clamp(index - 1, 0, value.Length - 1)];
    }
}