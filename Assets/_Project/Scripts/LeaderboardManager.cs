using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    [SerializeField] private int _timeBeforeNewGame;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de LeaderBoardManager dans la scene");
            return;
        }
        instance = this;
    }
    
    public void LaunchGame()
    {
        GameManager.instance.ChangeGameState(1);
    }
}
