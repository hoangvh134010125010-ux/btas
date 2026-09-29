using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Mục tiêu")]
    public Transform target;
    [Header("Cài đặt di chuyển")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 1, -19);
    [Header("Cấu hình map")]
    public bool useBound = false;
    public float minX, maxX;
    public float minY, maxY;
    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        if (useBound)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
