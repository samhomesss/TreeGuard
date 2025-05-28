using System.Collections;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color originalColor;
    Color returnColor;

    Coroutine hitEffectCoroutine;
    Coroutine fireEffectCoroutine;

    // ���� ���� �޾��� �� ���ú� ����
    public Color[] stackColors = {
        Color.white,
        new Color(0.6f, 0.8f, 1f), // ���Ķ�
        new Color(0.4f, 0.6f, 1f), // �߰�
        new Color(0.2f, 0.4f, 1f)  // ���Ķ�
    };

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        returnColor = originalColor; // ���� ���� ����
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
        // ������
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

        // ����
        spriteRenderer.color = returnColor;
        hitEffectCoroutine = null;
    }

    public void TakeFireEffect(float duration)
    {
        // �� ���� ȿ�� ����
        if(fireEffectCoroutine != null)
        {
            StopCoroutine(fireEffectCoroutine);
        }
        fireEffectCoroutine = StartCoroutine(FireEffectCoroutine(duration));
    }

    private IEnumerator FireEffectCoroutine(float duration)
    {
        // �� ȿ�� ����
        spriteRenderer.color = Color.red; // ���÷� ���������� ����
        returnColor = Color.red;
        // ���� �ð� ���� ���
        yield return new WaitForSeconds(duration);
        // �� ȿ�� ����
        spriteRenderer.color = originalColor; // ���� �������� ����
        returnColor = originalColor; // ���� �������� ����
    }

    public void UpdateIceEffect(int currentIceStacks)
    {
        spriteRenderer.color = stackColors[Mathf.Min(currentIceStacks, stackColors.Length -1)];

        returnColor = stackColors[Mathf.Min(currentIceStacks, stackColors.Length - 1)];
    }
}
