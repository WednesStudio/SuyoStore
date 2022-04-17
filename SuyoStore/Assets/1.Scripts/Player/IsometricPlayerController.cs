using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private Vector3 moveDirection; // 이동 방향
    [SerializeField]
    private float moveSpeed = 10.0f; // 기본 이동 속도
    [SerializeField]
    private float rotationSpeed = 10.0f; // 회전(방향전환) 속도

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
        hAxis = Input.GetAxisRaw("Horizontal"); // 방향키 좌우
        vAxis = Input.GetAxisRaw("Vertical"); // 방향키 위아래
    }

    void Move()
    {
        transform.Rotate(0, hAxis * rotationSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * vAxis;
        characterController.SimpleMove(forward * curSpeed);

    }
}
