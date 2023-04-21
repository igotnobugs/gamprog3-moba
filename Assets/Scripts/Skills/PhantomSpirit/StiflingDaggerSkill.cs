using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Skill / Unique / Stifling Dagger")]
public class StiflingDaggerSkill : Skill
{
    [Header("Projectile Settings")]
    public Leveled<float> bonusDamageFromAttack = new Leveled<float>(0.25f);


    [Header("Projectile Settings")]

    [Required]
    public Projectile projectile;

    [SerializeField]
    private Vector3 projectileOffset;

    public bool overrideProjectileSettings = true;

    [ShowIf("overrideProjectileSettings")]
    public float projectileSpeed = 1200;

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
            target.TakeDamage(baseDamage.At(skillLevel), damageType, unitOwner);

            // New
            int bonusDamage = Mathf.CeilToInt(unitOwner.data.attackDamage.GetTotal() * bonusDamageFromAttack.At(skillLevel));
            target.TakeDamage(bonusDamage, DamageType.Physical, unitOwner);

            ApplyBuff(target);
        };
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }
}
