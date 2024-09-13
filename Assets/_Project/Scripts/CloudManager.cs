using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] GameObject cloud;
    [SerializeField] int nbClouds;
    [SerializeField] AnimationCurve densityFunction;

    private void Start()
    {
        for(int i=0; i < nbClouds; i++)
        {
            float x, y, z, w;
            x = Random.Range(-100f, 100f);
            z = Random.Range(0f, 1000f);
            w = Random.Range(0f, 1f);
            y = 120*densityFunction.Evaluate(w) + 40;
            GameObject.Instantiate(cloud, new Vector3(x,y,z), Quaternion.identity);
        }
    }
}
