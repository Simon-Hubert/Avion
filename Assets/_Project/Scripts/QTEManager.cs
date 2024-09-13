using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Xml.Xsl;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;

    [SerializeField] private float _qteDuration = 3f;

    [SerializeField] private Avion _avion;

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
                QTESuccess();
            } 
        }
    }

    private bool CheckKeyPress()
    {
        switch (_selectedKey)
        {
            case "enter":
                return _qteAction.triggered && Keyboard.current.enterKey.wasPressedThisFrame;
            case "left":
                return _qteAction.triggered && Keyboard.current.leftArrowKey.wasPressedThisFrame;
            case "right":
                return _qteAction.triggered && Keyboard.current.rightArrowKey.wasPressedThisFrame;
            // Ajouter d'autres cas si nécessaire
            default:
                return false;
        }
    }

    public void QTESuccess()
    {
        _qteCompleted = true;
        _qteActive = false;
        Debug.Log("QTE Success!");
        FeedbackManager.instance.Success();
        _avion.Stabilize();
        StartCoroutine(AltitudeManager.instance.UpAltitudeCoroutine());
    }

    public void QTEFailure()
    {
        _qteCompleted = true;
        _qteActive = false;
        Debug.Log("QTE Failed!");
        FeedbackManager.instance.Failure();
        _avion.Stabilize();
        StartCoroutine(AltitudeManager.instance.DownAltitudeCoroutine());
    }

    private IEnumerator QTETimer()
    {
        yield return new WaitForSeconds(_qteDuration);
        if (!_qteCompleted)
        {
            QTEFailure();
        }
    }

    public void StartQTEWithKey(string keyToPress)
    {

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
                _avion.Roll(false);
                _selectedKey = "right";
                break;
            case 1:
                //on penche sur la droite
                _avion.Roll(true);
                _selectedKey = "left";
                break;
        }
        StartCoroutine(QTETimer());
    }
}
