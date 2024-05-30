using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleAnimationController : MonoBehaviour
{
    #region Fields, Properties

    [SerializeField]
    private TextMeshProUGUI[] _titleLetters = null;
    [SerializeField]
    private float _letterDelay = .1f;

    [SerializeField]
    private float _duration = .9f;

    private bool _canStartTitleAnimation = false;
    private int _iterator = 0;
    #endregion Fields, Properties

    #region Methods
    #region Unity Hooks

    private void Awake()
    {
        _canStartTitleAnimation = true;
    }

    private void Update()
    {
        if (_canStartTitleAnimation)
        {
            _canStartTitleAnimation = false;
            StartCoroutine(AnimateTitle());
        }
            
    }

    private void OnEnable()
    {
        _canStartTitleAnimation = true;
    }

    private void OnDisable()
    {
        _canStartTitleAnimation = false;

    }

    public IEnumerator AnimateTitle()
    {
        yield return null;
        _iterator = 0;
        do
        {
            StartCoroutine(AnimateUtility.AnimateGraphicColor(_titleLetters[_iterator], Color.black, _duration,
                _letterDelay, null));
            _iterator++;
            yield return new WaitForSeconds(_letterDelay);

        } while (_iterator < _titleLetters.Length);

        //yield return new WaitForSeconds(0.2f);
        _iterator = 0;
        do
        {
            StartCoroutine(_iterator == _titleLetters.Length - 1
                ? AnimateUtility.AnimateGraphicColor(_titleLetters[_iterator], Color.white, _duration, _letterDelay,
                    AnimationCallback)
                : AnimateUtility.AnimateGraphicColor(_titleLetters[_iterator], Color.white, _duration, _letterDelay,
                    null));
            _iterator++;
            yield return new WaitForSeconds(_letterDelay);

        } while (_iterator < _titleLetters.Length);

        yield return null;        
    }


    public void AnimationCallback()
    {
        _canStartTitleAnimation = true;
    }
    #endregion Unity Hooks (end)
    #endregion Methods (end)
}