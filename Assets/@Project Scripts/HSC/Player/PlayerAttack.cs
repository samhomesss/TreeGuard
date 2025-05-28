using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 attackPushForceDir;
    WeaponData currentWeapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentWeapon = PlayerController.Instance.currentWeapon;
    }

    void Update()
    {
        GetAttackInput();
    }

    private void GetAttackInput()
    {
        //todo : ���ݹ���

        if (Input.GetMouseButtonDown(0) && !PlayerController.Instance.isAttack)
        {
            // ���߿� ���ü�÷� �̵��� �ڵ� ����
            currentWeapon = PlayerController.Instance.currentWeapon;
            // ���� ���� �ð��� ���� �ִϸ��̼� ����ð�. �ϴ� �ӽ÷� 0.2f�� ����.
            // �߰��������� ���ݼӵ� ���� �ִϸ��̼� ����ð��� �����ϴ� �ڵ� �ʿ�.
            StartCoroutine(ControlAttackCoroutine(0.1f));
        }
    }

    IEnumerator ControlAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        yield return new WaitForSeconds(currentWeapon.readyDuration);
        Attack(time);
        yield return new WaitForSeconds(0.1f); // ���� �ִϸ��̼� ���� �� ��� ���
        rb.linearVelocity = Vector2.zero; // ���� �� �̵��ӵ� �ʱ�ȭ
        yield return new WaitForSeconds(time); // �ִϸ��̼� ������ �ð�
        PlayerController.Instance.isAttack = false;
    }

    private void Attack(float time)
    {
        // ��ų���� �ִϸ��̼�, ���� ����
        if (currentWeapon != null && currentWeapon.skills.Length > 0)
        {
            foreach (var skill in currentWeapon.skills)
            {
                Debug.Log($"Attacking with skill: {skill.skillName}");
            }
        }

        // ���� �ݵ� (������ �򸮴� �ݵ� ���ϴ°���)
        attackPushForceDir = PlayerController.Instance.moveDir;
        if (attackPushForceDir == Vector2.zero)
        {
            // �̵�Ű �Է� ���� ���� ���� ���콺 ��������
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        rb.AddForce(attackPushForceDir * currentWeapon.attackPushForce, ForceMode2D.Impulse);
    }
}
