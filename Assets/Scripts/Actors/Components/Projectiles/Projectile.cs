using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile settings")]
    [SerializeField]
    public float projectileSpeed = 750.0f;

    [SerializeField] 
    private float rotationSpeed = 200.0f;

    [SerializeField] 
    private bool willUseFirePtRotation = false;

    [SerializeField] 
    private Vector3 rotationOffset = new Vector3(0, 0, 0);

    [SerializeField] 
    private GameObject projectileHit;

    [SerializeField] 
    private float hitOffset = 1.0f;

    [SerializeField] 
    private GameObject projectileFlash;

    [SerializeField] 
    private GameObject[] effects;
    
    [SerializeField]
    private bool allowOverrideForTeamColor = true;

    [SerializeField]
    public bool onlyHitTarget = true;

    private Unit unitOwner { set; get; }

    //private float baseDamage { set; get; }
    //private DamageType damageType { set; get; }

    private Rigidbody rb;
    private Unit target;
    private ParticleSystem ps;

    public Action<GameObject> OnHit;

    private Vector3 targetPosition;
    private bool isTargetingGround = false;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Initialize(Unit instigator)
    {
        unitOwner = instigator;
        //baseDamage = damage;
        //damageType = type;
        
        if (allowOverrideForTeamColor)
        {
            var main = ps.main;
            if (instigator.faction == Faction.Dire)
            {
                main.startColor = Color.red;
            } else
            {
                main.startColor = Color.green;
            }

            foreach (ParticleSystem psChild in GetComponentsInChildren<ParticleSystem>())
            {
                var mainChild = psChild.main;
                mainChild.startColor = main.startColor;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectileSpeed = projectileSpeed.ToUnitValue();

        if (projectileFlash != null)
        {
            GameObject flashInstance = Instantiate(projectileFlash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            ParticleSystem flashParticleSystem = flashInstance.GetComponent<ParticleSystem>();
            if (flashParticleSystem != null)
            {
                Destroy(flashInstance, flashParticleSystem.main.duration);
            } else
            {
                ParticleSystem flashParticleSystemParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashParticleSystemParts.main.duration);
            }
        }
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(projectileSpeed != 0)
        {
            // follow target here
            if(target)
            {
                Vector3 dirToTarget = (target.transform.position + Vector3.up * hitOffset) - transform.position;
                dirToTarget.Normalize();
                Vector3 rotation = Vector3.Cross(dirToTarget, transform.forward);
                rb.angularVelocity = -rotation * rotationSpeed;
            } else if (isTargetingGround)
            {
                Vector3 dirToTarget = targetPosition - transform.position;
                dirToTarget.Normalize();
                Vector3 rotation = Vector3.Cross(dirToTarget, transform.forward);
                rb.angularVelocity = -rotation * rotationSpeed;
            } else
            {            
                rb.angularVelocity = transform.forward * rotationSpeed;               
            }

            rb.velocity = transform.forward * projectileSpeed;
        }
    }

    public void SetTarget(Unit targetUnit = null)
    {
        target = targetUnit;
    }

    public void SetTarget(Vector3 targetVector)
    {
        target = null;
        isTargetingGround = true;
        targetPosition = targetVector;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Unit>() == unitOwner) return;

        if (target)
        {
            if (other.GetComponent<Unit>() != target) return;

            OnHit?.Invoke(other.gameObject);

            rb.constraints = RigidbodyConstraints.FreezeAll;

            projectileSpeed = 0;

            ApplyEffect(other.gameObject);

            Destroy(gameObject);

        } else if (isTargetingGround)
        {        
            OnHit?.Invoke(other.gameObject);
            ApplyEffect(other.gameObject);

            Destroy(gameObject, 1.0f);
        }

        Destroy(gameObject, 1.0f);
    }

    private void ApplyEffect(GameObject hitObject)
    {

        if (projectileHit != null)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 position = transform.position;

            GameObject hitInstance = Instantiate(projectileHit, position, rotation);
            if (willUseFirePtRotation)
            {
                hitInstance.transform.rotation
                    = gameObject.transform.rotation * Quaternion.Euler(0, 180.0f, 0);
            } else if (rotationOffset != Vector3.zero)
            {
                hitInstance.transform.rotation = Quaternion.Euler(rotationOffset);
            } else
            {
                hitInstance.transform.LookAt(hitObject.transform.position);
            }

            ParticleSystem hitParticleSystem = hitInstance.GetComponent<ParticleSystem>();
            if (hitParticleSystem != null)
            {
                Destroy(hitInstance, hitParticleSystem.main.duration);
            } else
            {
                ParticleSystem hitParticleSystemPart = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitParticleSystemPart.main.duration);
            }
        }

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i] != null)
                effects[i].transform.parent = null;
        }
    }

}
