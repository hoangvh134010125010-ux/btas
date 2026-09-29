using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Ui Pop-up")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    void Awake()
    {
        Instance = this;
    }
    public void TogglePause()
    {
        bool isPause = !pausePanel.activeSelf;
        pausePanel.SetActive(isPause);
        Time.timeScale = isPause ? 0f : 1f;
    }
    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Tắt bảng Pause
        }
        Time.timeScale = 1f;             // Cho game tiếp tục chạy lại
    }
    public void SaveGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedLevel", currentSceneIndex);
        int currentScore = GameManager.Instance.currentScore;
        int oldHightscore = PlayerPrefs.GetInt("Highscore", 0);
        if (currentScore > oldHightscore)
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
        }
        PlayerPrefs.Save();
        Debug.Log("Đã lưu!");
    }
    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
