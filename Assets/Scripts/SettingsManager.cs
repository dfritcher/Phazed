using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField]
        private Image _audioOn = null;

        [SerializeField]
        private Image _audioOff = null;

        [SerializeField]
        private Image _easySelected = null;

        [SerializeField]
        private Image _easyNotSelected = null;

        [SerializeField]
        private Image _normalSelected = null;

        [SerializeField]
        private Image _normalNotSelected = null;

        [SerializeField]
        private Image _hardSelected = null;

        [SerializeField]
        private Image _hardNotSelected = null;

        [SerializeField]
        private AudioListener _gameAudioListener = null;
        
        private static bool _playAudio = false;
        private Difficulty _difficulty;


        public void OnEnable()
        {
            _playAudio = GameManager.PlayAudio;
            _difficulty = GameManager.Difficulty;

            UpdateSettingsUI();
        }

        public void ToggleSound()
        {
            _playAudio = !_playAudio;
            _audioOn.gameObject.SetActive(_playAudio);
            _audioOff.gameObject.SetActive(!_playAudio);
            _gameAudioListener.enabled = _playAudio;
        }

        
        public void HomeButtonClicked()
        {
            SaveSettings();            
        }

        private void UpdateSettingsUI()
        {
            SetDifficulty(_difficulty);
            _audioOn.gameObject.SetActive(_playAudio);
            _audioOff.gameObject.SetActive(!_playAudio);
        }

        public void SetDifficulty(int value)
        {
            SetDifficulty(value.ToDifficulty());
        }

        private void SetDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;
            ResetDifficultyImages();
            switch (difficulty)
            {
                case Difficulty.Easy:
                    _easySelected.gameObject.SetActive(true);
                    _normalNotSelected.gameObject.SetActive(true);
                    _hardNotSelected.gameObject.SetActive(true);
                    break;
                case Difficulty.Normal:
                    _easyNotSelected.gameObject.SetActive(true);
                    _normalSelected.gameObject.SetActive(true);
                    _hardNotSelected.gameObject.SetActive(true);
                    break;
                case Difficulty.Hard:
                    _easyNotSelected.gameObject.SetActive(true);
                    _normalNotSelected.gameObject.SetActive(true);
                    _hardSelected.gameObject.SetActive(true);
                    break;
                default:
                    _easyNotSelected.gameObject.SetActive(true);
                    _normalSelected.gameObject.SetActive(true);
                    _hardNotSelected.gameObject.SetActive(true);
                    break;
            }
        }

        private void ResetDifficultyImages()
        {
            _easyNotSelected.gameObject.SetActive(false);
            _easySelected.gameObject.SetActive(false);
            _normalNotSelected.gameObject.SetActive(false);
            _normalSelected.gameObject.SetActive(false);
            _hardNotSelected.gameObject.SetActive(false);
            _hardSelected.gameObject.SetActive(false);
        }

        private void SaveSettings()
        {
            GameManager.UpdateSettings(_playAudio, _difficulty);
        }
    }
}