using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState _gameState;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de GameManager dans la scene");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _gameState = GameState.Menu;
    }

    public void ChangeGameState(int sender)
    {
        GameState newState = (GameState)sender;

        switch (newState)
        {
            case GameState.Menu:
                _gameState = GameState.Menu;
                SceneManager.LoadScene(0);
                break;
            case GameState.Game:
                _gameState = GameState.Game;
                SceneManager.LoadScene(1);
                break;
            case GameState.Leaderboard:
                _gameState = GameState.Leaderboard;
                SceneManager.LoadScene(2);
                break;
        }
    }

    public enum GameState
    {
        Menu,
        Game,
        Leaderboard
    }
}
