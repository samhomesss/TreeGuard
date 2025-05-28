using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    PlayerAttack playerAttack;
    PlayerDash playerDash;
    PlayerMove playerMove;

    // ���� �÷��̾� ����
    public bool isDash = false;
    public bool isAttack = false;

    // ���� �̵� ����
    public Vector2 moveDir;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerDash = GetComponent<PlayerDash>();
        playerMove = GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        if (isDash)
        {
            playerDash.Dash(moveDir);
        }
        else
        {
            playerMove.Move();
        }
    }
}
