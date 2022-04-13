using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private Vector3 moveDirection; // �̵� ����
    [SerializeField]
    private float moveSpeed = 10.0f; // �⺻ �̵� �ӵ�
    [SerializeField]
    private float rotationSpeed = 10.0f; // ȸ��(������ȯ) �ӵ�

    float hAxis;
    float vAxis;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        GetInput();
        Move();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ����Ű �¿�
        vAxis = Input.GetAxisRaw("Vertical"); // ����Ű ���Ʒ�
    }

    void Move()
    {
        transform.Rotate(0, hAxis * rotationSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * vAxis;
        characterController.SimpleMove(forward * curSpeed);

    }
}
