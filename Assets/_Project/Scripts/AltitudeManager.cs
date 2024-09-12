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
        _currentAltitude = _startingAltitude;
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
            _currentAltitude -= _crashSpeed * Time.deltaTime;
            if (_currentAltitude <= 0)
            {
                _currentAltitude = 0;
                _isFalling = false;
                GameManager.instance.ChangeGameState(2);
            }
        }
        else if (_isGoingUp)
        {
            _currentAltitude += _riseSpeed * Time.deltaTime;
        }
        _displayedAltitude = Mathf.FloorToInt(_currentAltitude);
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
