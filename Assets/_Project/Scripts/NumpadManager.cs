using System;
using System.Collections.Generic;
using _Project.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class NumpadManager : MonoBehaviour
{
    public Key specialKey = Key.LeftCtrl;
    public NumpadButton[] numpadButtons = Array.Empty<NumpadButton>();

    private bool _isPressing = false;
    private NumpadButton _currentButton;
    
    void Start()
    {
        numpadButtons = GetComponentsInChildren<NumpadButton>();
        ChooseButton();

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
    }
}
