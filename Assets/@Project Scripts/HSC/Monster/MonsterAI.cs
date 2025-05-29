using System.Net.NetworkInformation;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float originalMoveSpeed = 5f; // 몬스터 기본 이동 속도
    public float moveSpeed = 5f; // 몬스터 이동 속도
    [SerializeField] private float detectionRange = 20f;
    private Vector2 moveDir;


    [SerializeField] private float wanderRange = 3f; // 몬스터가 돌아다닐 범위
    [SerializeField] private float wanderInterval = 2f;


    // 웨이브 타이밍인데 나중엔 웨이브 매니저 두고 몬스터는 몇번째 웨이브에 출발할지만 정해두어야함.
    [SerializeField] private float startWaveTime;
    private float timeSinceStart;


    private Transform player;
    private Transform tree;


    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tree = GameObject.Find("Tree").transform;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = originalMoveSpeed;
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (ShouldChasePlayer())
        {
            ChasePlayer();
        }
        else if (timeSinceStart >= startWaveTime)
        {
            // 매번 초기화할필요는 없는데.. 아몰랑 ㅎ
            moveDir = (tree.position - transform.position).normalized;
        }
        else
        {
            // Todo : 몬스터 순찰 로직
            moveDir = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveSpeed * moveDir;
    }

    private bool ShouldChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    private void ChasePlayer()
    {
        moveDir = (player.position - transform.position).normalized;
    }
}
