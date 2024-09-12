using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AltitudeManager : MonoBehaviour
{
    public static AltitudeManager instance;

    [SerializeField] private int _startingAltitude;
    [SerializeField] private int _displayedAltitude;
    private float _currentAltitude;

    [SerializeField] private float _crashSpeed = 1f;
    [SerializeField] private float _riseSpeed = 1f;

    [SerializeField] private TextMeshProUGUI _altitudeText;

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
        StartFalling();
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
        CurrentAltitude = Mathf.Min( CurrentAltitude, _startingAltitude );
        _displayedAltitude = Mathf.FloorToInt(CurrentAltitude);
        DisplayAltitude(_displayedAltitude);
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

    public IEnumerator UpAltitudeCoroutine()
    {
        StartGoingUp();
        yield return new WaitForSeconds(3f);
        StartFalling();
    }    
 
    private void DisplayAltitude(int displayedAltitude)
    {
        _altitudeText.text = displayedAltitude.ToString() + "m";
    }
}
