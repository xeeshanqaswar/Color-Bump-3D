using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Manager;

namespace Scripts.PlayerController
{
    public class StateController : MonoBehaviour
    {

        #region FIELDS DECLERATION

        //public bool receiveDamage;
        public int playerId;

        [Header("PROPERTIES")]
        [SerializeField]private float moveSpeed = 4f;
        public float ballThrust = 5f;
        public float ballConstraintLR = 5f;

        [Header("PLAYER STATES")]
        public bool isPlaying;
        public bool levelComplete;
        public bool gameFail;

        [Header("DEBUG")]
        public float moveInput; // For detecting Input
        public float horizontal;
        public float vertical;

        private Transform _myTransform;
        private Transform _playerBall;
        private float _maxBallDist;
        private Camera _mainCam;

        #endregion

        private void OnEnable()
        {
            GameManager.GameStartEvent += OnGameStart;
            GameManager.LevelCompleteEvent += OnlevelComplete;
            GameManager.GameFailEvent += OnGameFail;
        }

        public void Init()
        {
            transform.GetChild(0).TryGetComponent<Transform>(out _playerBall);
            TryGetComponent<Transform>(out _myTransform);

            _mainCam = Camera.main;
            _maxBallDist = 12f + 8f;
        }

        private void OnGameStart()
        {
            isPlaying = true;
        }

        public void Tick(float delta)
        {
            HandleMovement(delta);
            ClampPlayerBall(delta);
            LevelProgress();

            // Giving away position of player
            GameManager.Instance.playerPos = _playerBall.position;
        }

        public void FixedTick(float delta)
        {

        }

        private void HandleMovement(float delta)
        {
            if (!isPlaying || gameFail)
                return;

            if (levelComplete)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, 0.5f, Time.deltaTime * 2f);
                ballThrust = 0f;
            }
            _myTransform.position += Vector3.forward * moveSpeed * delta;
        }

        /// <summary>
        /// Defining Screen bounds and clamping player
        /// position to screen bounds.
        /// </summary>
        private void ClampPlayerBall(float delta)
        {
            Vector3 newPos = new Vector3(_playerBall.localPosition.x, 0f, _playerBall.localPosition.z);
            _playerBall.localPosition = newPos;

            // Convert WorldPosition to Viewport point
            Vector3 ballWorldPos = transform.TransformPoint(_playerBall.localPosition);
            Vector3 wpToViewport = _mainCam.WorldToViewportPoint(ballWorldPos);
            wpToViewport.x = Mathf.Clamp01(wpToViewport.x);
            wpToViewport.y = Mathf.Clamp(wpToViewport.y, 0.1f, 0.95f); // Restrict player in Forward Position

            Vector3 vpToWorld = _mainCam.ViewportToWorldPoint(wpToViewport);
            vpToWorld.x = Mathf.Clamp(vpToWorld.x, -ballConstraintLR, ballConstraintLR);
            _playerBall.localPosition = transform.InverseTransformPoint(vpToWorld);
        }

        private void LevelProgress()
        {
            int currentPos = Mathf.RoundToInt(transform.position.z);

            if (currentPos == _maxBallDist)
            {
                GameManager.LevelProgressEventInvoke();
                _maxBallDist += 12f;
            }
        }

        private void OnlevelComplete()
        {
            levelComplete = true;
        }

        private void OnGameFail()
        {
            gameFail = true;
        }

        private void OnDisable()
        {
            GameManager.GameStartEvent -= OnGameStart;
            GameManager.LevelCompleteEvent -= OnlevelComplete;
            GameManager.GameFailEvent -= OnGameFail;
        }

    }
}
