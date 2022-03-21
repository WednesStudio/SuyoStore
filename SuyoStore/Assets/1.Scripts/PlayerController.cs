using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float moveSpeed = 10.0f; // �⺻ �̵� �ӵ�
    [SerializeField]
    private float gravity = -9.81f; // �߷� ���
    [SerializeField]
    private float rotationSpeed = 360f; // ȸ��(������ȯ) �ӵ�
    private Vector3 moveDirection; // �̵� ����
    float hAxis;
    float vAxis;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //if (characterController.isGrounded == false)
        //{
        //    moveDirection.y += gravity * Time.deltaTime;
        //}
        GetInput();
        Move();
        Rotate();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ����Ű �¿�
        vAxis = Input.GetAxisRaw("Vertical"); // ����Ű ���Ʒ�
    }

    void Move()
    {
        //moveDirection = new Vector3(hAxis, moveDirection.y, vAxis);
        //moveDirection.Normalize();
        //characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        moveDirection = new Vector3(hAxis, 0, vAxis);
        float magnitud = Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;
        moveDirection.Normalize();
        characterController.SimpleMove(moveDirection * moveSpeed);
    }

    void Rotate()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
