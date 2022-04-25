using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed = 10.0f; // 기본 이동 속도
    [SerializeField]
    private float gravity = -9.81f; // 중력 계수

    private float rotationSpeed; // 회전(방향전환) 속도
    private Vector3 moveDirection; // 이동 방향


    private void Awake()
    {
        //// 바닥에 닿지 않을 때
        //if(characterController.isGrounded == false)
        //{
        //    moveDirection.y += gravity * Time.deltaTime;
        //}

        //characterController = GetComponent<CharacterController>();
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
