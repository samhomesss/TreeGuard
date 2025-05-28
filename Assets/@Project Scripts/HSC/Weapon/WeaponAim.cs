using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponAim : MonoBehaviour
{
    private void Update()
    {
        AimWeaponAtMouse();
    }

    private void AimWeaponAtMouse()
    {
        Vector3 aimDirection = PlayerController.Instance.lookDir;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // 마우스가 왼쪽이면 위에서 아래로 뒤집은 회전 보정
        if (PlayerController.Instance.lookDir.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
