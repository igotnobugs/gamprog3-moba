using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill / Unique / Nether Swap")]
public class NetherSwapSkill : Skill
{

    private Vector3 ownerOldPosition;
    private Vector3 targetOldPosition;

    public override void OnActivate(Unit target = null)
    {
        ApplyBuff(target);

        ownerOldPosition = unitOwner.gameObject.transform.position;
        targetOldPosition = target.gameObject.transform.position;

        target.gameObject.transform.position = new Vector3(-9999, 9999, 99999);
        unitOwner.gameObject.transform.position = targetOldPosition;

        target.gameObject.transform.position = ownerOldPosition;

    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }
}
