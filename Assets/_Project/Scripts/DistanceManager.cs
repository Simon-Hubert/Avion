using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public static DistanceManager instance;

    [SerializeField] private int _distance;

    [SerializeField] private float _speed = 1f;

    private float _elapsedTime = 0f;

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
    }
}
