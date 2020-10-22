using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPatterns;
using Gameplay.GunSystem.Events;


namespace Gameplay.GunSystem
{
    public delegate bool ShotController();
    public class Gun : MonoBehaviour
    {
        public int pooledBulletsObjectsPerGun = 20;

        private ObjectPool bulletPool;

        [SerializeField] private int currentBulletQuantity;

        public GunSO GunScriptableObject;
        private GunType gunType {get => GunScriptableObject.Gun; }
        private GameObject bulletPrefab {get => GunScriptableObject.BulletPrefab ;}
        public bool HasGun { get => GunScriptableObject != null; }
        public int NumberOfBulletsPerShot { get => GunScriptableObject.BulletCost; }

        [SerializeField] private KeyCode shootButton;

        private void Awake() 
        {
            bulletPool = new ObjectPool(bulletPrefab, pooledBulletsObjectsPerGun, this.transform);
            
        }

        private void Shoot()
        {
            var bullets = bulletPool.GetObjects(NumberOfBulletsPerShot);
            GunScriptableObject.GetDistributionMethod(ref bullets, gunType);
            GunSystemBroker.ActivateOnGunShot(this.gameObject);
        }

        public void ChangeGun(GunSO newGun)
        {
            GunScriptableObject = newGun;
        }

        public void RemoveGun()
        {
            GunScriptableObject = null;
        }

        private void Update() 
        {
            if(Input.GetKeyDown(shootButton) && HasGun)
                Shoot();
        }


        
    }

}