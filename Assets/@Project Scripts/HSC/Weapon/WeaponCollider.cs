using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // layer가 Hittable인 경우
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hittable"))
        {
            // Hittable 인터페이스를 구현한 객체인지 확인
            Hittable hittable = collision.GetComponent<Hittable>();
            if (hittable != null)
            {
                hittable.TakeAttack();
            }
        }
    }
}
