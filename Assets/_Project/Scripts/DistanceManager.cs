using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public static DistanceManager instance;

    public int _distance;
    [SerializeField] private float _speed = 1f;
    private int _threshold = 1;

    private float _elapsedTime = 0f;

    public event Action<int> onThresholdPass;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de DistanceManager dans la scene");
            return;
        }
        instance = this;
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        _distance = Mathf.FloorToInt(_elapsedTime * _speed);
        if(_distance >= 5000 * _threshold)
        {
            _threshold++;
            onThresholdPass.Invoke(_distance);
        }
    }
}
