using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Cài đặt màn chơi")]
    public string levelName = "Map 1";
    public int targetScore = 100; // Đặt số điểm cần đạt của màn này trực tiếp trên Inspector
    [HideInInspector] public int currentScore = 0;
    private int remainingEnemies;

    [Header("Tham chiếu UI")]
    public Text levelText;
    public Text scoreText;
    public Text enemyText;
    public Text livesText;

    void Awake()
    {
        // Đảm bảo Instance luôn trỏ về GameManager của màn hiện tại
        Instance = this;
    }
    
    void Start()
    {
        // Khởi tạo lại toàn bộ dữ liệu mỗi khi load hoặc chơi lại màn
        currentScore = 0;
        
        // Đếm lại số quái có trong màn hiện tại (Yêu cầu quái phải gắn Tag là "Enemy")
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        remainingEnemies = enemies.Length;

        UpdateUI(); 
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
        CheckLevelComplete();
    }

    public void OnEnemyKilled(int scoreReward)
    {
        remainingEnemies--;
        if (remainingEnemies < 0) remainingEnemies = 0;
        AddScore(scoreReward);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (levelText != null) levelText.text = levelName;
        if (scoreText != null) scoreText.text = $"Điểm: {currentScore}/{targetScore}";
        if (enemyText != null) enemyText.text = $"Quái: {remainingEnemies}";
    }

    public void UpdateLivesUI(int currentLives)
    {
        if (livesText != null) livesText.text = $"Mạng: {currentLives}";
    }

    private void CheckLevelComplete()
    {
        if (currentScore >= targetScore)
        {
            Debug.Log("Đủ điểm qua màn: " + levelName);
            // Gọi hàm chuyển màn ở đây nếu muốn tự động qua màn
            // LoadNextLevel();
        }
    }

    public bool IsEnoughScore()
    {
        return currentScore >= targetScore;
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("SavedLevel", nextSceneIndex);
        PlayerPrefs.Save();

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã hoàn thành tất cả các màn!");
            SceneManager.LoadScene("MainMenu");
        }
    }
}