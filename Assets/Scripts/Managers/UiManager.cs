using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Scripts.Manager
{
    public class UiManager : MonoBehaviour
    {
        #region FIELDS DECLERATION

        [Header("== UI REFERENCES ==")]
        public ProgressBar progressBar;

        [Header("== UI PANELS ==")]
        public GameObject mainMenu;
        public GameObject levelFail;
        public GameObject levelComplete;
        public GameObject gameComplete;

        private GameData _gameData;
        public static UiManager Instance;

        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GameManager.GameCompleteEvent += OnGameComplete;
            GameManager.LevelCompleteEvent += OnCompleteLevel;
            GameManager.GameFailEvent += OnGameFail;
            GameManager.GameStartEvent += OnGameStart;

            mainMenu.SetActive(true); // Activate Mainmenu on start
        }

        public void Init(GameData data)
        {
            _gameData = data;
            progressBar.Init();
        }

        /// <summary>
        /// Update Logic for Ui Manager
        /// </summary>
        public void Tick(float delta)
        {
            progressBar.Tick(delta);
        }

        public void GameStartButtonPress()
        {
            GameManager.GameStartEventInvoke();
        }

        public void GameCompleteButtonPress()
        {
            Application.Quit();
            Debug.Log("Game is completed!!");
        }

        public void NextLevelButtonPress()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Gameplay");
        }

        public void LevelRetryButtonPress()
        {
            SceneManager.LoadScene("Gameplay");
        }

        private void OnGameStart()
        {
            mainMenu.SetActive(false);
        }

        private void OnCompleteLevel()
        {
            if (_gameData.currentLevel >= _gameData.maxLevelsCap)
            {
                return;
            }

            levelComplete.SetActive(true);
        }

        private void OnGameComplete()
        {
            gameComplete.SetActive(true);
        }

        private void OnGameFail()
        {
            levelFail.SetActive(true);
        }

        private void OnDisable()
        {
            GameManager.GameCompleteEvent -= OnGameComplete;
            GameManager.LevelCompleteEvent -= OnCompleteLevel;
            GameManager.GameFailEvent -= OnGameFail;
            GameManager.GameStartEvent -= OnGameStart;
        }

    }
}
