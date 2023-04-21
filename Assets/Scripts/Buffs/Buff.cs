using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class Buff : ScriptableObject
{
    [SerializeField]
    public string buffId = "Unset";

    public bool hasDuration = false;

    public bool isDispellable = true;

    public bool isStackable = false;

    public int stacks = 1;

    [BoxGroup("Buff Levels")]
    public int buffLevel = 1;

    [BoxGroup("Buff Levels"), ShowIf("hasDuration")]
    public Leveled<float> duration = new Leveled<float>(0.0f);
 

    [BoxGroup("For UI")]
    public Sprite HUDIcon;
    [BoxGroup("For UI")]
    public bool isHidden = false;
    [BoxGroup("For UI")]
    public bool isDetrimental = false;


    protected Unit unitAffected { private set; get; }
    protected Unit unitOwner { private set; get; }
    protected bool isActivated { private set; get; } = false;
    [HideInInspector]
    public float durationCount = 0;

    //TEST
    public bool onlyAffectsRange = false;

    #region Overridables
    public abstract void OnActivate();
    public abstract void OnDeactivate();
    #endregion


    public bool Initialize(Unit target, int level = 1, Unit owner = null)
    {
        if (onlyAffectsRange && !target.data.isRange) return false;

        if (target.data.buffsApplied.Find(buffToFind => buffToFind.buffId == buffId))
        {
            if (isStackable)
            {

                Buff existingBuff = target.data.buffsApplied.Find(buffToFind => buffToFind.buffId == buffId);
                existingBuff.stacks++;
                existingBuff.buffLevel = level;
                existingBuff.UpdateStack();
                return false;
            }

            return false;
        }

        buffLevel = level;
        unitOwner = owner;
        unitAffected = target;
        durationCount = duration.At(level);
        return true;
    }

    public void Activate()
    {
        if (!unitAffected) return;
        isActivated = true;
        OnActivate();
    }

    public void Deactivate()
    {
        if (!unitAffected) return;
        isActivated = false;     
        OnDeactivate();
    }

    protected virtual void UpdateStack()
    {
        durationCount = duration.At(buffLevel);
    }

    public void UpdateBuff(float timeDelta)
    {
        if (!hasDuration) return;
        if (durationCount > 0)
        {
            durationCount -= timeDelta;
        } else
        {
            Deactivate();
            Destroy(this);
        }
    }
}
