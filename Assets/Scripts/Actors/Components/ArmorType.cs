using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit Armor Type")]
public class ArmorType : ScriptableObject
{
    public ArmType type;

    public float VsBasic = 1.0f;
    public float VsPierced = 1.0f;
    public float VsSiege = 1.0f;
    public float VsHero = 1.0f;

    public float GetDamageModifier(Unit instigator)
    {
        float damageModifier = 1.0f;
        if (instigator == null) return damageModifier;

        if (instigator.data.attackType == AttackType.Basic)
            return VsBasic;
        else if (instigator.data.attackType == AttackType.Pierce)
            return VsPierced;
        else if (instigator.data.attackType == AttackType.Siege)
            return VsSiege;
        else
            return VsHero;
    }
}

public enum AttackType
{
    Basic, Pierce, Siege, Hero
}

public enum ArmType
{
    Basic, Fortified, Hero
}
