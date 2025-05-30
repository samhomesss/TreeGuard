using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100; // 플레이어 최대 체력
    public int currentHP; // 플레이어 현재 체력

    SpriteRenderer spriteRenderer; 
    Color originalColor; // 원래 색상

    private void Start()
    {
        currentHP = maxHP; // 시작할 때 최대 체력으로 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MonsterAI monsterAI = collision.gameObject.GetComponent<MonsterAI>();
            if (monsterAI != null)
            {
                Managers.Game.PlayerHpChange(-(int)monsterAI.damage); // 몬스터의 공격력만큼 체력 감소
                TakeDamage((int)monsterAI.damage); // 플레이어 체력 감소
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
        // 깜빡임
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

        // 보정
        spriteRenderer.color = originalColor;
    }

    public void Die()
    {
        Debug.Log("Player has died.");
    }
}
