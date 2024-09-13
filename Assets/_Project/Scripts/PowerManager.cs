using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerManager : MonoBehaviour
{

    public static PowerManager instance;

    [SerializeField] private int _currentPowerLevel;
    [SerializeField] private TextMeshProUGUI _requiredPowerLevel;
    [SerializeField] private List<GameObject> _interrupteurSprites;
    [SerializeField] private int _powerGameTime;
    private bool _isGameActive = false;

    private string _selectedKey;
    private InputAction _qteAction;
    private Controls _playerInputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de PowerManager dans la scene");
            return;
        }
        instance = this;
        _playerInputActions = new Controls();
    }

    private void OnEnable()
    {
        _qteAction = _playerInputActions.QTE.QTEButton;
        _qteAction.Enable();
    }

    private void OnDisable()
    {
        _qteAction.Disable();
    }

    private void Update()
    {
        if (_isGameActive)
        {
            if (CheckKeyPress())
            {
                switch (_selectedKey)
                {
                    case "up":
                        IncreasePowerLevel();
                        break;
                    case "down":
                        DecreasePowerLevel();
                        break;
                }
            }
        }
    }

    private bool CheckKeyPress()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _selectedKey = "up";
            return true;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _selectedKey = "down";
            return true;
        }
        return false;
    }

    private void IncreasePowerLevel()
    {
        if(_currentPowerLevel < 4)
        {
            _currentPowerLevel += 1;
            UpdatePowerLevelVisual();
        }
    }

    private void DecreasePowerLevel()
    {
        if (_currentPowerLevel > 0)
        {
            _currentPowerLevel -= 1;
            UpdatePowerLevelVisual();
        }
    }

    private void UpdatePowerLevelVisual()
    {
        for(int i = 0; i<_interrupteurSprites.Count; i++)
        {
            _interrupteurSprites[i].SetActive(false);
            if (_currentPowerLevel == i)
            {
                _interrupteurSprites[i].SetActive(true);
            }
        }
    }
    public void StartPowerGame(int requiredPowerLevel)
    {
        DisplayRequiredPowerLevel(requiredPowerLevel);
        _isGameActive = true;
        StartCoroutine(PlayAndEndGame(requiredPowerLevel));
    }

    private void DisplayRequiredPowerLevel(int requiredPowerLevel)
    {
        _requiredPowerLevel.text = requiredPowerLevel.ToString();
    }

    private IEnumerator PlayAndEndGame(int requiredPowerLevel)
    {
        yield return new WaitForSeconds(_powerGameTime);
        if (_currentPowerLevel == requiredPowerLevel)
        {
            QTEManager.instance.QTESuccess();
        }
        else
        {
            QTEManager.instance.QTEFailure();
        }
        ResetRequiredPowerLevel();
        _isGameActive = false;
    }

    private void ResetRequiredPowerLevel()
    {
        _requiredPowerLevel.text = "";
    }
}
