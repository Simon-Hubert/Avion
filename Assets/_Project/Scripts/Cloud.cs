using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class Cloud : MonoBehaviour
{
    [SerializeField] Transform avion;

    [SerializeField, MinMaxSlider(30.0f, 70.0f)] private Vector2 xzRange;
    [SerializeField, MinMaxSlider(15.0f, 25.0f)] private Vector2 yRange;


    private void FixedUpdate()
    {
        if(transform.position.z < avion.position.z)
        {
            ResetCloud();
        }
    }

    [Button]
    public void ResetCloud()
    {
        transform.position += new Vector3(0f, 0f, 1000f);
        transform.localScale.Set(Random.Range(xzRange.x, xzRange.y),
                                 Random.Range(yRange.x, yRange.y),
                                 Random.Range(xzRange.x, xzRange.y));
    }

}
