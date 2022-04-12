using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerStatus playerStatus;

    public bool isMove = false;
    private float speed = 1f; // �̵� �ӵ�

    private Rigidbody charRigidbody;

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
    bool isRun;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        charRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
        Move();

        if (isAlt == true) SitAction();
        if (Input.GetKey(KeyCode.G)) GetItem();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ����Ű �¿�
        vAxis = Input.GetAxisRaw("Vertical"); // ����Ű ���Ʒ�
    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis);
        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * speed;
        moveDirection.Normalize();
        characterController.SimpleMove(moveDirection * speed);


        // ������ ���� üũ
        if (moveDirection != Vector3.zero)
        {
            isMove = true;
            speed = playerStatus.moveSpeed;
            
            charRigidbody.velocity = moveDirection * playerStatus.moveSpeed;

            // �ٶ󺸴� �������� ȸ��
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (isRun == true) Run();
            else {
                state = PlayerState.Walk;
                /* �ִϸ��̼� : Walk */
            }
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        if (state == PlayerState.Idle)
        {
            isIdle = true;
            state = PlayerState.Idle;
            speed = playerStatus.moveSpeed;
            
            /* �ִϸ��̼� : Idle */
        }
    }

    void Run()
    {
        state = PlayerState.Run;
        speed += playerStatus.runSpeed;

        /* �ִϸ��̼� : Run */
    }

    void SitAction()
    {
        if (state == PlayerState.Sit)
        {
            state = PlayerState.Idle;

            /* �ִϸ��̼� : Idle */

        }
        else
        {
            state = PlayerState.Sit;
            speed = playerStatus.sitSpeed;

            /* �ִϸ��̼� : Sit */
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
