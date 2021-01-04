using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Manager;

namespace Scripts.Props
{
    public class FinishPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.LevelCompleteEventInvoke();
            }
        }
    }
}
