using System.Collections;
using TMPro;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    [SerializeField] float currentHP;
    [SerializeField] float moveSpeed = 5f; // 몬스터 이동 속도

    //public int maxIceStacks = 3;
    public int currentIceStacks = 0; // 현재 스택 수
    public float[] slowMultipliers = { 1.0f, 0.8f, 0.6f, 0.2f }; // 스택별 속도

    SkiillEquipPlayer currentWeapon; // 플레이어의 현재 장착된 무기
    MonsterEffect monsterEffect;
    GameObject damageTextObj;
    DamageText damageText;
    Transform damageTextCanvas;

    private Coroutine fireCoroutine;

    private void Start()
    {
        currentWeapon = FindAnyObjectByType<SkiillEquipPlayer>();
        damageTextCanvas = GameObject.Find("DamageTextCanvas").transform;
        monsterEffect = GetComponent<MonsterEffect>();
        damageTextObj = Resources.Load<GameObject>("DamageText");
        
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            TakeAttack();
            // 공격 콜라이더 비활성화 (중첩딜 방지)
            //collision.gameObject.SetActive(false);
        }
    }

    private void TakeAttack()
    {
        TakeDamage(currentWeapon.TotalDamage, Color.yellow);
        if (currentWeapon.HasFire)
        {
            TakeFireAttack();
        }

        if (currentWeapon.HasIce)
        {
            TakeIceAttack();
        }
    }

    private void TakeDamage(float damage, Color color)
    {
        monsterEffect.TakeDamageEffect();

        GameObject dt = Instantiate(damageTextObj, transform.position, Quaternion.identity);
        dt.transform.SetParent(damageTextCanvas, false); // 캔버스에 자식으로 설정
        damageText = dt.GetComponent<DamageText>();
        damageText.SetDamage((int)damage, color);

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void TakeFireAttack()
    {
        float duration = 4f; // 불 공격 지속 시간

        monsterEffect.TakeFireEffect(duration);

        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireCoroutine = StartCoroutine(FireDamageOverTime(duration));
    }

    private IEnumerator FireDamageOverTime(float duration)
    {
        float elapsedTime = 0f;
        float fireDamage = 5f;
        float damageInterval = 1f;

        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(damageInterval);
            TakeDamage(fireDamage, Color.red);
            
            elapsedTime += damageInterval;
        }
    }

    private void TakeIceAttack()
    {
        float iceDuration = 2f; // 얼음 공격 지속 시간

        currentIceStacks++;
        monsterEffect.UpdateIceEffect(currentIceStacks);
        StartCoroutine(IceSlowEffect(iceDuration));
    }

    private IEnumerator IceSlowEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (currentIceStacks > 0)
        {
            currentIceStacks--;
            monsterEffect.UpdateIceEffect(currentIceStacks);
        }
    }



    private void Die()
    {
        Destroy(gameObject);
    }
}
