using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Scripts.Manager
{
    public class ProgressBar : MonoBehaviour
    {

        #region FIELDS DECLERATION

        [Header("== PROPERTIES ==")]
        public float lerpSpeed = 2f;

        [Header("== REFERENCES ==")]
        public GameData gameData;
        public TextMeshProUGUI currentLevel;
        public TextMeshProUGUI nextLevel;
        public Image progressImage;

        private float _distanceToCover;
        private bool _trackDisntace;

        #endregion

        private void OnEnable()
        {
            _trackDisntace = true;
            GameManager.LevelCompleteEvent += OnLevelComplete;
        }

        public void Init()
        {
            currentLevel.text = gameData.currentLevel.ToString("00");
            int nxtLevel = gameData.currentLevel + 1;
            nextLevel.text = nxtLevel.ToString("00");
            _distanceToCover = Vector3.Distance(GameManager.Instance.finishPoint, GameManager.Instance.playerPos);
        }

        public void Tick(float delta)
        {
            if (!_trackDisntace)
            {
                return;
            }

            float disToCover = Vector3.Distance(GameManager.Instance.finishPoint, GameManager.Instance.playerPos);
            float fillCalculated = 0.95f - Mathf.InverseLerp(0, _distanceToCover, disToCover);
            progressImage.fillAmount = fillCalculated;
        }

        private void OnLevelComplete()
        {
            progressImage.fillAmount = 1;
            _trackDisntace = false;
        }

        private void OnDisable()
        {
            GameManager.LevelCompleteEvent -= OnLevelComplete;
        }


    }
}
