using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStatus playerStatus = new PlayerStatus();

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    float speed = 10;
    float runSpeed = 3;
    float sitSpeed = 3;
    int motion; // 플레이어의 모션 상태

    private void Update()
    {
        playerStatus.SetSpeed(speed);

        GetInput();

        Move();
        Run();
        Sit();

    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * Time.deltaTime;

        // turn
        transform.LookAt(transform.position + moveVec);
    }

    void Run()
    {
        if (Input.GetButton("Run"))
        {
            // 달리기 애니메이션

        }

    }

    void Sit()
    {
        if (Input.GetButton("Sit"))
        {

        }

        // 앉기 자세 지속

        // 앉기 애니메이션

    }

    void GetItem()
    {

    }

    void Attack()
    {
        // 기본 공겨
        // 무기 공격
    }

    void Sleep(int changeHp, int changeFatigue)
    {
        // 취침
        


        // 하루 스킵
        // 체력 회복
        // 포만감 감소
    }
}

//pos를 주는 방식.. !
