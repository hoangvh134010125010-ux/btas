using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("GD Cổng")]
    public SpriteRenderer portalRenderer;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.red;
    private bool isOpened = false;
    private void Update()
    {
     if (!isOpened && GameManager.Instance != null && GameManager.Instance.IsEnoughScore())
        {
            OpenPortal();
        }   
    }
    private void OpenPortal()
    {
        isOpened = true;
        if (portalRenderer != null)
        {
            portalRenderer.color = activeColor;
        }
        Debug.Log("Cổng đã mở");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance != null && GameManager.Instance.IsEnoughScore())
            {
                Debug.Log("Đã vào cổng!!! Đang tải map tiếp theo...");
                GameManager.Instance.LoadNextLevel();
            }
            else
            {
                int needScore = GameManager.Instance.targetScore - GameManager.Instance.currentScore;
                Debug.Log($"Cổng chưa mở!!! Bạn cần thêm {needScore} điểm");
            }
        }
    }
}
