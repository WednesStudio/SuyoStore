using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public enum WeaponKind { LightSwing, HeavySwing, KnifeSwing, None };
    //public WeaponKind wKind;
    public float rate;
    public BoxCollider meleeArea;
    //public bool isAttackRange = false;
    //public TrailRenderer trailEffect;

    private void Start()
    {
        meleeArea = GetComponent<BoxCollider>();
        rate = 1f;
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
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        // anim 타이밍에 맞춰 콜라이더 활성화
        meleeArea.enabled = true;
        yield return new WaitForSeconds(0.1f);

        meleeArea.enabled = false;
        // 공격 후 콜라이더 비활성화
        yield return new WaitForSeconds(0.6f);
    }
}
