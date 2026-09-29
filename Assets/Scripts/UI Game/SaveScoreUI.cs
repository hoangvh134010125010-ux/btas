using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveScoreUI : MonoBehaviour
{
    public static SaveScoreUI Instance;

    [Header("UI Nhập tên")]
    public GameObject saveScorePanel; 
    public InputField nameInputField;  
    public Text currentScoreText;    
    private int scoreToSave;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ShowSaveScorePanel(int score)
{
    scoreToSave = score;
    Time.timeScale = 0f; 

    saveScorePanel.SetActive(true); 

    if (currentScoreText != null) 
        currentScoreText.text = $"Điểm đạt được: {scoreToSave}";

    
    bool isTopScore = HighScoreManager.IsHighScore(scoreToSave);
    if (nameInputField != null) nameInputField.gameObject.SetActive(isTopScore);

    Debug.Log(isTopScore ? "Đạt kỉ lục mới!" : "Chưa đủ điểm vào Top 5.");
}


    public void OnClickReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickSave()
    {
        string playerName = nameInputField != null ? nameInputField.text : "Player";
        if (string.IsNullOrEmpty(playerName.Trim())) playerName = "Player";

        HighScoreManager.SaveHighScore(playerName, scoreToSave);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}