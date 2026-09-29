using Unity.Mathematics;
using UnityEngine;

public class Coin : MonoBehaviour
{
   public int scoreValue = 10;
   public GameObject coinVFXPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coinSFX);

            if (coinVFXPrefab != null)
            {
                Instantiate(coinVFXPrefab, transform.position, Quaternion.identity);
            }
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
