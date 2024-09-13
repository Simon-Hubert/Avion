using Sounds;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState _gameState;

    public int _score;

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

    public IEnumerator LaunchLeaderBoard(GameObject blackScreen)
    {
        SoundManager.Instance.StopSoundType(SoundType.AlertCrash);
        SoundManager.Instance.PlaySoundType(SoundType.CrashExplode);
        _score = DistanceManager.instance._distance;
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        ChangeGameState(2);
    }

    public enum GameState
    {
        Menu,
        Game,
        Leaderboard
    }
}
