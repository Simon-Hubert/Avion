using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using Sounds;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class WindShield : MonoBehaviour
{
    private Image _dirt;
    [SerializeField] private Animator _wipersAnimator1;
    [SerializeField] private Animator _wipersAnimator2;
    [SerializeField] private float _amount = 0;
    [SerializeField] private float _speed;
    [SerializeField] private float _threshold1;
    [SerializeField] private float _threshold2;
    [SerializeField] private float _threshold3;
    [SerializeField] private Sprite _dirtLevel1;
    [SerializeField] private Sprite _dirtLevel2;
    [SerializeField] private Sprite _dirtLevel3;

    private bool wiping = false;

    private void Start() {
        _dirt = GetComponent<Image>();
    }

    private void FixedUpdate() {
         
        if(!wiping) _amount += _speed * Time.fixedDeltaTime;

        if (_amount < _threshold1) {
            _dirt.enabled = false;
            return;
        }

        if (_amount < _threshold2) {
            _dirt.enabled = true;
            _dirt.sprite = _dirtLevel1;
            return;
        }
        
        if (_amount < _threshold3) {
            _dirt.enabled = true;
            _dirt.sprite = _dirtLevel2;
            return;
        }
        
        _dirt.enabled = true;
        _dirt.sprite = _dirtLevel3;
    }

    private void Update() {
        if (Keyboard.current.mKey.wasPressedThisFrame) {
            
            _wipersAnimator1.enabled = true;
            _wipersAnimator2.enabled = true;
            _wipersAnimator1.SetTrigger("Wipe");
            _wipersAnimator2.SetTrigger("Wipe");
            _amount = Mathf.Min(80, _amount);
            wiping = true;
            StartCoroutine(Wipe());
        }
    }

    IEnumerator Wipe() {
        float t = 0f;
        float startAmount = _amount;
        SoundManager.Instance.PlaySoundType(SoundType.WindshieldWiper);
        while (t < 3) {
            _amount = Mathf.Lerp(startAmount, 0, t/3);
            t += Time.deltaTime;
            yield return 0;
        }
        _amount = 0;
        wiping = false;
    }
}
