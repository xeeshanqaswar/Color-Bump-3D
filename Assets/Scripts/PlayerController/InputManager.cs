using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerController {
    public class InputManager : MonoBehaviour
    {

        #region FIELDS DECLERATION

        private float _vertical;
        private float _horizontal;

        private StateController _stateController;
        private CameraHandler _cameraHandler;
        private float _delta;

        #endregion

        public void Start()
        {
            TryGetComponent<StateController>(out _stateController);
            _stateController.Init();

            Camera.main.transform.parent.TryGetComponent<CameraHandler>(out _cameraHandler);
            _cameraHandler.Init(_stateController);
        }

        public void Update()
        {
            _delta = Time.deltaTime;

            _stateController.Tick(_delta);
            _cameraHandler.Tick(_delta);
            GetInput();
            UpdatePlayerState();
        }

        public void FixedUpdate()
        {
            _delta = Time.fixedDeltaTime;
            _stateController.FixedTick(_delta);
        }

        private void GetInput()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }

        private void UpdatePlayerState()
        {
            _stateController.horizontal = _horizontal;
            _stateController.vertical = _vertical;

            float detectInput = Mathf.Abs(_horizontal) + Mathf.Abs(_vertical);
            _stateController.moveInput = Mathf.Clamp01(detectInput);
        }

    }
}
