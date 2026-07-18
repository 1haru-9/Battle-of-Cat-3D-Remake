using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // 移動の変数
    [Header("移動設定")]
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float dashSpeed = 9f;
    
    [SerializeField]
    private float jumpHeight = 2f;

    [SerializeField]
    private float gravity = -9.81f;

    private float verticalVelocity = 0f;

    // 入力の変数
    [Header("Input")]
    [SerializeField]
    private InputActionReference moveAction;

    [SerializeField]
    private InputActionReference dashAction;

    [SerializeField]
    private InputActionReference jumpAction;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        dashAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        dashAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        bool isDashing = dashAction.action.IsPressed();
        
        float currentSpeed = isDashing ? dashSpeed : moveSpeed;

        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 horizontalMove =
            (transform.forward * input.y +
             transform.right * input.x) * currentSpeed;

        Vector3 velocity =
            horizontalMove +
            Vector3.up * verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}