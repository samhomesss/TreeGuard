using System.Collections;
using UnityEngine;

public class HittableBox : MonoBehaviour, Hittable
{
    [SerializeField] private int maxHP = 3;
    private int currentHP;

    [SerializeField] private GameObject returnItem;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    MonsterSpawner monsterSpawner;

    private void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterSpawner = GetComponent<MonsterSpawner>();
        originalColor = spriteRenderer.color;
    }

    public void TakeAttack()
    {
        currentHP--;
        StartCoroutine(HitEffectCoroutine());
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator HitEffectCoroutine()
    {
        // ±ôºýÀÓ
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

        // º¸Á¤
        spriteRenderer.color = originalColor;
    }

    private void OnDestroy()
    {
        Instantiate(returnItem, transform.position, Quaternion.identity);
        monsterSpawner?.DestroySpawner();
    }
}
