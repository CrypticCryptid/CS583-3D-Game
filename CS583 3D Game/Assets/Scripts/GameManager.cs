using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int enemyScore = 0;
    private int playerScore = 0;

    public int winScore;
 
    public TextMeshProUGUI displayedEnemyScore;
    public TextMeshProUGUI displayedPlayerScore;
    public TextMeshProUGUI winTest;

    public GameObject gameOverPanel;

    public static bool GameIsOver = false;

    private void Awake()
    {
        Instance = this;
        GameIsOver = false;
    }

    public void ScoreUpdate(string type, int val)
    {
        switch(type)
        {
            case "Enemy":
                playerScore += val;
                displayedPlayerScore.text = "Player Score: " + playerScore.ToString();
                break;
            case "Player":
                enemyScore += val;
                displayedEnemyScore.text = "Enemy Score: " + enemyScore.ToString();
                break;
        }
        WinLoseCheck();
    }

    void WinLoseCheck()
    {
        if(enemyScore >= winScore)
        {
            GameIsOver = true;

            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            winTest.text = "You Lose!";
        }
        else if(playerScore >= winScore)
        {
            GameIsOver = true;
            
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            winTest.text = "You Win!";
        }
    }
}