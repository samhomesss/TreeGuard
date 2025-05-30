using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 attackPushForceDir;
    SkiillEquipPlayer currentWeapon;
    PlayerAnimationEventController playerAnimationEventController;
    public Transform parentTransform;

    [SerializeField] private float attackPushForce = 7f; // ���� �ݵ� ��
    [SerializeField] private float specialAttackPushForce = 10f; // Ư�� ���� �ݵ� ��

    [SerializeField] Transform weaponTransform; // ���� ��ġ�� ���� Transform

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimationEventController = GetComponent<PlayerAnimationEventController>();
        currentWeapon = GetComponent<SkiillEquipPlayer>();
    }

    void Update()
    {
        GetAttackInput();
    }

    private void GetAttackInput()
    {
        //todo : ���ݹ���
        if(PlayerController.Instance.isDash)
        {
            //�뽬 ���� ����
            return;
        }

        if (Input.GetMouseButtonDown(0) && !PlayerController.Instance.isAttack)
        {
            // ���� ���� �ð��� ���� �ִϸ��̼� ����ð�. �ϴ� �ӽ÷� 0.2f�� ����.
            // �߰��������� ���ݼӵ� ���� �ִϸ��̼� ����ð��� �����ϴ� �ڵ� �ʿ�.
            StartCoroutine(ControlAttackCoroutine(0.1f));
            return;
        }

        //��Ŭ��
        if (Input.GetMouseButtonDown(1) && !PlayerController.Instance.isAttack && currentWeapon.CanSpecialAttack)
        {
            StartCoroutine(ControlSpecialAttackCoroutine(0.2f));
            Debug.Log("��Ŭ������ ��ų ���");
            return;
        }
    }

    IEnumerator ControlAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(currentWeapon.readyDuration);

        Attack(time);
        playerAnimationEventController.EnableAttackCollider(); // ���� �ݶ��̴� Ȱ��ȭ
        yield return new WaitForSeconds(time); // ���� �ִϸ��̼� ���� �� ��� ���
        playerAnimationEventController.DisableAttackCollider(); // ���� �ݶ��̴� ��Ȱ��ȭ

        PlayerController.Instance.isAttack = false;
    }

    IEnumerator ControlSpecialAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(currentWeapon.specialReadyDuration);

        SpecialAttack();
        playerAnimationEventController.EnableSpecialAttackCollider(); // Ư�� ���� �ݶ��̴� Ȱ��ȭ
        yield return new WaitForSeconds(time); // �ִϸ��̼� ������ �ð�
        playerAnimationEventController.DisableSpecialAttackCollider(); // Ư�� ���� �ݶ��̴� ��Ȱ��ȭ

        PlayerController.Instance.isAttack = false;
    }

    private void Attack(float time)
    {
        // ��ų���� �ִϸ��̼�, ���� ����
        if (currentWeapon != null && currentWeapon.skillData.Count > 0)
        {
            foreach (var skill in currentWeapon.skillData)
            {
                if( skill.SkillEffect != null)
                {
                    // �����̼� ���� �θ� transform�� �����̼��� ���
                    Quaternion rotation = parentTransform.rotation;
                    if(PlayerController.Instance.lookDir.x < 0)
                    {
                        // ���콺 ������ �����̸� 180�� ȸ��
                        rotation *= Quaternion.Euler(0, 180, 0);
                    }
                    GameObject effect = Instantiate(skill.SkillEffect, transform.position, rotation);
                    effect.transform.localScale = weaponTransform.localScale; // ���� ũ�⿡ ���缭 ����Ʈ ũ�� ����
                    //effect.GetComponentInChildren<ParticleSystem>().Ro
                    Destroy(effect, skill.EffectDuration); // ����Ʈ ���ӽð� �� ����
                }
            }
        }

        // �Ѿ� �߻�
        if (currentWeapon.projectiles.Count > 0)
        {
            foreach (GameObject projectilePrefab in currentWeapon.projectiles)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile proj = projectile.GetComponent<Projectile>();
                if (proj != null)
                {
                    proj.Init(PlayerController.Instance.lookDir); // �Ѿ� �ʱ�ȭ
                }
            }
        }

        // ���� ����
        if (currentWeapon.IsInvincible)
        {
            StartCoroutine(ControlInvincibleCoroutine(1f));
        }


        // ���� �ݵ� (������ �򸮴� �ݵ� ���ϴ°���)
        attackPushForceDir = PlayerController.Instance.moveDir;

        attackPushForce = currentWeapon.TotalAttackPushForce;
        if(attackPushForce < 7f)
        {
            // ���� �ݵ� ���� �ʹ� ������ �⺻������ ����
            attackPushForce = 7f;
        }

        if (attackPushForceDir == Vector2.zero)
        {
            // �̵�Ű �Է� ���� ���� ���� ���콺 ��������
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        if(PlayerController.Instance.moveDir.x * PlayerController.Instance.lookDir.x < 0 
            || PlayerController.Instance.moveDir.y * PlayerController.Instance.lookDir.y < 0)
        {
            // �̵�Ű ����� ���콺 ������ �ݴ�� �ݵ��� �ڷ� ��
            attackPushForce = 7f;
        }
        rb.AddForce(attackPushForceDir * attackPushForce, ForceMode2D.Impulse);
    }

    IEnumerator ControlInvincibleCoroutine(float time)
    {
        GetComponent<CircleCollider2D>().enabled = false; // ���� ������ ���� �ݶ��̴� ��Ȱ��ȭ
        yield return new WaitForSeconds(time);
        GetComponent<CircleCollider2D>().enabled = true; // ���� ���� ������ �ݶ��̴� Ȱ��ȭ
    }

    private void SpecialAttack()
    {
        // todo : ����Ʈ

        // ���� �ݵ� (������ �򸮴� �ݵ� ���ϴ°���)
        attackPushForceDir = PlayerController.Instance.moveDir;
        if (attackPushForceDir == Vector2.zero)
        {
            // �̵�Ű �Է� ���� ���� ���� ���콺 ��������
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        rb.AddForce(attackPushForceDir * specialAttackPushForce, ForceMode2D.Impulse);
    }
}
