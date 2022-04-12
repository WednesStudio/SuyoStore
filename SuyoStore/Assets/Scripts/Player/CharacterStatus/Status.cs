using UnityEngine;

[System.Serializable]
public class Status
{
    [SerializeField]
    public float BaseValue;
    public float GetValue()
    {
        return BaseValue;
    }

}
