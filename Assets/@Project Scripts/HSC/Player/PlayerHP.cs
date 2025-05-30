using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100; // �÷��̾� �ִ� ü��
    public int currentHP; // �÷��̾� ���� ü��

    SpriteRenderer spriteRenderer; 
    Color originalColor; // ���� ����

    private void Start()
    {
        currentHP = maxHP; // ������ �� �ִ� ü������ �ʱ�ȭ
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // ���� ���� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MonsterAI monsterAI = collision.gameObject.GetComponent<MonsterAI>();
            if (monsterAI != null)
            {
                Managers.Game.PlayerHpChange(-(int)monsterAI.damage); // ������ ���ݷ¸�ŭ ü�� ����
                TakeDamage((int)monsterAI.damage); // �÷��̾� ü�� ����
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        StartCoroutine(HitEffectCoroutine());
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private IEnumerator HitEffectCoroutine()
    {
        // ������
        spriteRenderer.color = Color.red;

        float colorFadeTime = 0.3f;
        float t = 0f;
        Color startColor1 = spriteRenderer.color;

        while (t < colorFadeTime)
        {
            t += Time.deltaTime;
            float normalized = t / colorFadeTime;

            spriteRenderer.color = Color.Lerp(startColor1, originalColor, normalized);

            yield return null;
        }

        // ����
        spriteRenderer.color = originalColor;
    }

    public void Die()
    {
        Debug.Log("Player has died.");
    }
}
