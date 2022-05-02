using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private PoolableObject PrefabObj;
    private List<PoolableObject> AvailableObjects;

    private ObjectPool(PoolableObject Prefab, int Size)
    {
        PrefabObj = Prefab;
        AvailableObjects = new List<PoolableObject>(Size);
    }

    //public static ObjectPool CreateInstance(PoolableObject poolable, int Size)
    //{
    //    ObjectPool pool = new ObjectPool(PrefabObj, Size);

    //    GameObject poolObject = new GameObject(PrefabObj.name + " Pool");
    //    pool.CreateObjects(poolObject.transform, Size);

    //    return pool;
    //}

    public void CreateObjects(Transform parent, int Size)
    {
        for(int i = 0; i<Size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(PrefabObj, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        AvailableObjects.Add(poolableObject);
    }

    public PoolableObject GetObjects()
    {
        if(AvailableObjects.Count > 0)
        {
            PoolableObject instance = AvailableObjects[0];
            AvailableObjects.RemoveAt(0);

            instance.gameObject.SetActive(true);

            return instance;
        }
        else
        {
            Debug.LogError("Could not get an Object from pool");
            return null;
        }

    }
}
