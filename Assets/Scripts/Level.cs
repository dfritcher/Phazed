using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    #region Fields, Properties  

    [SerializeField]
    private AudioSource _levelMusic;
    public Vector2 StartLocation
    {
        get { return _levelStartPosition.transform.position; }
    }
    [SerializeField]
    private Transform _levelStartPosition = null;

    [SerializeField]
    private int _levelNumber = 0;

    public bool HasTutorial { get { return _hasTutorial;} }
    [SerializeField]
    private bool _hasTutorial = false;
   
    [SerializeField]
    private GameObject _tutorialContent = null;

    [SerializeField]
    private TutorialManager _tutorialManager = null;

    private List<PlatformBase> _whitePlatforms = new List<PlatformBase>();
    private List<PlatformBase> _blackPlatforms = new List<PlatformBase>();
    private List<PlatformBase> _grayPlatforms = new List<PlatformBase>();

    private List<SpikeBase> _whiteSpikes = new List<SpikeBase>();
    private List<SpikeBase> _blackSpikes = new List<SpikeBase>();
    private List<SpikeBase> _graySpikes = new List<SpikeBase>();

    private GameManager _gameManager = null;
    
    #endregion

    #region Methods
    #region Unity Hooks
    private void Awake()
    {
        _whitePlatforms.AddRange(GetComponentsInChildren<WhitePlatform>(true));
        _blackPlatforms.AddRange(GetComponentsInChildren<BlackPlatform>(true));      
        _grayPlatforms.AddRange(GetComponentsInChildren<GrayPlatform>(true));

        _whiteSpikes.AddRange(GetComponentsInChildren<WhiteSpike>(true));
        _blackSpikes.AddRange(GetComponentsInChildren<BlackSpike>(true));
        _graySpikes.AddRange(GetComponentsInChildren<GraySpike>(true));
    }

    #endregion

    #region Public Methods

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
        LevelDisplay.Instance.SetLevelDisplay(_levelNumber);
        TogglePlatforms(GameManager.CurrentPhase);
        ToggleSpikes(GameManager.CurrentPhase);
        if (HasTutorial)
            _tutorialManager.Setup(_tutorialContent, this);
        else{
            if (GameManager.PlayAudio)
                _levelMusic.Play();
            _gameManager.Level_InitializationComplete();
        }
    }

    public void TutorialClosed()
    {
        _tutorialContent?.gameObject.SetActive(false);
        _gameManager.Level_InitializationComplete();
        if (GameManager.PlayAudio)
            _levelMusic.Play();
    }
    public void PlayAudio()
    {
        if (GameManager.PlayAudio)
            _levelMusic.Play();
    }

    public void StopAudio()
    {
        _levelMusic.Stop();
    }

    public void ToggleSpikes(Phase phase)
    {
        switch (phase)
        {
            case Phase.Black:
                _blackSpikes.ForEach(s => s.Enable());
                _whiteSpikes.ForEach(s => s.Disable());
                break;
            case Phase.White:
                _whiteSpikes.ForEach(s => s.Enable());
                _blackSpikes.ForEach(s => s.Disable());
                break;
        }
    }

    public void TogglePlatforms(Phase phase)
    {
        switch (phase)
        {
            case Phase.Black:
                _whitePlatforms.ForEach(p => p.Disable());
                _blackPlatforms.ForEach(p => p.Enable());
                break;
            case Phase.White:
                _blackPlatforms.ForEach(p => p.Disable());
                _whitePlatforms.ForEach(p => p.Enable());                
                break;
        }
    }
    
    #endregion
    #endregion

    
}