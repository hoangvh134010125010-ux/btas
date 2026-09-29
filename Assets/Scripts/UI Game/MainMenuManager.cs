using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Ui Element")]
    public Button continueButton;
    public Text[] hightscoreTexts;
    public GameObject highscorePanel;
    
    void Start()
    {
        int savedlevel = PlayerPrefs.GetInt("Savedlevel", -1);
        if (continueButton != null)
        {
            continueButton.interactable = (savedlevel != -1);
        }
    }
    public void StartNewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map1");
    }
    public void ContinueGame()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", -1);
        if (savedLevel != -1)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(savedLevel);
        }
        else
        {
            Debug.Log("Chưa có dữ liệu lưu");
        }
    }
    public void OpenHighscorePanel()
    {
        if (highscorePanel != null) highscorePanel.SetActive(true);
        DisplayHighScores();
    }
    public void CloseHighscorepanel()
    {
        if (highscorePanel != null) highscorePanel.SetActive(false);
    }
    public void DisplayHighScores()
{
    List<HighScoreEntry> list = HighScoreManager.GetHighScores();

    for (int i = 0; i < hightscoreTexts.Length; i++)
    {
        if (hightscoreTexts[i] != null)
        {
            if (i < list.Count)
            {
                hightscoreTexts[i].text = $"{i + 1}. {list[i].name} - {list[i].score}";
            }
            else
            {
                hightscoreTexts[i].text = $"{i + 1}. --- - 0";
            }
        }
    }
}
    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Đã thoát game!");
    }
}
