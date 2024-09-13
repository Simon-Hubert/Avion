using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using NaughtyAttributes;
using Sounds;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager instance;


    [SerializeField] float duration = 0.5f;
    PostProcessVolume volume;
    Vignette vignette;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("plus d'une instance de FeedbackManager dans la scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.smoothness.Override(0.436f);
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
    }

    [Button]
    public void Success()
    {
        vignette.enabled.Override(true);
        vignette.color.Override(Color.green);
        StartCoroutine(Reset());
        SoundManager.Instance.PlaySoundType(SoundType.MiniGameSuccess);
    }

    [Button]
    public void Failure()
    {
        vignette.enabled.Override(true);
        vignette.color.Override(Color.red);
        StartCoroutine(Reset());
        SoundManager.Instance.PlaySoundType(SoundType.MiniGameFailure);
    }
     
    IEnumerator Reset()
    {
        float t = 0f;
        while(t < duration)
        {
            vignette.intensity.Override(Mathf.Lerp(0.48f, 0, t/duration));
            t += Time.deltaTime;
            yield return 0;
        }
        vignette.enabled.Override(false);
        EventManager.instance._canLaunchEvent = true;
    }
}
