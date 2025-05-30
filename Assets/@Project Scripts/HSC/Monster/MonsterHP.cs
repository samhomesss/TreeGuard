using System.Collections;
using TMPro;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] private float maxHP = 300f;
    [SerializeField] float currentHP;

    //public int maxIceStacks = 3;
    public int currentIceStacks = 0; // ���� ���� ��
    public float[] slowMultipliers = { 1.0f, 0.7f, 0.5f, 0.01f }; // ���ú� �ӵ�

    SkiillEquipPlayer currentWeapon; // �÷��̾��� ���� ������ ����
    MonsterEffect monsterEffect;
    GameObject damageTextObj;
    DamageText damageText;
    Transform damageTextCanvas;
    MonsterAI monsterAI;

    private Coroutine fireCoroutine;

    private void Start()
    {
        currentWeapon = FindAnyObjectByType<SkiillEquipPlayer>();
        damageTextCanvas = GameObject.Find("DamageTextCanvas").transform;
        monsterEffect = GetComponent<MonsterEffect>();
        damageTextObj = Resources.Load<GameObject>("DamageText");
        monsterAI = GetComponent<MonsterAI>();

        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage, Color.yellow);
            }
        }
        else if (collision.CompareTag("PlayerAttack"))
        {
            TakeAttack();
            Debug.Log("���Ͱ� ������ �޾ҽ��ϴ�.");
            // ���� �ݶ��̴� ��Ȱ��ȭ (��ø�� ����)
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
        dt.transform.SetParent(damageTextCanvas, false); // ĵ������ �ڽ����� ����
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
        float duration = 4f; // �� ���� ���� �ð�

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
        float iceDuration = 3f; // ���� ���� ���� �ð�

        currentIceStacks++;
        monsterEffect.UpdateIceEffect(currentIceStacks);
        monsterAI.moveSpeed = monsterAI.originalMoveSpeed * GetCurrentSpeedMultiplier();
        StartCoroutine(IceSlowEffect(iceDuration));
    }

    private IEnumerator IceSlowEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (currentIceStacks > 0)
        {
            currentIceStacks--;
            monsterEffect.UpdateIceEffect(currentIceStacks);
            monsterAI.moveSpeed = monsterAI.originalMoveSpeed * GetCurrentSpeedMultiplier();
        }
    }

    public float GetCurrentSpeedMultiplier()
    {
        Debug.Log($"Current Ice Stacks: {currentIceStacks}");
        Debug.Log("�̵��ӵ�" + monsterAI.moveSpeed);
        if (currentIceStacks >= slowMultipliers.Length)
        {
            return slowMultipliers[slowMultipliers.Length - 1]; // �ִ� ���� ���� �ʰ��ϸ� ���� ���� �ӵ� ��ȯ
        }
        return slowMultipliers[currentIceStacks];
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
