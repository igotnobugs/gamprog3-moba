using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroLevelData : BaseData
{
    public int level;
    public float requiredExp;
    public float expKillBounty;
    public float goldLastHitBounty;
    public float goldAssistBounty;
    public float respawnTime;

    public bool WillLevelUp(float exp)
    {
        return exp >= requiredExp;
    }
}