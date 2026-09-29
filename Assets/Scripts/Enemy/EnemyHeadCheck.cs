using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    private EnemyAI enemyParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyParent = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (enemyParent != null)
            {
                enemyParent.DieByStomp(playerRb);
            }
        }
    }
}
