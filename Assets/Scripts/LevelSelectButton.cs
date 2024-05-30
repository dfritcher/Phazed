using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Assets.Scripts
{
    public class LevelSelectButton : MonoBehaviour
    {
        #region Fields, Properties
        [SerializeField]
        private Image _levelButtonImage = null;
        [SerializeField]
        private CanvasGroup _levelButton = null;
        [SerializeField]
        private CanvasGroup _lockedCanvasGroup = null;
        [SerializeField]
        private CanvasGroup _levelCanvasGroup = null;
        [SerializeField]
        private TextMeshProUGUI _levelNumberDisplay = null;

        public int LevelNumber { get { return _levelNumber; } }
        private int _levelNumber;
        private bool _isUnLocked;
        #endregion Fields, Properties (end)

        #region Delegates, Events
        public delegate void LevelSelectEvent(LevelSelectButton levelSelectButton);
        public event LevelSelectEvent OnLevelClickedCallback;
        #endregion Delegates, Events (end)

        #region Methods
        public void Setup(int levelNumber, int lastLevelUnlocked)
        {
            gameObject.SetActive(true);
            _levelNumber = levelNumber;
            var tempColor = _levelButtonImage.color;
            
            if (GameManager.IsDemo)
            {
                _isUnLocked = _levelNumber <= lastLevelUnlocked && GameManager.LevelActiveInDemo(_levelNumber);
                tempColor.a = GameManager.LevelActiveInDemo(_levelNumber) ? 1f : .25f;
            }
            else
            {
                _isUnLocked = _levelNumber <= lastLevelUnlocked;
                tempColor.a = 1f;                             
            }

            _levelButtonImage.color = tempColor;
            SetDisplay();
        }

        public void OnLevelClicked()
        {
            if (_isUnLocked)
                OnLevelClickedCallback?.Invoke(this);
        }

        private void SetDisplay()
        {
            if (_isUnLocked)
            {
                _lockedCanvasGroup.alpha = 0;
                _levelCanvasGroup.alpha = 1;
                _levelNumberDisplay.text = _levelNumber.ToString();
            }
            else
            {
                _lockedCanvasGroup.alpha = 1;
                _levelCanvasGroup.alpha = 0;
            }

            _levelButton.alpha = _isUnLocked ? 1f : .75f;
        }
        #endregion Methods (end)
    }
}