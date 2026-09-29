using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    [Header("Cài đặt Di chuyển")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    private float keyboardInput;
    private float uiInput;

    [Header("Cài đặt Nhảy & Mặt đất")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    [Header("Hiệu ứng")]
    public GameObject hitVFXPrefab;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    [Header("Chỉ số Nhân vật")]
    public int maxLives = 3;
    private int currentLives;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLives = maxLives;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateLivesUI(currentLives);
        }
    }

    void Update()
    {
        // Kiểm tra chạm đất
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // Đọc phím bàn phím
        keyboardInput = Input.GetAxisRaw("Horizontal");

        // Bấm Space để nhảy từ bàn phím
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (anim != null)
        {
            float animationMoveInput = (keyboardInput != 0) ? keyboardInput : uiInput;
            anim.SetFloat("speed", Mathf.Abs(animationMoveInput));   
            anim.SetBool("isGrounded", isGrounded);          
            anim.SetFloat("yVelocity", rb.linearVelocity.y);       
        }
    }

    void FixedUpdate()
    {
        // Ưu tiên bàn phím, nếu không bấm bàn phím thì lấy tín hiệu từ Nút UI
        float finalMove = (keyboardInput != 0) ? keyboardInput : uiInput;

        // Cập nhật vận tốc di chuyển
        rb.linearVelocity = new Vector2(finalMove * moveSpeed, rb.linearVelocity.y);

        // Lật hướng nhân vật
        if (finalMove > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (finalMove < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // --- CÁC HÀM DÙNG CHO UI BUTTON ---

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void MoveLeft() 
    { 
        uiInput = -1f; 
    }

    public void MoveRight() 
    { 
        uiInput = 1f; 
    }

    public void StopMoving() 
    { 
        uiInput = 0f; 
    }

    // --- XỬ LÝ MÁU & CHẾT ---

    public void TakeDamage()
    {
        //if (isInvincible) return;
        currentLives--;
        if (AudioManager.Instance != null)
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSFX);

        if (hitVFXPrefab != null)
        Instantiate(hitVFXPrefab, transform.position, Quaternion.identity);

        Debug.Log("Mất 1 mạng! Còn lại: " + currentLives);
        if (GameManager.Instance != null)
    {
        GameManager.Instance.UpdateLivesUI(currentLives);
    }

        if (currentLives <= 0)
        {
            Die();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(BecomeInvincibleCoroutine());
        }
    }
    private IEnumerator BecomeInvincibleCoroutine()
    {
        isInvincible = true;
        float duration = 1.5f;
        float flashInterval = 0.1f;

        for (float t = 0; t < duration; t += flashInterval)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("Game Over!");
        this.enabled = false;
        if (SaveScoreUI.Instance != null && GameManager.Instance != null)
    {
        SaveScoreUI.Instance.ShowSaveScorePanel(GameManager.Instance.currentScore);
    }
    else
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    }
}