using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private InputAction _qteAction;
    private Controls _playerInputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de EventManager dans la scene");
            return;
        }
        instance = this;
        _playerInputActions = new Controls();
    }

    private void OnEnable()
    {
        _playerInputActions.QTE.Enable();
        _playerInputActions.QTE.StartEvents.performed += OnNumericKeyPressed;
    }

    private void OnDisable()
    {
        _playerInputActions.QTE.Disable();
        _playerInputActions.QTE.StartEvents.performed -= OnNumericKeyPressed;
    }

    private void OnNumericKeyPressed(InputAction.CallbackContext context)
    {
        var keyPressed = context.control.name;
        TriggerEvent(int.Parse(keyPressed));
    }

    private void TriggerEvent(int eventNumber)
    {
        Debug.Log("Événement " + eventNumber + " déclenché");
        switch (eventNumber)
        {
            case 0:
                //Evenement IRL
                StartQTE("buttonSouth");
                break;
            case 1:
                //Gyroscope
                StartQTEGyroscope();
                break;
            case 2:
                //BlueRed
                StartBlueRedQTE();
                break;
            case 3:
                StartQTE("buttonWest");
                break;
            default:
                Debug.LogWarning("Aucun événement assigné à cette touche.");
                break;
        }
    }

    private void StartQTE(string keyToPress)
    {
        QTEManager.instance.StartQTEWithKey(keyToPress);
    }

    private void StartQTEGyroscope()
    {
        int randomValue = Random.Range(0, 2);
        QTEManager.instance.StartQTEGyroscope(randomValue);
    }

    private void StartBlueRedQTE()
    {
        int randomValue = Random.Range(0, 2);
        QTEManager.instance.StartBlueRedQTE(randomValue);
    }

}
