using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 attackPushForceDir;
    SkiillEquipPlayer currentWeapon;
    PlayerAnimationEventController playerAnimationEventController;
    public Transform parentTransform;

    [SerializeField] private float attackPushForce = 7f; // 공격 반동 힘
    [SerializeField] private float specialAttackPushForce = 10f; // 특수 공격 반동 힘

    [SerializeField] Transform weaponTransform; // 무기 위치를 위한 Transform

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
        //todo : 공격버퍼
        if(PlayerController.Instance.isDash)
        {
            //대쉬 공격 없음
            return;
        }

        if (Input.GetMouseButtonDown(0) && !PlayerController.Instance.isAttack)
        {
            // 여기 들어가는 시간은 어택 애니메이션 실행시간. 일단 임시로 0.2f로 설정.
            // 추가로직으로 공격속도 값이 애니메이션 실행시간을 조절하는 코드 필요.
            StartCoroutine(ControlAttackCoroutine(0.1f));
            return;
        }

        //우클릭
        if (Input.GetMouseButtonDown(1) && !PlayerController.Instance.isAttack && currentWeapon.CanSpecialAttack)
        {
            StartCoroutine(ControlSpecialAttackCoroutine(0.2f));
            Debug.Log("우클릭으로 스킬 사용");
            return;
        }
    }

    IEnumerator ControlAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(currentWeapon.readyDuration);

        Attack(time);
        playerAnimationEventController.EnableAttackCollider(); // 공격 콜라이더 활성화
        yield return new WaitForSeconds(time); // 공격 애니메이션 시작 후 잠시 대기
        playerAnimationEventController.DisableAttackCollider(); // 공격 콜라이더 비활성화

        PlayerController.Instance.isAttack = false;
    }

    IEnumerator ControlSpecialAttackCoroutine(float time)
    {
        PlayerController.Instance.isAttack = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(currentWeapon.specialReadyDuration);

        SpecialAttack();
        playerAnimationEventController.EnableSpecialAttackCollider(); // 특수 공격 콜라이더 활성화
        yield return new WaitForSeconds(time); // 애니메이션 끝나는 시간
        playerAnimationEventController.DisableSpecialAttackCollider(); // 특수 공격 콜라이더 비활성화

        PlayerController.Instance.isAttack = false;
    }

    private void Attack(float time)
    {
        // 스킬마다 애니메이션, 연출 실행
        if (currentWeapon != null && currentWeapon.skillData.Count > 0)
        {
            foreach (var skill in currentWeapon.skillData)
            {
                if( skill.SkillEffect != null)
                {
                    // 로테이션 값은 부모 transform의 로테이션을 사용
                    Quaternion rotation = parentTransform.rotation;
                    if(PlayerController.Instance.lookDir.x < 0)
                    {
                        // 마우스 방향이 왼쪽이면 180도 회전
                        rotation *= Quaternion.Euler(0, 180, 0);
                    }
                    GameObject effect = Instantiate(skill.SkillEffect, transform.position, rotation);
                    effect.transform.localScale = weaponTransform.localScale; // 무기 크기에 맞춰서 이펙트 크기 조정
                    //effect.GetComponentInChildren<ParticleSystem>().Ro
                    Destroy(effect, skill.EffectDuration); // 이펙트 지속시간 후 제거
                }
            }
        }

        // 총알 발사
        if (currentWeapon.projectiles.Count > 0)
        {
            foreach (GameObject projectilePrefab in currentWeapon.projectiles)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile proj = projectile.GetComponent<Projectile>();
                if (proj != null)
                {
                    proj.Init(PlayerController.Instance.lookDir); // 총알 초기화
                }
            }
        }

        // 무적 상태
        if (currentWeapon.IsInvincible)
        {
            StartCoroutine(ControlInvincibleCoroutine(1f));
        }


        // 공격 반동 (앞으로 쏠리는 반동 말하는거임)
        attackPushForceDir = PlayerController.Instance.moveDir;

        attackPushForce = currentWeapon.TotalAttackPushForce;
        if(attackPushForce < 7f)
        {
            // 공격 반동 힘이 너무 작으면 기본값으로 설정
            attackPushForce = 7f;
        }

        if (attackPushForceDir == Vector2.zero)
        {
            // 이동키 입력 없을 때는 때는 마우스 방향으로
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        if(PlayerController.Instance.moveDir.x * PlayerController.Instance.lookDir.x < 0 
            || PlayerController.Instance.moveDir.y * PlayerController.Instance.lookDir.y < 0)
        {
            // 이동키 방향과 마우스 방향이 반대면 반동을 뒤로 줌
            attackPushForce = 7f;
        }
        rb.AddForce(attackPushForceDir * attackPushForce, ForceMode2D.Impulse);
    }

    IEnumerator ControlInvincibleCoroutine(float time)
    {
        GetComponent<CircleCollider2D>().enabled = false; // 무적 상태일 때는 콜라이더 비활성화
        yield return new WaitForSeconds(time);
        GetComponent<CircleCollider2D>().enabled = true; // 무적 상태 끝나면 콜라이더 활성화
    }

    private void SpecialAttack()
    {
        // todo : 이펙트

        // 공격 반동 (앞으로 쏠리는 반동 말하는거임)
        attackPushForceDir = PlayerController.Instance.moveDir;
        if (attackPushForceDir == Vector2.zero)
        {
            // 이동키 입력 없을 때는 때는 마우스 방향으로
            attackPushForceDir = PlayerController.Instance.lookDir;
        }

        rb.AddForce(attackPushForceDir * specialAttackPushForce, ForceMode2D.Impulse);
    }
}
