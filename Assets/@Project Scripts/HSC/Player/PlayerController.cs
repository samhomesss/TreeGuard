using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    PlayerAttack playerAttack;
    PlayerDash playerDash;
    PlayerMove playerMove;
    //PlayerLook playerLook;

    // ���� �÷��̾� ����
    public bool isDash = false;
    public bool isAttack = false;
    
    // ���� �̵� Ű �Է� ���� : �뽬 �߿��� ���� �ȵ� �ٸ� ��Ȳ�϶� ��� ���ŵ�.
    public Vector2 moveDir;

    // ���콺-�÷��̾� ����
    public Vector2 lookDir;

    public Vector2 moveSpeed;

    Vector3 mouseScreenPos;
    Vector3 mouseWorldPos;

    public Dictionary<ItemType, int> itemCount = new Dictionary<ItemType, int>()
    {
        { ItemType.Soul, 0 },
        { ItemType.Leaf, 0 }
    };

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

    private void Update()
    {
        SetLookDirection();
    }

    void FixedUpdate()
    {
        if (isDash)
        {
            //if(isAttack)
            //{
            //    //todo : �뽬 ����
            //    return;
            //}
            
            playerDash.Dash(moveDir);
        }
        else if(!isAttack)
        {
            playerMove.Move();
        }
    }

    private void SetLookDirection()
    {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // 2D �����̹Ƿ� z���� 0���� ����
        lookDir = (mouseWorldPos - transform.position).normalized;

        // filp
        if (lookDir.x < 0)
        {
            // ������ �ٶ󺸴� ���
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // �������� �ٶ󺸴� ���
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
