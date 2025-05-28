using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetDashInput();
    }

    private void GetDashInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !PlayerController.Instance.isDash)
        {
            StartCoroutine(ControlDashState(dashDuration));
        }
    }

    IEnumerator ControlDashState(float time)
    {
        PlayerController.Instance.isDash = true;
        yield return new WaitForSeconds(time);
        PlayerController.Instance.isDash = false;
    }

    public void Dash(Vector2 direction)
    {
        // 가만히 있을 때 대쉬 : 마우스 방향으로.
        if (direction == Vector2.zero)
        {
            direction = PlayerController.Instance.lookDir;
        }

        // 이동 중 대쉬
        Vector2 dashVelocity = direction * dashSpeed;
        rb.linearVelocity = dashVelocity;

        // Todo : 대시 후 대시 상태를 유지할 수 있도록 추가 로직 구현
    }
}
