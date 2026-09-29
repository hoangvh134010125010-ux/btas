using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    public float moveSpeed = 2f;
    public float moveDistance = 1f;
    private Vector3 startPos;
    private bool movingRight = true;
    [Header("Tương tác")]
    public float bounceForce = 8f;
    public int scoreReward = 15;
    private Rigidbody2D rb;
    private Animator anim;
    [Header("Hiệu ứng")]
    public GameObject enemyDieVFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
            }
        }
        if (anim != null)
        {
            bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
            anim.SetBool("isMoving", moving);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
    }
    public void DieByStomp(Rigidbody2D playerRb)
    {
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(AudioManager.Instance.enemyDieSFX);

        if (enemyDieVFX != null)
            Instantiate(enemyDieVFX, transform.position, Quaternion.identity);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemyKilled(scoreReward);
        }
        Destroy(gameObject);
    }
}
