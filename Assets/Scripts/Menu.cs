using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu menuInstance;
    public string HighScorePlayer;
    public int HighScore;
    public string currentPlayer;

    // Displayed text
    public TextMeshProUGUI HighScorePlayerText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI CurrentPlayerText;


    private void Awake()
    {
        if (menuInstance == null)
        {
            menuInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        HighScorePlayerText.text = "Name: " + HighScorePlayer;
        HighScoreText.text = "Score: " + HighScore;
    }

    /// <summary>
    /// Method to start the game after pushing the menu's start button
    /// </summary>
    public void StartGame()
    {
        currentPlayer = CurrentPlayerText.text;
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Method to end the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }


    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string Player;
    }


    /// <summary>
    /// Saves the high score and the name of the player listed with the score
    /// </summary>
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.HighScore = HighScore;
        data.Player = HighScorePlayer;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(Application.persistentDataPath + "/savefile.json");
    }

    /// <summary>
    /// Loads the high score and the name of the high scoring player
    /// </summary>
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            HighScorePlayer = data.Player;
        }
    }
}
