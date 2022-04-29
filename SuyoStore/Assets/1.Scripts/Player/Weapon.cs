using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { LightSwing, HeavySwing, KnifeSwing, None };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public bool isAttackRange = false;
    //public TrailRenderer trailEffect;

    private void Start()
    {
        meleeArea = GetComponentInChildren<BoxCollider>();
        //switch (type)
        //{
        //    case Type.LightSwing:
        //        rate = 1f;
        //        break;
        //    case Type.HeavySwing:
        //        rate = 1.5f;
        //        break;
        //    case Type.KnifeSwing:
        //        rate = 0.6f;
        //        break;
        //    case Type.None:
        //        rate = 0.4f;
        //        break;
        //    default:
        //        rate = 0f;
        //        break;
        //}
    }

    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        // 공격할 동안 콜라이더 활성화
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        // 공격 후 콜라이더 비활성화
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;
    }
}
