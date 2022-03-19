using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public int hp; // ü��
    public int satiety; // ������
    public int fatigue; // �Ƿε�
    public int attack; // ���ݷ�
    public int stamina; // ������

    public int maxHp = 100;
    public int maxSatiety = 100;
    public int maxFatique = 100;

    private float speed; // ���ǵ�

    public void SetSpeed(float speed)
    {
        if (speed > 0) this.speed = speed;
    }
    public float GetSpeed() {
        return speed;
    }
}
