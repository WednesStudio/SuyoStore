using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private float speed = 1f; // �̵� �ӵ�
    [SerializeField]
    private float moveSpeed = 10.0f; // �⺻ ������ �� �̵� �ӵ�
    [SerializeField]
    private float runSpeed = 5.0f; // �޸��� ������ �� �̵� �ӵ�
    [SerializeField]
    private float sitSpeed = 3.0f; // �ɱ� ������ �� �̵� �ӵ�

    //private float gravity = -9.81f; // �߷� ���
    [SerializeField]
    private float rotationSpeed = 360f; // ȸ��(������ȯ) �ӵ�
    private Vector3 moveDirection; // �̵� ����
    float hAxis;
    float vAxis;

    // �׼�
    enum PlayerState{ Idle, Walk, Run, Sit, Attack, Lay, Dead };
    PlayerState state = PlayerState.Idle;

    bool isIdle = true;
    bool isAlt; // alt Ű�� �������� ����
    bool isAttack; 
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //if (characterController.isGrounded == false)
        //{
        //    moveDirection.y += gravity * Time.deltaTime;
        //}
        GetInput();

        Idle();
        if (isAlt == true) SitAction();
        if (Input.GetButton("Run")) Run();

        Move();

        if (Input.GetKey(KeyCode.G)) GetItem();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ����Ű �¿�
        vAxis = Input.GetAxisRaw("Vertical"); // ����Ű ���Ʒ�

        // �׼� ����
        isAlt = Input.GetButtonDown("Sit"); // alt Ű �Է� ����
        isAttack = Input.GetButtonDown("Attack");
    }

    void Move()
    {
        //moveDirection = new Vector3(hAxis, moveDirection.y, vAxis);
        //moveDirection.Normalize();
        //characterController.Move(moveDirection * speed * Time.deltaTime);

        moveDirection = new Vector3(hAxis, 0, vAxis);
        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * speed;
        moveDirection.Normalize();
        characterController.SimpleMove(moveDirection * speed);


        // ������ ���� üũ
        if (moveDirection != Vector3.zero)
        {
            /* �ִϸ��̼� : Walk */

            // �ٶ󺸴� �������� ȸ��
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            /* �ִϸ��̼� : Idle */
        }
    }

    void Idle()
    {
        if (state == PlayerState.Idle)
        {
            isIdle = true;
            state = PlayerState.Idle;

            /* �ִϸ��̼� : Idle */

            speed = moveSpeed;
        }
    }

    void Run()
    {
        if (state == PlayerState.Idle)
        {
            state = PlayerState.Run;

            /* �ִϸ��̼� : Run */

            speed += runSpeed;
        }
    }

    void SitAction()
    {
        if (state == PlayerState.Sit)
        {
            state = PlayerState.Idle;
        }
        else
        {
            state = PlayerState.Sit;

            /* �ִϸ��̼� : Sit */

            speed += sitSpeed;
        }
    }


    void GetItem()
    {
        if (state == PlayerState.Run) return; // �Ĺ� �Ұ���
        else if(state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // �Ĺ� ����

            // Ÿ�� ������ ��ġ�� �ٴ��� ��:
            /* �ִϸ��̼� : PickUpItem */

            // Ÿ�� ������ ��ġ�� �ٴ��� �ƴ� ��:
            /* �ִϸ��̼� : CatchingItem */


            /*
             * Ÿ�� ������ �����
             * �κ��丮�� Ÿ�� ������ �߰�
             */


            // ������ ���� �Ϸ� �� �⺻ �ڼ��� ��ȯ
            state = PlayerState.Idle;
        }
        else
        {
            
        }
    }

    void Attack()
    {
        if (state == PlayerState.Run) return; // ���� �Ұ���
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            // ���� ���� ����
                /* �ִϸ��̼� : WeaponAttack */

            // ���� ������ ����
                /* �ִϸ��̼� : FistAttack */
        }
        else
        {
        }
    }

    void LayDown()
    {
        //if(��� ������ == ������ || ħ����)

        if (state == PlayerState.Run) return; // ���� �Ұ���
        else if (state == PlayerState.Idle || state == PlayerState.Sit)
        {
            if (state == PlayerState.Sit)
            {
                Idle();
            }

            // ���� ����
            state = PlayerState.Lay;

            /* �ִϸ��̼� : LayDown */

        }
        else { }
    }

    void Sleep()
    {
        /* 
         * �Ϸ� ��ŵ(day++;)
         * ü��, ������ �������ͽ� ��ȭ(�����ۿ� ���� ���� �޶���)
         * �Ϸ簡 ��ŵ�� ���� ���� �ڼ����� �⺻ �ڼ��� ��ȯ
         */
    }
}
