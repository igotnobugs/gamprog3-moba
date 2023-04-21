using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Skill / Aura")]
public class Aura : Skill
{
    public GameObject auraSight;

    public override void OnActivate(Unit target = null)
    {
        // Spawns Aura Sight
        GameObject sightGO = Instantiate(auraSight,
            unitOwner.transform);

        Sight sight = sightGO.GetComponent<Sight>();
        sightGO.name = skillName + "Aura Sight";
        sight.unitOwner = unitOwner;
        sight.GetStatsFromOwner = false;
        sight.sightRange = range.At(skillLevel);
        sight.sightCollider.radius = sight.sightRange.ToUnitValue();

        sight.targetingEvents.OnAllyFound.AddListener(ApplyBuff);
        sight.targetingEvents.OnAllyExit.AddListener(RemoveBuff);
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveBuff(Unit targetUnit)
    {      
        if (buffsApplied.Count <= 0) return;

        for (int i = 0; i < buffsApplied.Count; i++)
        {
            if (buffsApplied[i])
            {
                buffsApplied[i].Deactivate();
                buffsApplied.Remove(buffsApplied[i]);                
            }
        }
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }
}
