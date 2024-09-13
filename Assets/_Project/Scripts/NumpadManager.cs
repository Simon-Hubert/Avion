using System;
using System.Collections.Generic;
using _Project.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class NumpadManager : MonoBehaviour
{
    public static NumpadManager Instance { get; private set; }
    
    public Key specialKey = Key.LeftCtrl;
    public NumpadButton[] numpadButtons = Array.Empty<NumpadButton>();

    private bool _isPressing = false;
    private NumpadButton _currentButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("plus d'une instance de NumpadManager dans la scene");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        numpadButtons = GetComponentsInChildren<NumpadButton>();

        foreach (NumpadButton numpadButton in numpadButtons)
        {
            numpadButton.InputActionReference.action.Enable();
            numpadButton.InputActionReference.action.performed += (context) =>
            {
                numpadButton.PressDown();
            };
            numpadButton.InputActionReference.action.canceled += (context) =>
            {
                numpadButton.PressUp();
            };
        }
    }

    private void OnDisable()
    {
        foreach (NumpadButton numpadButton in numpadButtons)
        {
            numpadButton.InputActionReference.action.Disable();
        }
    }

    public void ChooseButton()
    {
        NumpadButton randomButton = numpadButtons[Random.Range(0, numpadButtons.Length)];
        randomButton.isOn = true;
        Invoke(nameof(TooManyTime), 10f);
    }

    public bool IsThereAButtonTurnedOn()
    {
        bool result = false;
        foreach (NumpadButton numpadButton in numpadButtons)
        {
            if (numpadButton.isOn)
            {
                result = true;
            }
        }

        return result;
    }

    public void TurnOffButton()
    {
        foreach (NumpadButton numpadButton in numpadButtons)
        {
            numpadButton.isOn = false;
        }
    }

    private void TooManyTime()
    {
        if (IsThereAButtonTurnedOn())
        {
            FeedbackManager.instance.Failure();
            StartCoroutine(AltitudeManager.instance.DownAltitudeCoroutine());
            NumpadManager.Instance.TurnOffButton();
        }
    }
}
