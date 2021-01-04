using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerController
{
    public class CameraHandler : MonoBehaviour
    {
        #region FIELDS DECLERATION

        public float mvSpeed = 3f;

        private StateController _stateController;

        #endregion

        public void Init(StateController player)
        {
            _stateController = player;
        }

        public void FixedTick(float delta)
        {
            
        }

        public void Tick(float delta)
        {
            HandlePosition(Time.deltaTime);
        }

        private void HandlePosition(float delta)
        {
            transform.position = Vector3.Lerp(transform.position, _stateController.transform.position, mvSpeed * delta);
        }

    }
}
