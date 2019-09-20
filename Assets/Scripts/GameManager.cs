using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {
        #region Fields, Properties
        [Header("Scene References")]
        [SerializeField]
        private GameObject _titleCanvas = null;
        [SerializeField]
        private GameObject _levelSelectCanvas = null;
        [SerializeField]
        private GameObject _gameScene = null;
        [SerializeField]
        private GameObject _levelEndedCanvas = null;

        [SerializeField]
        private GameObject _shortCutDeathCanvas = null;

        [SerializeField]
        private Camera2DFollow _cameraFollow = null;

        [SerializeField]
        private TextMeshProUGUI _appVersion = null;
            
        [Header("Player Related"), Space(8)]
        [SerializeField]
        private PlayerController _player = null;

        public static Phase CurrentPhase { get{ return _instance._player.CurrentPhase; } } 

        [Header("Level Related"), Space(8)]
        [SerializeField]
        private Level[] _levels = null;
        [SerializeField]
        private Transform _levelSelectParent = null;
        [SerializeField]
        private GameObject _levelSelectPrefab = null;

        [SerializeField]
        private GameObject _levelFailedScreen = null;
        [SerializeField]
        private GameObject _levelSuccededScreen = null;

        [SerializeField]
        private AudioSource _dyingSound = null;

        [Header("Level Related"), Space(8)]
        [SerializeField]
        private GameObject _phaseOverlay = null;

        private List<LevelSelectButton> _levelSelectPool = new List<LevelSelectButton>();
        static private GameManager _instance;
        static public GameManager Instance { get { return _instance; } }
        static private WaitForSeconds _restartTime = new WaitForSeconds(5f);
        private static int _currentLevel;
        private bool _phasedDisabled = false;
        static public float PhasedDisabledTime = 2.5f;
        public float _currentPhasedDisabledTime;

        public static bool PlayAudio
        {
            get { return _saveData.IsSoundOn; }
        }
        public static Difficulty Difficulty
        {
            get { return _saveData?.Difficulty ?? Difficulty.Normal; }
        }
        private static SaveData _saveData = null;
       
        #endregion
        
        #region Methods
        #region Unity Hooks

        private void Awake()
        {
            _saveData = FileIOWrapper.LoadGameFromLocalStore();
            _saveData.LastLevelUnlocked = 24;            
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            ResetDisabledPhaseOverlay();
            ColliderManager.ResetColliderPositions();
            _player.gameObject.SetActive(false);
            _player.OnPlayerDied += Player_OnPlayerDied;
            _player.OnLevelComplete += Player_OnLevelComplete;
            _currentLevel = 1;
            PhasedDisabledTime = 2.5f;
            _appVersion.text = $"V - {Application.version}";
        }

        private void FixedUpdate()
        {
            if (_phasedDisabled){
                if (_currentPhasedDisabledTime >= 0)
                {
                    _currentPhasedDisabledTime -= Time.fixedDeltaTime;
                }
                else
                {
                    _phaseOverlay.gameObject.SetActive(false);
                    _phasedDisabled = false;
                }
            }            
        }
        #endregion

        #region Public Methods

        #region Methods Called From Unity
        public void OnLoadNextLevelClicked()
        {
            _cameraFollow.ResetCameraPosition();
            _levelEndedCanvas.SetActive(false);
            _gameScene.SetActive(true);
            LevelComplete();
            ColliderManager.ResetColliderPositions();
        }

        public void OnPlayClicked()
        {
            ColliderManager.ResetColliderPositions();
            _titleCanvas.SetActive(false);
            _levelSelectCanvas.SetActive(true);

            GenerateLevelSelectButtons();
        }

        public void OnRetryLevelClicked()
        {
            ColliderManager.ResetColliderPositions();
            _cameraFollow.ResetCameraPosition();
            RestartLevel();
            ReturnAllToPool();            
        }

        public void OnShowLevelSelectClicked()
        {
            ReturnAllToPool();
            ShowLevelSelectScreen();
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        public void OnReturnHomeClicked()
        {
            ReturnAllToPool();
        }
        #endregion

        #region Public Methods
        static public void UpdateSettings(bool playAudio, Difficulty difficulty)
        {
            _saveData.Difficulty = difficulty;
            _saveData.IsSoundOn = playAudio;
            FileIOWrapper.SaveGameToLocalStore(_saveData);
        }

        static public void ToggleSpikes(Phase phase)
        {
            Instance._levels[_currentLevel - 1].GetComponent<Level>().ToggleSpikes(phase);
        }
        
        static public void TogglePlatforms(Phase phase)
        {
            Instance._levels[_currentLevel - 1].GetComponent<Level>().TogglePlatforms(phase);
        }

        static public void CheckLevelUnlocked(int levelUnlocked)
        {
            if (levelUnlocked <= _saveData.LastLevelUnlocked) return;

            _saveData.LastLevelUnlocked = levelUnlocked;
            FileIOWrapper.SaveGameToLocalStore(_saveData);
        }

        static public void DisablePhaseButton()
        {
            _instance._phasedDisabled = true;
            _instance._phaseOverlay.gameObject.SetActive(true);
            _instance._currentPhasedDisabledTime = 2.5f;
        }
        
        public void ShortCutDeathAnimation()
        {
            _dyingSound.Stop();
            StopAllCoroutines();
            SetFailedScreenState();
        }

        static public float GetBoostSpeed()
        {
            switch (Difficulty){
                case Difficulty.Easy:
                    return 2.6f;
                case Difficulty.Normal:
                    return 2.2f;
                case Difficulty.Hard:
                    return 1.7f;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #endregion

        #region Method Callbacks
        private void LevelSelectButton_OnLevelClickedCallback(LevelSelectButton levelSelectButton)
        {
            _levelSelectCanvas.SetActive(false);
            _gameScene.SetActive(true);
            _currentLevel = levelSelectButton.LevelNumber;
            for (var i = 0; i < _levels.Length; i++)
            {
                _levels[i].gameObject.SetActive(i == _currentLevel - 1);
            }
           
            _levels[_currentLevel - 1].Initialize(this);      
            ReturnAllToPool();
        }

        public void Level_InitializationComplete()
        {
            _player.InitializePlayer(_levels[_currentLevel - 1].StartLocation);
        }

        private void Player_OnPlayerDied(PlayerController playerController)
        {
            ResetDisabledPhaseOverlay();
            _levels[_currentLevel - 1].StopAudio();
            if(_saveData.IsSoundOn)
                _dyingSound.Play();            
            StartCoroutine(ShowFailedScreen());
            ColliderManager.DisableColliders();
        }

        private void Player_OnLevelComplete(PlayerController playerController)
        {
            CheckLevelUnlocked(_currentLevel);
            Instance._gameScene.SetActive(false);
            Instance._levelEndedCanvas.SetActive(true);
            Instance._levelFailedScreen.SetActive(false);
            Instance._levelSuccededScreen.SetActive(true);
            ColliderManager.DisableColliders();
        }

        #endregion

        #region Private Methods

        private void GenerateLevelSelectButtons()
        {
            //Generate Level select buttons
            for (var i = 0; i < _levels.Length; i++)
            {
                GetInstance().Setup(i + 1, _saveData.LastLevelUnlocked);
            }
        }

        private void LevelComplete()
        {
            StartCoroutine(ResetPlayer());
            _currentLevel++;
            if (_currentLevel >= _saveData.LastLevelUnlocked)
            {
                _saveData.LastLevelUnlocked = _currentLevel;
                FileIOWrapper.SaveGameToLocalStore(_saveData);
            }
            _levelSelectPool.ForEach(l => l.Setup(l.LevelNumber, _saveData.LastLevelUnlocked));
            LoadNextLevel();
        }

        private void RestartLevel()
        {
            _shortCutDeathCanvas.gameObject.SetActive(false);
            _gameScene.SetActive(true);
            _levelEndedCanvas.SetActive(false);
            _levels[_currentLevel - 1].PlayAudio();
            ResetDisabledPhaseOverlay();
            StartCoroutine(ResetPlayer());
        }

        private void ShowLevelSelectScreen()
        {
            GenerateLevelSelectButtons();
            Instance._levelSelectCanvas.SetActive(true);
            Instance._levelEndedCanvas.SetActive(false);
        }

        private LevelSelectButton GetInstance()
        {
            if (_levelSelectPool.Any(l => !l.gameObject.activeSelf))
                return _levelSelectPool.FirstOrDefault(l => !l.gameObject.activeSelf);

            var instance = Instantiate(_levelSelectPrefab, _levelSelectParent).GetComponent<LevelSelectButton>();
            _levelSelectPool.Add(instance);
            instance.OnLevelClickedCallback += LevelSelectButton_OnLevelClickedCallback;
            return instance;
        }

        private void ReturnAllToPool()
        {
            _levelSelectPool.ForEach(ReturnToPool);
        }

        private void ReturnToPool(LevelSelectButton obj)
        {
            obj.gameObject.SetActive(false);
        }

        private void ResetDisabledPhaseOverlay()
        {
            _phaseOverlay.gameObject.SetActive(false);
            _phasedDisabled = false;
            PhasedDisabledTime = 0f;
        }
        #endregion

        #region Helper Methods
        static private void LoadNextLevel()
        {
            for (int i = 0; i < Instance._levels.Length; i++)
            {
                Instance._levels[i].gameObject.SetActive(i + 1 == _currentLevel);
            }

            Instance._levels[_currentLevel - 1].Initialize(Instance);
        }

        static private IEnumerator ShowFailedScreen()
        {
            Instance._shortCutDeathCanvas.SetActive(true);
            yield return _restartTime;

            SetFailedScreenState();
        }

        static private void SetFailedScreenState()
        {
            Instance._gameScene.SetActive(false);
            Instance._levelEndedCanvas.SetActive(true);
            Instance._levelFailedScreen.SetActive(true);
            Instance._levelSuccededScreen.SetActive(false);
            Instance._shortCutDeathCanvas.SetActive(false);
        }

        static private IEnumerator ResetPlayer()
        {
            Instance._player.InitializePlayer(Instance._levels[_currentLevel - 1].StartLocation);
            yield return null;
        }
        #endregion  
        #endregion       
    }
}