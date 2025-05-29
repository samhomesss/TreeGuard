using System.Net.NetworkInformation;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float originalMoveSpeed = 5f; // ���� �⺻ �̵� �ӵ�
    public float moveSpeed = 5f; // ���� �̵� �ӵ�
    [SerializeField] private float detectionRange = 20f;
    private Vector2 moveDir;


    [SerializeField] private float wanderRange = 3f; // ���Ͱ� ���ƴٴ� ����
    [SerializeField] private float wanderInterval = 2f;


    // ���̺� Ÿ�̹��ε� ���߿� ���̺� �Ŵ��� �ΰ� ���ʹ� ���° ���̺꿡 ��������� ���صξ����.
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
            // �Ź� �ʱ�ȭ���ʿ�� ���µ�.. �Ƹ��� ��
            moveDir = (tree.position - transform.position).normalized;
        }
        else
        {
            // Todo : ���� ���� ����
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
