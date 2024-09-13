using System;
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
    }

    private void Start()
    {
        text.SetText("Your Score : " + Mathf.FloorToInt(GameManager.instance._score/1609).ToString() + " miles");
    }

    public void LaunchGame()
    {
        GameManager.instance.ChangeGameState(1);
    }
}
