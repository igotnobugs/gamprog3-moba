using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData
{
    [SerializeField]
    private string id;

    public string Id => id;
}

public class BaseDatabase<T> : ScriptableObject where T: BaseData
{
    public List<T> datas;

    public T GetData(string id)
    {
        T data = datas.Find(obj => obj.Id == id);

        if (data == null)
            Debug.Log("Could not find data");

        return data;
    }
}
