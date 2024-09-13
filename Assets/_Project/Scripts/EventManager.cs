using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private InputAction _qteAction;
    private Controls _playerInputActions;

    public bool _canLaunchEvent = true;

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
        if (!_canLaunchEvent) return;
        TriggerEvent(int.Parse(keyPressed));
    }

    private void TriggerEvent(int eventNumber)
    {
        _canLaunchEvent = false;
        Debug.Log("�v�nement " + eventNumber + " d�clench�");
        switch (eventNumber)
        {
            case 0:
                //Evenement IRL
                StartQTE("enter");
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
                StartPowerLevelQTE();
                break;
            case 4:
                StartNumpad();
                break;
            default:
                Debug.LogWarning("Aucun �v�nement assign� � cette touche.");
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
        List<int> myList = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int randomValue = Random.Range(1, 3);
            myList.Add(randomValue);
        }
        LightsManager.instance.StartLightsGame(myList);
        //QTEManager.instance.StartBlueRedQTE(randomValue);
    }

    private void StartPowerLevelQTE()
    {
        int randomValue = Random.Range(0, 5);
        PowerManager.instance.StartPowerGame(randomValue);
    }

    private void StartNumpad()
    {
        NumpadManager.Instance.ChooseButton();
    }
}
