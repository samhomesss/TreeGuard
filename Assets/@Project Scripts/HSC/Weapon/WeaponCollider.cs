using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // layer�� Hittable�� ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hittable"))
        {
            // Hittable �������̽��� ������ ��ü���� Ȯ��
            Hittable hittable = collision.GetComponent<Hittable>();
            if (hittable != null)
            {
                hittable.TakeAttack();
            }
        }
    }
}
