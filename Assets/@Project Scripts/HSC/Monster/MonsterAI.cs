using System.Net.NetworkInformation;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float originalMoveSpeed = 5f; // ���� �⺻ �̵� �ӵ�
    public float moveSpeed = 5f; // ���� �̵� �ӵ�
    [SerializeField] private float detectionRange = 20f;
    private Vector2 moveDir;

    public float damage = 10f; // ���Ͱ� �÷��̾�� �ִ� ���ط�
    //[SerializeField] private float wanderRange = 3f; // ���Ͱ� ���ƴٴ� ����
    //[SerializeField] private float wanderInterval = 2f;


    // ���̺� Ÿ�̹��ε� ���߿� ���̺� �Ŵ��� �ΰ� ���ʹ� ���° ���̺꿡 ��������� ���صξ����.
    //[SerializeField] private float startWaveTime;
    //private float timeSinceStart;
    public int monsterType = 1; // ���� Ÿ��

    private Transform player;
    private Transform tree;


    private Rigidbody2D rb;

    private bool onWave = false;
    private void Awake()
    {
        // ���̺� �Ŵ������� ���̺� ���� �̺�Ʈ�� ����
        if(monsterType == 1)
            WaveManager.Instance.type1WaveStart += OnWaveStart;
        else if (monsterType == 2)
            WaveManager.Instance.type2WaveStart += OnWaveStart;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tree = GameObject.Find("Tree").transform;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = originalMoveSpeed;
    }

    void Update()
    {
        if (ShouldChasePlayer())
        {
            ChasePlayer();
        }
        else if (onWave)
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

    private void OnWaveStart()
    {
        onWave = true;
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

    private void OnDestroy()
    {
        // ���̺� �Ŵ������� ���̺� ���� �̺�Ʈ ���� ����
        if (monsterType == 1)
            WaveManager.Instance.type1WaveStart -= OnWaveStart;
        else if (monsterType == 2)
            WaveManager.Instance.type2WaveStart -= OnWaveStart;
    }
}
