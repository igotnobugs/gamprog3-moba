using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ancient : Structure
{
    public static UnityEvent<string> OnAncientDestroy = new UnityEvent<string>();
    public static UnityEvent OnVictory = new UnityEvent();

    protected override void OnUnitDeath()
    {
        OnVictory.Invoke();
        if (faction == Faction.Dire)
        {
            OnAncientDestroy.Invoke("RADIANT");
            AudioManager.Instance.PlayBGM("Victory");
        }
        else
        {
            OnAncientDestroy.Invoke("DIRE");
            AudioManager.Instance.PlayBGM("Defeat");
        }

        base.OnUnitDeath();

    }
}
