using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class LevelManager : MonoBehaviour
    {

        #region FIELDS DECLERATION

        [Header("== PROPERTIES ==")]
        public int maxLevelCap = 30;
        public float tileMargin;
        public int preprocessTiles = 3;

        [Header("== REFERENCES ==")]
        public GameObject finishLineTile;
        public GameObject emptyTiles;
        public GameObject[] levelTiles;

        private GameData _gameData;

        private int _levelRange;
        private float _newSpawnPos = 0;

        private Transform _tilesParent;
        private List<int> _levelData;
        private Queue<GameObject> _tilesBeingUsed;
        private Queue<GameObject> _spawnedTiles;

        public static LevelManager Instance;

        #endregion

        private void Awake()
        {
            Instance = this;
        }

        // only for Subscribing to events
        private void OnEnable()
        {
            GameManager.LevelCompleteEvent += OnCompleteLevel;
            GameManager.LevelProgressEvent += ProgressLevel;
        }

        public void Init(GameData data)
        {
            _gameData = data;

            _levelData = new List<int>();
            _spawnedTiles = new Queue<GameObject>();
            _tilesBeingUsed = new Queue<GameObject>();

            _tilesParent = new GameObject("Environment Tiles").transform;
            _tilesParent.parent = gameObject.transform;

            InitateLevels();
            PreProcessTiles();
        }

        /// <summary>
        /// Spawn tiles, store them and then hide them.
        /// </summary>
        private void InitateLevels()
        {
            ManageDifficulity();

            // Instantiate Starting Tile
            GameObject spawnedTile = Instantiate(emptyTiles, _tilesParent.transform);
            spawnedTile.transform.position = Vector3.forward * _newSpawnPos;
            spawnedTile.SetActive(false);

            _newSpawnPos += tileMargin;
            _spawnedTiles.Enqueue(spawnedTile);

            // Instantiate Level Tiles
            for (int i = 0; i < _levelData.Count; i++)
            {
                spawnedTile = Instantiate(levelTiles[_levelData[i]], _tilesParent.transform);
                spawnedTile.transform.position = Vector3.forward * _newSpawnPos;
                spawnedTile.SetActive(false);

                _newSpawnPos += tileMargin;
                _spawnedTiles.Enqueue(spawnedTile);
            }

            // Instantiate Final Tile
            spawnedTile = Instantiate(finishLineTile, _tilesParent.transform);
            spawnedTile.transform.position = Vector3.forward * _newSpawnPos;
            GameManager.Instance.finishPoint = spawnedTile.transform.position;
            spawnedTile.SetActive(false);

            _newSpawnPos += tileMargin;
            _spawnedTiles.Enqueue(spawnedTile);

            // Instantiate Empty Tiles
            for (int i = 0; i < 10; i++)
            {
                spawnedTile = Instantiate(emptyTiles, _tilesParent.transform);
                spawnedTile.transform.position = Vector3.forward * _newSpawnPos;
                spawnedTile.SetActive(false);

                _newSpawnPos += tileMargin;
                _spawnedTiles.Enqueue(spawnedTile);
            }
        }

        /// <summary>
        /// Run first time to display how many tiles you want to show first.
        /// </summary>
        private void PreProcessTiles()
        {
            for (int i = 0; i < preprocessTiles; i++)
            {
                GameObject obj = _spawnedTiles.Dequeue();
                _tilesBeingUsed.Enqueue(obj);
                obj.SetActive(true);
            }
        }
        
        /// <summary>
        /// Hide & Show tiles as player moves Forward
        /// </summary>
        public void ProgressLevel()
        {
            GameObject objToHide = _tilesBeingUsed.Dequeue();
            GameObject objToShow = _spawnedTiles.Dequeue();
            _tilesBeingUsed.Enqueue(objToShow);

            objToShow.SetActive(true);
            objToHide.SetActive(false);
        }

        private void ManageDifficulity()
        {
            if (_gameData.currentLevel < 10)
            {
                _levelRange = 10;
            }
            else if (_gameData.currentLevel < 20)
            {
                _levelRange = 15;
            }
            else
            {
                _levelRange = 20;
            }

            RandomGenerateLevel();
        }

        private void RandomGenerateLevel()
        {
            // Generate random sequence for level
            Random.InitState(_gameData.currentLevel);
            for (int i = 0; i < _levelRange; i++)
            {
                int randomDigit = Mathf.RoundToInt(Random.value * (levelTiles.Length - 1));
                _levelData.Add(randomDigit);
            }

            #region IN PROGRESS

            //Debug.Log("With out refine");
            //foreach (var item in _levelData)
            //{
            //    Debug.Log(item);
            //}

            //// Refine random sequence.....
            //List<int> tempArray = _levelData;
            //_levelData = new List<int>();

            //int pointer = 0;
            //while (_levelData.Count < tempArray.Count)
            //{
            //    if (pointer == 0)
            //    {
            //        _levelData.Add(tempArray[pointer]);
            //    }
            //    else
            //    {
            //        if (tempArray[pointer - 1] != tempArray[pointer])
            //        {
            //            _levelData.Add(tempArray[pointer]);
            //        }
            //    }
            //    pointer++;
            //}

            //Debug.Log("With refine");
            //foreach (var item in _levelData)
            //{
            //    Debug.Log(item);
            //}

            #endregion
        }

        private void OnCompleteLevel()
        {
            if (maxLevelCap != 0 && (_gameData.currentLevel >= _gameData.maxLevelsCap))
            {
                Debug.Log(" Game Complete Congratulation!!!");
                GameManager.GameCompleteEventInvoke();
                return;
            }

            _gameData.currentLevel++;
            _gameData.SavePlayerProgression();
        }

        private void OnDisable()
        {
            GameManager.LevelCompleteEvent -= OnCompleteLevel;
            GameManager.LevelProgressEvent -= ProgressLevel;
        }

    }
}
