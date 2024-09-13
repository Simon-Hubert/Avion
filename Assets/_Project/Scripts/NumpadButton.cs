using Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class NumpadButton : MonoBehaviour
    {
        private bool _isOn;
        private Sprite onSprite;
        private Sprite offSprite;
        private Sprite pressedSprite;
        private Image _image;

        public InputActionReference InputActionReference;

        public bool isOn
        {
            get => _isOn;
            set
            {
                _isOn = value;
                _image.sprite = _isOn ? onSprite : offSprite;
            }
        }

        void Start()
        {
            this._image = GetComponent<Image>();
            
            this.onSprite = Resources.Load<Sprite>("boutons_on");
            this.offSprite = Resources.Load<Sprite>("boutons_off");
            this.pressedSprite = Resources.Load<Sprite>("boutons_press");
        }

        public void PressDown()
        {
            _image.sprite = pressedSprite;
        }
        
        public void PressUp()
        {   
            if (_isOn)
            {
                SoundManager.Instance.PlaySoundType(SoundType.MiniGameSuccess);
                _isOn = false;
            }
            else
            {
                SoundManager.Instance.PlaySoundType(SoundType.MiniGameFailure);
            }
            _image.sprite = _isOn ? onSprite : offSprite;
        }
    }
}