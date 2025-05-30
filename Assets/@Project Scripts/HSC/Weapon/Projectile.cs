using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public float damage = 10f;
    public Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;

        // 이동 방향에 따라 총알 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // 충돌 시 제거
            Debug.Log("총알 터짐");
        }

    }
}
