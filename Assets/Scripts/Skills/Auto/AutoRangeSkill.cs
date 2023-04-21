using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

//Auto skills uses attackDamage of unit
public class AutoRangeSkill : Skill
{
    [Header("Projectile Settings")]

    [Required]
    public Projectile projectile;

    [SerializeField]
    private Vector3 projectileOffset;

    public bool overrideProjectileSettings;

    [ShowIf("overrideProjectileSettings")]
    public float projectileSpeed = 200;

    public override void OnActivate(Unit target = null)
    {
        if (projectile == null) return;

        Projectile projectileSpawned = Instantiate(
            projectile,
            unitOwner.transform.position + projectileOffset,
            unitOwner.transform.rotation
            );

        projectileSpawned.Initialize(unitOwner);

        if (overrideProjectileSettings)
        {
            projectileSpawned.projectileSpeed = projectileSpeed;
        }

        projectileSpawned.SetTarget(target);
        projectileSpawned.OnHit = (hitObject) =>
        {
            if (hitObject.GetComponent<Unit>() != target) return;
            target.TakeDamage(unitOwner.data.attackDamage.GetTotal(), damageType, unitOwner);
            ApplyBuff(target);
        };
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        if (projectile == null) return;

        Projectile projectileSpawned = Instantiate(
            projectile,
            unitOwner.transform.position + projectileOffset,
            unitOwner.transform.rotation
            );

        projectileSpawned.Initialize(unitOwner);
        projectileSpawned.SetTarget(targetPosition);
        projectileSpawned.OnHit = (hitObject) =>
        {
            if (hitObject.TryGetComponent(out Unit hitUnit))
            {
                if (hitUnit.faction == unitOwner.faction) return;

                //Debug.Log(hitUnit);

                hitUnit.TakeDamage(baseDamage.At(skillLevel), damageType, unitOwner);
                ApplyBuff(hitUnit.GetComponent<Unit>());
            }
        };
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }
}
