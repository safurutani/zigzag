using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public Text notifyHighScore;
    private int picCounter;
    

    private void Awake()
    {
        highScoreText.text = "High Score: " + GetHighScore().ToString();
    }
    public void StartGame()
    {
        gameStarted = true;
        FindFirstObjectByType<Road>().StartBuilding();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("picCounter", picCounter++);
            ScreenCapture.CaptureScreenshot("screenshot" + PlayerPrefs.GetInt("picCounter").ToString() + ".png");
        }
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "New High Score: " + score.ToString();
            notifyHighScore.text = "New High Score!";
        }
    }
    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
