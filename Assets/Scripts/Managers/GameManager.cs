using UnityEngine;
using System;

namespace Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region FIELDS DECLERATION

        public GameData gameData;
        [NonSerialized] public Vector3 playerPos;
        [NonSerialized] public Vector3 finishPoint;

        private UiManager _uiManager;
        private LevelManager _levelManager;
        private float _delta;

        public static GameManager Instance;
        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;

            _levelManager = LevelManager.Instance;
            _levelManager.Init(gameData);

            _uiManager = UiManager.Instance;
            _uiManager.Init(gameData);
        }

        private void Update()
        {
            _delta = Time.deltaTime;
            _uiManager.Tick(_delta);
        }

        #region EVENTS & ACTIONS INVOKING

        public static event Action GameFailEvent;
        public static event Action GameStartEvent;
        public static event Action LevelCompleteEvent;
        public static event Action GameCompleteEvent;

        public static event Action LevelProgressEvent;

        public static void GameFailEventInvoke()
        {
            GameFailEvent.Invoke();
        }

        public static void GameStartEventInvoke()
        {
            GameStartEvent.Invoke();
        }

        public static void LevelCompleteEventInvoke()
        {
            LevelCompleteEvent.Invoke();
        }

        public static void GameCompleteEventInvoke()
        {
            GameCompleteEvent.Invoke();
        }

        public static void LevelProgressEventInvoke()
        {
            LevelProgressEvent.Invoke();
        }

        #endregion

    }
}
