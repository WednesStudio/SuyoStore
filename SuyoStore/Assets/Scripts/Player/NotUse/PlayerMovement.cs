using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed = 10.0f; // �⺻ �̵� �ӵ�
    [SerializeField]
    private float gravity = -9.81f; // �߷� ���

    private float rotationSpeed; // ȸ��(������ȯ) �ӵ�
    private Vector3 moveDirection; // �̵� ����


    private void Awake()
    {
        // �ٴڿ� ���� ���� ��
        if(characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }
}
