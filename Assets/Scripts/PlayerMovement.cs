using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private InputActionAsset inputActions;

    private Rigidbody2D body;
    private Animator animator;
    private InputAction moveAction;
    private Vector2 input;

    private static readonly int LookX = Animator.StringToHash("LookX");
    private static readonly int LookY = Animator.StringToHash("LookY");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (inputActions != null)
        {
            moveAction = inputActions.FindAction("Gameplay/Move");
        }
    }

    private void OnEnable()
    {
        moveAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
    }

    private void Start()
    {
        animator.SetFloat(LookX, 0f);
        animator.SetFloat(LookY, -1f);
    }

    private void Update()
    {
        input = Vector2.zero;

        if (moveAction == null)
            return;

        Vector2 rawInput = moveAction.ReadValue<Vector2>();
        input = rawInput.normalized;

        if (rawInput.sqrMagnitude > 0.01f)
        {
            animator.SetFloat(LookX, input.x);
            animator.SetFloat(LookY, input.y);
        }
    }

    private void FixedUpdate()
    {
        body.MovePosition(
            body.position + speed * Time.fixedDeltaTime * input
        );
    }
}