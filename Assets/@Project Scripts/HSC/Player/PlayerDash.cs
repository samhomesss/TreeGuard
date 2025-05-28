using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;

    Rigidbody2D rb;
    Vector2 currentDashVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetDashInput();
    }

    void FixedUpdate()
    {
        if (PlayerController.Instance.isDash)
        {
            // �ӵ� ����: �ܺ� AddForce ����
            if (rb.linearVelocity.magnitude > currentDashVelocity.magnitude)
            {
                rb.linearVelocity = currentDashVelocity;
            }
        }
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
        // ������ ���� �� �뽬 : ���콺 ��������.
        if (direction == Vector2.zero)
        {
            direction = PlayerController.Instance.lookDir;
        }

        // �̵� �� �뽬
        currentDashVelocity = direction * dashSpeed;
        rb.linearVelocity = currentDashVelocity;

        // Todo : ��� �� ��� ���¸� ������ �� �ֵ��� �߰� ���� ����
    }
}
