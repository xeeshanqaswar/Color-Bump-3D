using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.PlayerController;
using Scripts.Manager;
using DG.Tweening;

namespace Scripts.Props
{
    public class DestoryObj : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SetActive(false);
        }

    }
}
