using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Canvas _tutorialDisplay = null;

    private Level _level = null;

    public void Setup(GameObject content, Level level)
    {
        _level = level;
        _tutorialDisplay.gameObject.SetActive(true);
        content.gameObject.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorialDisplay.gameObject.SetActive(false);
        _level?.TutorialClosed();
    }
}
