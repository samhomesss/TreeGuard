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
        //todo : 공격버퍼

        if (Input.GetMouseButtonDown(0) && !PlayerController.Instance.isAttack)
        {
            // 나중에 장비교체시로 이동될 코드 한줄
            currentWeapon = PlayerController.Instance.currentWeapon;
            // 여기 들어가는 시간은 어택 애니메이션 실행시간. 일단 임시로 0.2f로 설정.
            // 추가로직으로 공격속도 값이 애니메이션 실행시간을 조절하는 코드 필요.
            StartCoroutine(ControlAttackCoroutine(0.1f));
        }
    }

    IEnumerator ControlAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        yield return new WaitForSeconds(currentWeapon.readyDuration);
        Attack(time);
        yield return new WaitForSeconds(0.1f); // 공격 애니메이션 시작 후 잠시 대기
        rb.linearVelocity = Vector2.zero; // 공격 후 이동속도 초기화
        yield return new WaitForSeconds(time); // 애니메이션 끝나는 시간
        PlayerController.Instance.isAttack = false;
    }

    private void Attack(float time)
    {
        // 스킬마다 애니메이션, 연출 실행
        if (currentWeapon != null && currentWeapon.skills.Length > 0)
        {
            foreach (var skill in currentWeapon.skills)
            {
                Debug.Log($"Attacking with skill: {skill.skillName}");
            }
        }

        // 공격 반동 (앞으로 쏠리는 반동 말하는거임)
        attackPushForceDir = PlayerController.Instance.moveDir;
        if (attackPushForceDir == Vector2.zero)
        {
            // 이동키 입력 없을 때는 때는 마우스 방향으로
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        rb.AddForce(attackPushForceDir * currentWeapon.attackPushForce, ForceMode2D.Impulse);
    }
}
