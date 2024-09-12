using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AltitudeManager : MonoBehaviour
{
    public static AltitudeManager instance;

    [SerializeField] private int _startingAltitude;
    [SerializeField] private int _displayedAltitude;
    private float _currentAltitude;

    [SerializeField] private float _crashSpeed = 1f;
    [SerializeField] private float _riseSpeed = 1f;

    private bool _isFalling = false;
    private bool _isGoingUp = false;

    public float CurrentAltitude { get => _currentAltitude; private set => _currentAltitude = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de AltitudeManager dans la scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        CurrentAltitude = _startingAltitude;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartFalling();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartGoingUp();
        }
        if (_isFalling)
        {
            CurrentAltitude -= _crashSpeed * Time.deltaTime;
            if (CurrentAltitude <= 0)
            {
                CurrentAltitude = 0;
                _isFalling = false;
                GameManager.instance.ChangeGameState(2);
            }
        }
        else if (_isGoingUp)
        {
            CurrentAltitude += _riseSpeed * Time.deltaTime;
        }
        _displayedAltitude = Mathf.FloorToInt(CurrentAltitude);
    }

    public void StartFalling()
    {
        _isFalling = true;
        _isGoingUp = false;
    }

    public void StartGoingUp()
    {
        _isFalling = false;
        _isGoingUp = true;
    }
}
