using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public ItemObject data;
    private void Start()
    {
        if (data != null)
            LoadItemObject(data);
    }
    private void Update()
    {
        if (data == null)
            return;
    }
    private void LoadItemObject(ItemObject _data)
    {
        //remove children objects i.e. visuals
        foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }
        //load current item visuals
        GameObject visuals = Instantiate(_data.itemPrefab);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
    }
}