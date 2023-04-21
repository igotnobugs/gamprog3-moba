using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Unit
{
    public Lane lane;
    public Tier tier;

    [SerializeField]
    private List<Structure> precedingStructures;
    [SerializeField]
    private List<Structure> nextStructures;

    protected override void OnUnitDeath()
    {
        base.OnUnitDeath();

        MakeVulnerable();
    }

    private void MakeVulnerable()
    {
        int count = 0;
        if (precedingStructures == null) return;
        // check the prev list of its next structures
        for (int i = 0; i < precedingStructures.Count; i++)
        {
            if (precedingStructures[i].nextStructures.Contains(this))
                precedingStructures[i].nextStructures.Remove(this);

            for (int j = 0; j < nextStructures.Count; j++)
            {
                if (precedingStructures[i].nextStructures[j] == null)
                    count++;
            }
            
            if (count == precedingStructures[i].nextStructures.Count)
                precedingStructures[i].isInvulnerable = false;

            if (precedingStructures[i].tier == Tier.Four && precedingStructures[i].isInvulnerable)
            {
                precedingStructures[i].isInvulnerable = false;
                precedingStructures[i].nextStructures.Clear();
            }
        }
    }

    protected override void GainGold(Unit victim)
    {
        base.GainGold(victim);
    }
}

public enum Tier
{
    One, Two, Three, Racks, Four, Ancient // Racks = barracks
}
