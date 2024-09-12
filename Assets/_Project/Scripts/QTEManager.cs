using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Xml.Xsl;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;

    [SerializeField] private TextMeshProUGUI _qteText;
    [SerializeField] private float _qteDuration = 3f;
    [SerializeField] private GameObject _successUI;
    [SerializeField] private GameObject _failureUI;

    private InputAction _qteAction;
    //private string[] _possibleKeys = { "a", "w", "e", "space", "up", "down", "left", "right", "buttonSouth", "buttonEast", "dpad/up" };  // Les touches possibles (clavier et manette)
    //private string[] _possibleKeys = {"buttonSouth", "buttonEast"};  // Les touches possibles (clavier et manette)
    private string _selectedKey;

    private bool _qteActive = false;
    private bool _qteCompleted = false;

    private Controls _playerInputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de QTEManager dans la scene");
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
        if (_qteActive && !_qteCompleted)
        {
            if (CheckKeyPress())
            {
                StartCoroutine(QTESuccess());
            } 
        }
    }

    private bool CheckKeyPress()
    {
        switch (_selectedKey)
        {
            case "buttonNorth":
                return _qteAction.triggered && Gamepad.current.buttonNorth.wasPressedThisFrame;
            case "buttonWest":
                return _qteAction.triggered && Gamepad.current.buttonWest.wasPressedThisFrame;
            case "buttonSouth":
                return _qteAction.triggered && Gamepad.current.buttonSouth.wasPressedThisFrame;
            case "buttonEast":
                return _qteAction.triggered && Gamepad.current.buttonEast.wasPressedThisFrame;
            case "left":
                return _qteAction.triggered && Keyboard.current.leftArrowKey.wasPressedThisFrame;
            case "right":
                return _qteAction.triggered && Keyboard.current.rightArrowKey.wasPressedThisFrame;
            case "b":
                return _qteAction.triggered && Keyboard.current.bKey.wasPressedThisFrame;
            case "r":
                return _qteAction.triggered && Keyboard.current.rKey.wasPressedThisFrame;
            // Ajouter d'autres cas si nécessaire
            default:
                return false;
        }
    }

    private IEnumerator QTESuccess()
    {
        _qteCompleted = true;
        _qteActive = false;
        _successUI.SetActive(true);
        _failureUI.SetActive(false);
        Debug.Log("QTE Success!");
        yield return new WaitForSeconds(3f);
        _successUI.SetActive(false);
        _qteText.text = "";
    }

    private IEnumerator QTEFailure()
    {
        _qteCompleted = true;
        _qteActive = false;
        _successUI.SetActive(false);
        _failureUI.SetActive(true);
        Debug.Log("QTE Failed!");
        yield return new WaitForSeconds(3f);
        _failureUI.SetActive(false);
        _qteText.text = "";
    }

    private IEnumerator QTETimer()
    {
        yield return new WaitForSeconds(_qteDuration);
        if (!_qteCompleted)
        {
            StartCoroutine(QTEFailure());
        }
    }

    public void StartQTEWithKey(string keyToPress)
    {
        _qteText.text = "Press " + keyToPress.ToUpper() + "!";

        _qteCompleted = false;
        _qteActive = true;

        _selectedKey = keyToPress;

        StartCoroutine(QTETimer());
    }

    public void StartQTEGyroscope(int randomValue)
    {
        _qteCompleted = false;
        _qteActive = true;
        switch (randomValue)
        {
            case 0:
                //on penche sur la gauche
                _qteText.text = "On penche sur la gauche!";
                _selectedKey = "right";
                break;
            case 1:
                //on penche sur la droite
                _qteText.text = "On penche sur la droite!";
                _selectedKey = "left";
                break;
        }
        StartCoroutine(QTETimer());
    }

    public void StartBlueRedQTE(int randomValue)
    {
        _qteCompleted = false;
        _qteActive = true;
        switch (randomValue)
        {
            case 0:
                //blue
                _qteText.text = "Bleu";
                _selectedKey = "b";
                break;
            case 1:
                //red
                _qteText.text = "Rouge";
                _selectedKey = "r";
                break;
        }
        StartCoroutine(QTETimer());
    }
}
