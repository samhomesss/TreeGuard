using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText; // UI ≈ÿΩ∫∆Æ
    public float floatUpSpeed = 1f;
    public float destroyTime = 1f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }

    public void SetDamage(int damage, Color color)
    {
        damageText.text = damage.ToString();
        damageText.color = color;
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatUpSpeed * Time.deltaTime;
    }
}
