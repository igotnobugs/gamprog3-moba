using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[SelectionBase]

public abstract class Unit : Character
{
    public bool isAI = true;
    public bool isAlive = true;
    public bool isInvulnerable = false;

    [Header("Unit")]
    [SerializeField, Tooltip("Stats to use"), FormerlySerializedAs("_stats")]
    private UnitData _data;

    public Camera portraitCamera;
    
    public UnitData data { get { return _data; } }
    public Unit currentTarget { private set; get; }
    public Vector3 currentTargetPosition { private set; get; }
    public Skill currentSkillToAttack { private set; get; }

    public AttackEvents attackEvents { set; get; } = new AttackEvents();
    public StateEvents stateEvents { set; get; } = new StateEvents();

    public Controller controller { private set; get; }

    public Sight unitExpBountySight;   // detects nearby alive Hero allies to give them exp bounty
    private Sight expBountySight;

    public Sight unitGoldBountySight;   // detects nearby alive Hero allies to give them gold bounty
    private Sight goldBountySight;

    //public static List<Hero> spawnedHeroes = new List<Hero>();

    public Skill selectedSkill { get; set; }
    private Action OnAttack { get; set; }
    public bool isAttackingGround { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();

        //Setting Data
        if (_data)
        {
            _data = Instantiate(_data);
            _data.Initialize(this);
        }

        //buffReceiver = GetComponent<BuffReceiver>();
        controller = GetComponent<Controller>();

        SpawnExpBountySight();
        SpawnGoldBountySight();
    }


    public void SetNewData(UnitData newData)
    {
        _data = Instantiate(newData);
        _data.Initialize(this);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        GameManager.Instance.selectedUnit = this;
        portraitCamera.gameObject.SetActive(true);
    }

    public override void OnUnSelected()
    {
        base.OnUnSelected();
        portraitCamera.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        data.UpdateUnitStats(Time.deltaTime);
    }

    public bool Attack(Unit targetUnit, Skill skillToUse = null)
    {
        isAttackingGround = false;
        SetTarget(targetUnit);

        if (skillToUse != null && skillToUse.Select())
        {
            currentSkillToAttack = skillToUse;
        } else
        {
            currentSkillToAttack = _data.autoAttack;
            selectedSkill = null;
        }
        currentSkillToAttack.Initialize(this);

        OnAttack = () =>
        {
            currentSkillToAttack.Activate(targetUnit);
            attackEvents.OnAttackHitFrame?.Invoke();         
        };

        return true;
    }

    public bool Attack(Vector3 targetPosition, Skill skillToUse = null)
    {
        if (skillToUse != null && skillToUse.Select())
        {
            
            if (skillToUse.targetingType != TargetingType.Ground
                && skillToUse.targetingType != TargetingType.GroundSphere) return false;

            currentTargetPosition = targetPosition;
            currentSkillToAttack = skillToUse;
            currentSkillToAttack.Initialize(this);
            isAttackingGround = true;

            OnAttack = () =>
            {
                currentSkillToAttack.Activate(targetPosition);
                attackEvents.OnAttackHitFrame?.Invoke();
                isAttackingGround = false;
            };
            return true;

        }

        currentSkillToAttack = _data.autoAttack;
        selectedSkill = null;
        currentSkillToAttack.Initialize(this);
        return true;
    }

    public void SetSkillToAttack(Skill newSkill)
    {
        currentSkillToAttack = newSkill;
    }
    
    private void OnUnitAttack()
    {      
        if (currentSkillToAttack)
        {
            OnAttack?.Invoke();

            if (currentSkillToAttack != data.autoAttack)
            {
                currentSkillToAttack = data.autoAttack;
                currentSkillToAttack.Initialize(this);
                currentTarget = null;
                //Otherwise unit will not chase after using a skill
                //controller.AttackTarget(currentTarget);
            }
            selectedSkill = null;
            
        } else
        {
            Debug.Log(gameObject.name + "has no skill/attack to use");
        }
    }

    #region Mana Consumption and Restoration
    public bool HasEnoughMana(float amount)
    {
        if (data.mana.current >= amount)
            return true;
        else
        {
            Debug.LogError("Not enough mana!");
            return false;
        }
    }

    public bool HasEnoughMana()
    {
        if(currentSkillToAttack)
        {
            if (currentSkillToAttack.HasEnoughMana())
                return true;
            else
            {
                Debug.LogError("Not enough mana!");
                return false;
            }
        }
        return false;
    }

    public bool ConsumeMana(float amount)
    {
        if (HasEnoughMana(amount))
        {
            data.mana.Reduce(amount);
            stateEvents.OnConsumeMana?.Invoke(amount);
            return true;
        }
        return false;        
    }

    public void RestoreMana(float amount)
    {
        if (!isAlive) return;

        data.mana.Restore(amount);
    }
    #endregion

    #region Targeting
    public void SetTarget(Unit newTarget)
    {
        currentTarget = newTarget;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentTargetPosition = targetPosition;
    }

    public bool IsTargetInRange()
    {
        // Non unit target skills
        if (currentSkillToAttack)
        {
            if (currentSkillToAttack.targetingType == TargetingType.Ground
               || currentSkillToAttack.targetingType == TargetingType.GroundSphere)
            {
                return Vector3.Distance(currentTargetPosition, transform.position)
                    <= _data.attackRange.GetTotal().ToUnitValue();
            }
        }

        return currentTarget
             && ((Vector3.Distance(currentTarget.transform.position, transform.position)
             - currentTarget.width.ToUnitValue())
             <= _data.attackRange.GetTotal().ToUnitValue());
    }
    #endregion

    #region Damage, Healing and Death
    public void TakeDamage(float amount, DamageType damageType, Unit instigator = null, bool byPassEvasion = false)
    {

        if (UnityEngine.Random.Range(0, 99) < data.evasion.GetTotal() && !byPassEvasion)
        {
            stateEvents.OnEvade?.Invoke();
            return;
        }

        float damageModifier = _data.armorType.GetDamageModifier(instigator);  // added calculate dmg modifier (atk type v. armor type)
        float calculatedDamage = CalculateDamage(amount, damageType) * damageModifier;      // added calculate dmg 
        bool isDead = data.health.Reduce(calculatedDamage);
        
        stateEvents.OnTakeDamage?.Invoke(calculatedDamage);

        if (isDead)
        {
            isAlive = false;
            data.deaths++;
            if(instigator != null)
            {
                //Debug.Log("Last hitter = " + instigator.name);
                instigator.data.kills++;
                SplitExp();
                instigator.GainGold(this);
                
            }

            stateEvents.OnUnitDeath?.Invoke(this, instigator);
            currentTarget = null;
            OnUnitDeath();
        }
    }

    public float CalculateDamage(float amount, DamageType damageType)
    {
        if(damageType == DamageType.Physical)
        {
            //damageMultiplier = 1 - ((0.052 × armor) ÷ (0.9 + 0.048 × | armor |)); armor = total armor of target
            float damageMultiplier = (float)
                (1 - ((0.052 * data.armor.GetTotal()) / (0.9 + 0.048 * Mathf.Abs(_data.armor.GetTotal()))));
            return amount * damageMultiplier;
        }
        else
        {
            float damageModFromMagicRes = 1 - _data.GetTotalMagicResistance();
            return amount * damageModFromMagicRes;
        }
        
    }

    public void Heal(float amount, Unit instigator = null)
    {
        if (!isAlive) return; // Cant heal dead people

        data.health.Restore(amount);
    }

    protected virtual void OnUnitDeath()
    {
        Destroy(_data);
        OnUnSelected();

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    #endregion

    #region Buffing
    public void Stun(float duration)
    {
        if (data.stunState < duration)
        {
            data.stunState = duration;
            stateEvents.OnStunned?.Invoke(duration);
        }     
    }

    public void AddBuff(Buff buffToAdd)
    {
        //Debug.Log("Buff added");
        data.buffsApplied.Add(buffToAdd);
        stateEvents.OnBuffApplied?.Invoke(buffToAdd);
    }

    public void RemoveBuff(Buff buffToRemove)
    {

        bool isSuccess = data.buffsApplied.Remove(buffToRemove);
        if (isSuccess)
        {
            stateEvents.OnBuffRemoved?.Invoke(buffToRemove);
        }
    }
    #endregion

    #region Exp, Gold and Bounty
    private void SpawnExpBountySight()
    {
        expBountySight = Instantiate(unitExpBountySight.GetComponent<Sight>(), this.transform.position, this.transform.rotation);
        expBountySight.unitOwner = this;
        expBountySight.GetStatsFromOwner = false;
        expBountySight.sightRange = Constants.expBountyRange;
        expBountySight.sightCollider.radius = expBountySight.sightRange.ToUnitValue();

        expBountySight.gameObject.transform.parent = this.transform;
    }

    private void SpawnGoldBountySight()
    {
        goldBountySight = Instantiate(unitGoldBountySight.GetComponent<Sight>(), this.transform.position, this.transform.rotation);
        goldBountySight.unitOwner = this;
        goldBountySight.GetStatsFromOwner = false;
        goldBountySight.sightRange = Constants.goldBountyRange;
        goldBountySight.sightCollider.radius = goldBountySight.sightRange.ToUnitValue();

        goldBountySight.gameObject.transform.parent = this.transform;
    }

    private void SplitExp()
    {
        // Gain evenly split exp for all enemy heroes within radius
        for (int i = 0; i < expBountySight.ValidEnemyTargets.Count; i++)
        {
            if (expBountySight.ValidEnemyTargets[i] != null)
            {
                // Add exp to heroes
                expBountySight.ValidEnemyTargets[i].GetComponent<Unit>().GainExp(this, CalculateHeroCount(expBountySight.ValidEnemyTargets));
            }
        }
    }

    private void SplitGold(List<GameObject> allies, float goldBounty)
    {
        for (int i = 0; i < allies.Count; i++)
        {
            if (allies[i] != null)
            {
                // Add gold to heroes
                allies[i].GetComponent<Unit>().IncreaseGold(goldBounty);
            }
        }
    }

    private void SplitGold(List<Hero> heroes, float goldBounty)
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            heroes[i].IncreaseGold(goldBounty);
        }
    }


    private int CalculateHeroCount(List<GameObject> instigators)
    {
        int heroCount = 0;

        for (int i = 0; i < instigators.Count; i++)
        {
            if (instigators[i] != null && instigators[i].TryGetComponent(out Unit instigator))
            {
                if (instigator.isAlive && instigator.data.unitType == UnitType.Hero)
                    heroCount++;
            }
        }
        return heroCount;
    }

    protected virtual void GainExp(Unit victim, int heroCount)
    {
        if (heroCount == 0) return;
        if (!isAlive) return;
        if (UnitType.Hero != data.unitType) return;
        if (this == null) return;

        float expBounty;

        // if victim killed was a hero
        if (victim.data.unitType == UnitType.Hero)
            expBounty = victim.data.levelData.expKillBounty / heroCount;
        else
            expBounty = victim.data.experienceBounty / heroCount;

        data.GainExp(expBounty);
    }

    protected virtual void GainGold(Unit victim)
    {
        // to gain gold...
        if (!isAlive) return;           // unit must be alive
        if (UnitType.Hero != data.unitType) return;     // unit must be a Hero
        if (!this) return;       // this must not be null

        float goldBounty;

        // if victim killed was a hero
        if (victim.data.unitType == UnitType.Hero)
        {
            goldBounty = victim.data.levelData.goldLastHitBounty;

            //int alliedHerosNearby = goldBountySight.ValidAllyTargets.Count;
            //float splitGold = victim.stats.levelData.goldAssistBounty / alliedHerosNearby;

            SplitGold(goldBountySight.ValidAllyTargets, victim.data.levelData.goldAssistBounty);
        }
        else
        {
            goldBounty = victim.data.goldBounty;

            if (victim.data.unitType == UnitType.Structure)
            {
                
                if (victim.faction == Faction.Radiant)
                {
                    SplitGold(GameManager.direHeroes, victim.data.teamGoldBounty);
                } else
                {
                    SplitGold(GameManager.radiantHeroes, victim.data.teamGoldBounty);
                }


            }
        }

        IncreaseGold(goldBounty);
    }

    private void IncreaseGold(float value)
    {
        _data.gold += value;
    }
    #endregion

    #region From Animation Events
    private void OnAnimationAttackStart() => attackEvents.OnAttackStart?.Invoke();

    private void OnAnimationAttackFrame() => OnUnitAttack();

    private void OnAnimationAttackEnd() => attackEvents.OnAttackEnd?.Invoke();
    #endregion
}

[System.Serializable]
public class AttackEvents
{
    public UnityEvent OnAttackStart = new UnityEvent();
    public UnityEvent OnAttackHitFrame = new UnityEvent();
    public UnityEvent OnAttackEnd = new UnityEvent();
}


[System.Serializable]
public class TargetEvents
{
    public UnityEvent<Unit, bool> OnTargetSet = new UnityEvent<Unit, bool>();
    public UnityEvent<Unit, Unit> OnTargetDeath = new UnityEvent<Unit, Unit>();
}


[System.Serializable]
public class StateEvents
{
    public UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public UnityEvent<Unit, Unit> OnUnitDeath = new UnityEvent<Unit, Unit>();
    public UnityEvent OnUnitRespawn = new UnityEvent();
    public UnityEvent OnGainExp = new UnityEvent();
    public UnityEvent OnEvade = new UnityEvent();
    public UnityEvent<float> OnStunned = new UnityEvent<float>();

    public UnityEvent<float> OnConsumeMana = new UnityEvent<float>();

    public UnityEvent<Buff> OnBuffApplied = new UnityEvent<Buff>();
    public UnityEvent<Buff> OnBuffRemoved = new UnityEvent<Buff>();
}