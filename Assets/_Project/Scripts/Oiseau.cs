using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Sounds;
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
        dist.SetText(Mathf.FloorToInt(distance/1609).ToString() + " mi");
        SoundManager.Instance.PlaySoundType(SoundType.SeagullScream);
    }

    [Button]
    public void TestAnim()
    {
        PlayAnimation(100);
    }
}
