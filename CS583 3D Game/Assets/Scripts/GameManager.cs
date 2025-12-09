using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    private int enemyScore = 0;
    private int playerScore = 0;
 
    public TextMeshProUGUI displayedEnemyScore;
    public TextMeshProUGUI displayedPlayerScore;
    public TextMeshProUGUI winTest;

    private void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        enemyScore = 0;
        playerScore = 0;
        displayedEnemyScore.text = "Enemy Score: " + enemyScore.ToString();
        displayedPlayerScore.text = "Player Score: " + playerScore.ToString();
        winTest.text = "";
        SwitchState(GameState.Playing);
    }
    void SwitchState(GameState newState)
    {
        State = newState;
    }
    public void ScoreUpdate(string type)
    {
        switch(type)
        {
            case "Enemy":
                playerScore += 10;
                displayedPlayerScore.text = "Player Score: " + playerScore.ToString();
                break;
            case "Player":
                enemyScore += 10;
                displayedEnemyScore.text = "Enemy Score: " + enemyScore.ToString();
                break;
        }
        WinLoseCheck();
    }
    void WinLoseCheck()
    {
        if(enemyScore >= 30)
        {
            Debug.Log("You Lose!");
            SwitchState(GameState.Loss);
            winTest.text = "You Lose!";

        }
        else if(playerScore >= 30)
        {
            Debug.Log("You Win!");
            SwitchState(GameState.Win);
            winTest.text = "You Win!";
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        Win,
        Loss
    }

}