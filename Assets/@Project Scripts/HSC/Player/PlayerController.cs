using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    PlayerAttack playerAttack;
    PlayerDash playerDash;
    PlayerMove playerMove;
    //PlayerLook playerLook;

    // 현재 플레이어 상태
    public bool isDash = false;
    public bool isAttack = false;
    
    // 현재 이동 키 입력 방향 : 대쉬 중에는 갱신 안됨 다른 상황일땐 계속 갱신됨.
    public Vector2 moveDir;

    // 마우스-플레이어 방향
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
            //    //todo : 대쉬 공격
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
        mouseWorldPos.z = 0; // 2D 게임이므로 z축은 0으로 설정
        lookDir = (mouseWorldPos - transform.position).normalized;

        // filp
        if (lookDir.x < 0)
        {
            // 왼쪽을 바라보는 경우
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // 오른쪽을 바라보는 경우
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
