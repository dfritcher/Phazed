using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{

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

    public delegate void LevelSelectEvent(LevelSelectButton levelSelectButton);
    public event LevelSelectEvent OnLevelClickedCallback;

    public void Setup(int levelNumber, int lastLevelUnlocked)
    {
        gameObject.SetActive(true);
        _levelNumber = levelNumber;
        _isUnLocked = _levelNumber <= lastLevelUnlocked;

        SetDisplay();
    }

    public void OnLevelClicked()
    {
        if(_isUnLocked)
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
}
