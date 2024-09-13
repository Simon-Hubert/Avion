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
        private Sprite pressedOffSprite;
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
            this.pressedOffSprite = Resources.Load<Sprite>("boutons_press_off");
        }

        public void PressDown()
        {
            _image.sprite = _isOn ? pressedSprite : pressedOffSprite;
        }
        
        public void PressUp()
        {   
            if (_isOn)
            {
                //SUCCESS
                FeedbackManager.instance.Success();
                StartCoroutine(AltitudeManager.instance.UpAltitudeCoroutine());
                _isOn = false;
            }
            else
            {
                if (NumpadManager.Instance.IsThereAButtonTurnedOn())
                {
                    //FAILED
                    FeedbackManager.instance.Failure();
                    StartCoroutine(AltitudeManager.instance.DownAltitudeCoroutine());
                    NumpadManager.Instance.TurnOffButton();
                }
            }
            _image.sprite = _isOn ? onSprite : offSprite;
        }
    }
}