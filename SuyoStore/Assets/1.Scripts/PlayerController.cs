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
    int motion; // �÷��̾��� ��� ����

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
            // �޸��� �ִϸ��̼�

        }

    }

    void Sit()
    {
        if (Input.GetButton("Sit"))
        {

        }

        // �ɱ� �ڼ� ����

        // �ɱ� �ִϸ��̼�

    }

    void GetItem()
    {

    }

    void Attack()
    {
        // �⺻ ����
        // ���� ����
    }

    void Sleep(int changeHp, int changeFatigue)
    {
        // ��ħ
        


        // �Ϸ� ��ŵ
        // ü�� ȸ��
        // ������ ����
    }
}

//pos�� �ִ� ���.. !
