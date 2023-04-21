using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuffReceiver : MonoBehaviour
{
    private List<Buff> buffs = new List<Buff>();
    public List<Buff> Buffs => buffs;

    /*
    public void ApplyBuff(Buff buff, Unit owner = null)
    {
        if (buffs.Contains(buff)) return;
        buffs.Add(buff);
        if (buff.IsActivated) return;
        buff.Activate(this);
    }

    public void RemoveBuff(Buff buff, Unit owner = null)
    {
        buffs.Remove(buff);
        if(buff.IsActivated)
            buff.Deactivate(this);
    }

    public void RemoveBuff(string id, Unit owner = null)
    {
        Buff buff = buffs.Where(b => b.buffId == id).FirstOrDefault();
        if (buff == null) return;
        RemoveBuff(buff);
    }
    */
}
