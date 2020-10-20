using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPatterns;


namespace Gameplay.GunSystem
{
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
        }

        private void ChangeGun(GunSO newGun)
        {
            GunScriptableObject = newGun;
        }

        private void Update() {
            if(Input.GetKeyDown(shootButton))
                Shoot();
        }


        
    }

}