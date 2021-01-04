using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.PlayerController;
using Scripts.Manager;
using DG.Tweening;
using System;

namespace Scripts.Props
{
    public class PropId : MonoBehaviour
    {
        public int id;

        private bool _canDamage = true;

        private void OnEnable()
        {
            GameManager.LevelCompleteEvent += OnGameComplete;
            GameManager.GameCompleteEvent += OnGameComplete;
        }

        private void OnGameComplete()
        {
            _canDamage = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (TryGetComponent<DOTweenAnimation>(out DOTweenAnimation myAnim))
            {
                myAnim.enabled = false;
            }

            if (collision.collider.CompareTag("Player") && _canDamage)
            {
                int pId = collision.transform.parent.GetComponent<StateController>().playerId;
                if (this.id != pId)
                {
                    GameManager.GameFailEventInvoke();
                }
            }
        }

        private void OnDisable()
        {
            GameManager.LevelCompleteEvent -= OnGameComplete;
            GameManager.GameCompleteEvent -= OnGameComplete;
        }

    }
}
