using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    // 기능 구현 우선시. 추후 리팩토링
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetMoveInput();
    }

    private void GetMoveInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        if (!PlayerController.Instance.isDash)
            PlayerController.Instance.moveDir = moveInput;
        moveVelocity = moveInput * moveSpeed;
    }

    public void Move()
    {
        rb.linearVelocity = moveVelocity;
    }
}
