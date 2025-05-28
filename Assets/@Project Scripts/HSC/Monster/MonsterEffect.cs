using System.Collections;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color originalColor;
    Color returnColor;

    Coroutine hitEffectCoroutine;
    Coroutine fireEffectCoroutine;

    // 얼음 공격 받았을 시 스택별 색상
    public Color[] stackColors = {
        Color.white,
        new Color(0.6f, 0.8f, 1f), // 연파랑
        new Color(0.4f, 0.6f, 1f), // 중간
        new Color(0.2f, 0.4f, 1f)  // 진파랑
    };

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        returnColor = originalColor; // 원래 색상 저장
    }

    public void TakeDamageEffect()
    {
        if (hitEffectCoroutine != null)
        {
            StopCoroutine(hitEffectCoroutine);
        }
        hitEffectCoroutine = StartCoroutine(HitEffectCoroutine());
    }

    private IEnumerator HitEffectCoroutine()
    {
        // 깜빡임
        spriteRenderer.color = Color.white;

        float colorFadeTime = 0.3f;
        float t = 0f;
        Color startColor1 = spriteRenderer.color;

        while (t < colorFadeTime)
        {
            t += Time.deltaTime;
            float normalized = t / colorFadeTime;

            spriteRenderer.color = Color.Lerp(startColor1, returnColor, normalized);

            yield return null;
        }

        // 보정
        spriteRenderer.color = returnColor;
        hitEffectCoroutine = null;
    }

    public void TakeFireEffect(float duration)
    {
        // 불 공격 효과 구현
        if(fireEffectCoroutine != null)
        {
            StopCoroutine(fireEffectCoroutine);
        }
        fireEffectCoroutine = StartCoroutine(FireEffectCoroutine(duration));
    }

    private IEnumerator FireEffectCoroutine(float duration)
    {
        // 불 효과 시작
        spriteRenderer.color = Color.red; // 예시로 빨간색으로 변경
        returnColor = Color.red;
        // 지속 시간 동안 대기
        yield return new WaitForSeconds(duration);
        // 불 효과 종료
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
        returnColor = originalColor; // 원래 색상으로 복원
    }

    public void UpdateIceEffect(int currentIceStacks)
    {
        spriteRenderer.color = stackColors[Mathf.Min(currentIceStacks, stackColors.Length -1)];

        returnColor = stackColors[Mathf.Min(currentIceStacks, stackColors.Length - 1)];
    }
}
