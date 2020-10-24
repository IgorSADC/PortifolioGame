using System.Collections;
using System.Collections.Generic;
using Gameplay.GunSystem.Controller;
using UnityEngine;


namespace Gameplay.Collectables
{
    public class AmmunitionCollection : MonoBehaviour
    {
        [SerializeField] private int minValue = 10;
        [SerializeField] private int maxValue = 25;

        private int GetValue()
        {
            return Random.Range(minValue, maxValue);
        }

        private void OnTriggerEnter(Collider other) 
        {
            var otherGO = other.gameObject;
            CheckForTriggering(otherGO);
        }

        private void CheckForTriggering(GameObject otherGO)
        {
            var gunController = otherGO.GetComponent<GunController>();
            if(gunController == null) return;
            gunController.AddBullets(GetValue());
            this.gameObject.SetActive(false);
        }

    }
}