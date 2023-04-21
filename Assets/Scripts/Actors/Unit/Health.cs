using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//Any attribute that regens and has a max

[System.Serializable]
public class Health
{
    public float current = 200;
    public float max = 200;

    public float regen = 0.5f;
    public float regenModifier = 0.0f;

    public float GetPercent()
    {
        return current / max;
    }

    public float GetTotalRegen()
    {
        return regen + regenModifier;
    }

    public bool Reduce(float amount)
    {
        if (current <= 0) return true;
        current -= amount;

        if (current <= 0)
        {
            current = 0;
            return true;
        }
        return false;
    }

    public void Restore(float amount)
    {
        current += Mathf.Min(current + Mathf.Abs(amount), max);
    }

    public void Modify(float modifier)
    {
        max += modifier;
        current += modifier;
        current = Mathf.Clamp(current, 1, max);
    }

    public void Regenerate()
    {
        if (current >= max) return;
        current += GetTotalRegen() * Time.deltaTime;
        if (current >= max) current = max;
    }

    public void ModifyRegen(float modifier)
    {
        regen += modifier;
        if (regen < 0) regen = 0;
    }
}
