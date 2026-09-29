using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HighScoreEntry
{
    public string name;
    public int score;
    public HighScoreEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
public static class HighScoreManager
{
    private const int MAX_ENTRIES = 5;
    public static List<HighScoreEntry> GetHighScores()
    {
        List<HighScoreEntry> list = new List<HighScoreEntry>();
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            string name = PlayerPrefs.GetString($"HighScore_Name_{i}", "---");
            int score = PlayerPrefs.GetInt($"HighScore_Score_{i}", 0);
            list.Add(new HighScoreEntry(name, score));
        }
        return list;
    }
    public static bool IsHighScore(int score)
    {
        if (score <= 0) return false;
        List<HighScoreEntry> list = GetHighScores();
        return score > list[MAX_ENTRIES -1].score;
    }
    public static void SaveHighScore(string name, int score)
    {
        if (string.IsNullOrWhiteSpace(name)) name = "Player";
        List<HighScoreEntry> list = GetHighScores();
        list.Add(new HighScoreEntry(name, score));
        list.Sort((a, b) => b.score.CompareTo(a.score));
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            PlayerPrefs.SetString($"HighScore_Name_{i}", list[i].name);
            PlayerPrefs.SetInt($"HighScore_Score_{i}", list[i].score);
        }
        PlayerPrefs.Save();
    }
}


