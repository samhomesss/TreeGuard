using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z; // Z 고정 (2D일 경우)
        Vector3 smoothed = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothed;
    }
}
