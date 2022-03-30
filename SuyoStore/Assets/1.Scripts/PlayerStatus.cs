using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 100;
    public int maxSatiety = 100;
    public int maxFatique = 100;

    public int currentHp; // ü��
    public int currentSatiety; // ������
    public int currentFatigue; // �Ƿε�
    public int currentAttack; // ���ݷ�
    public int currentStamina; // ������

    void Awake()
    {
        currentHp = 10;
    }


    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
    
        // ���� ���� ����
        if(currentHp <= 0)
        {
            Die();
        }

        // ȸ��
            // ġ���� ������ ���
            // ����, ħ�� ������ ���
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
    //private float speed; // ���ǵ�

    //public void SetSpeed(float speed)
    //{
    //    if (speed > 0) this.speed = speed;
    //}
    //public float GetSpeed() {
    //    return speed;
    //}
}
