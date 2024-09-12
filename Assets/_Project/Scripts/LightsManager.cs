using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightsManager : MonoBehaviour
{
    public static LightsManager instance;

    [SerializeField] private int _lightGameTime;
    [SerializeField] private List<Image> _lights;
    [SerializeField] List<int> _lightsStates = new List<int> { 0, 0, 0, 0 };
    [SerializeField] private List<Sprite> _lightSprites;
    [SerializeField] private List<int> _playerList = new List<int>();
    private bool _isGameActive = false;

    private string _selectedKey;
    private InputAction _qteAction;
    private Controls _playerInputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de LightsManager dans la scene");
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
            if (CheckKeyPress() && _playerList.Count <= _lights.Count)
            {
                switch (_selectedKey)
                {
                    case "b":
                        _playerList.Add(1);
                        break;
                    case "r":
                        _playerList.Add(2);
                        break; ;
                }
            }
        }
    }

    private bool CheckKeyPress()
    {
        // Vérifie si la touche 'b' ou 'r' a été enfoncée cette frame
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            _selectedKey = "b";
            return true;
        }
        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _selectedKey = "r";
            return true;
        }
        return false;
    }

    private void ChangeLightSprite(int lightIndex, int colorIndex)
    {
        _lightsStates[lightIndex] = colorIndex;
        _lights[lightIndex].sprite = _lightSprites[colorIndex];
    }

    public void StartLightsGame(List<int> requiredList)
    {
        for(int i = 0; i<requiredList.Count; i++)
        {
            ChangeLightSprite(i, requiredList[i]);
        }
        _isGameActive = true;
        StartCoroutine(PlayAndEndGame(requiredList));
    }

    private IEnumerator PlayAndEndGame(List<int> requiredList)
    {
        yield return new WaitForSeconds(_lightGameTime);
        if(AreListsEqual(requiredList, _playerList))
        {
            QTEManager.instance.QTESuccess();
        }
        else
        {
            QTEManager.instance.QTEFailure();
        }
        ResetLights();
        _playerList.Clear();
        _isGameActive = false;
    }

    bool AreListsEqual(List<int> list1, List<int> list2)
    {
        // Vérifier si les tailles sont différentes
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Comparer les éléments un par un
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }

        return true;
    }

    private void ResetLights()
    {
        for (int i = 0; i < _lights.Count ; i++)
        {
            ChangeLightSprite(i, 0);
        }
    }

}
