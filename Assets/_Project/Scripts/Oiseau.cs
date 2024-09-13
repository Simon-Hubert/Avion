using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class Oiseau : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text dist;
    
    private void OnDisable()
    {
        DistanceManager.instance.onThresholdPass -= PlayAnimation;
    }

    private void Start()
    {
        DistanceManager.instance.onThresholdPass += PlayAnimation;
        animator.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int distance)
    {
        animator.enabled = true;
        animator.SetTrigger("Oiseau");
        dist.SetText(Mathf.FloorToInt(distance/1000).ToString() + " km");
    }

    [Button]
    public void TestAnim()
    {
        PlayAnimation(100);
    }
}
