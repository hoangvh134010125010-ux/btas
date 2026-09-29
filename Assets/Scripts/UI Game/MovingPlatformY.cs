using UnityEngine;

public class MovingPlatformY : MonoBehaviour
{
    [Header("Settings")]
    public float moveDistance = 5f;
    public float speed = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveDistance);
        transform.position = startPos + new Vector3(0, offset, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi nhân vật chạm vào bẫy di chuyển
        if (collision.gameObject.CompareTag("Player"))
        {
            // Đặt Player làm con của bẫy để di chuyển cùng bẫy mượt mà
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Khi nhân vật rời khỏi bẫy (nhảy lên hoặc đi ra ngoài)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hủy quan hệ cha-con để nhân vật tự do di chuyển lại bình thường
            collision.transform.SetParent(null);
        }
    }
}
