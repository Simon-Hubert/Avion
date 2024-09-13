using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    [SerializeField] private int _timeBeforeNewGame;
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de LeaderBoardManager dans la scene");
            return;
        }
        instance = this;

        text.SetText("Your Score : " + GameManager.instance._score.ToString() + " m");
    }
    
    public void LaunchGame()
    {
        GameManager.instance.ChangeGameState(1);
    }
}
