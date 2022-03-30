using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 100;
    public int maxSatiety = 100;
    public int maxFatique = 100;

    public int currentHp; // 체력
    public int currentSatiety; // 포만감
    public int currentFatigue; // 피로도
    public int currentAttack; // 공격력
    public int currentStamina; // 지구력

    void Awake()
    {
        currentHp = 10;
    }


    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
    
        // 게임 오버 기준
        if(currentHp <= 0)
        {
            Die();
        }

        // 회복
            // 치료제 아이템 사용
            // 가구, 침낭 아이템 사용
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
    //private float speed; // 스피드

    //public void SetSpeed(float speed)
    //{
    //    if (speed > 0) this.speed = speed;
    //}
    //public float GetSpeed() {
    //    return speed;
    //}
}
