using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.DesignPatterns;
using Gameplay.GunSystem.Events;


namespace Gameplay.GunSystem
{
    public class Gun : MonoBehaviour
    {
        public int pooledBulletsObjectsPerGun = 8;

        private ObjectPool bulletPool;

        [SerializeField] private int currentBulletOnCartdrige;
        [SerializeField] private int spareAmmunition;

        public GunSO GunScriptableObject;
        private GunType gunType {get => GunScriptableObject.Gun; }
        private GameObject bulletPrefab {get => GunScriptableObject.BulletPrefab ;}
        private int BulletMaxQuantityPerCartridge {get => GunScriptableObject.BulletsPerCartridge;}
        private bool HasGun { get => GunScriptableObject != null; }
        private int NumberOfBulletsPerShot { get => GunScriptableObject.BulletCost; }
        private bool canShoot {get => currentBulletOnCartdrige >= NumberOfBulletsPerShot ;}

        [SerializeField] private KeyCode shootButton;

        private void Awake() 
        {
            bulletPool = new ObjectPool(bulletPrefab, pooledBulletsObjectsPerGun, this.transform);
            currentBulletOnCartdrige = BulletMaxQuantityPerCartridge;
            
        }

        public void Shoot()
        {
            var shouldDestroy = false;
            if(HasGun)
            {
                if(canShoot)
                {
                    var bullets = bulletPool.GetObjects(NumberOfBulletsPerShot);
                    GunScriptableObject.GetDistributionMethod(ref bullets, gunType);
                    currentBulletOnCartdrige -= NumberOfBulletsPerShot;
                    if(currentBulletOnCartdrige <= 0) 
                        shouldDestroy = !Recharge();
                    

                }
                else 
                    shouldDestroy = !Recharge();
            }

            if(shouldDestroy)
                RemoveGun();
                 
        }

        public bool Recharge()
        {
            if(spareAmmunition == 0) 
            {
                return false;
            }

            var d = BulletMaxQuantityPerCartridge - currentBulletOnCartdrige;

            if (CheckForSpareAmmunition(d))
            {
                spareAmmunition -= d;
                currentBulletOnCartdrige += d;
            }
            else
            {
                currentBulletOnCartdrige += spareAmmunition;
                spareAmmunition = 0;
                
            }
            return true;
        }

        private bool CheckForSpareAmmunition(int d)
        {
            return spareAmmunition >= d;
        }

        public void ChangeGun(GunSO newGun)
        {
            GunScriptableObject = newGun;
            spareAmmunition = 0;
            currentBulletOnCartdrige = BulletMaxQuantityPerCartridge;
        }

        public void RemoveGun()
        {
            GunScriptableObject = null;
        }

        public void AddBullets(int nBullets)
        {
            var d = BulletMaxQuantityPerCartridge - currentBulletOnCartdrige;
            if(nBullets <= d)
                currentBulletOnCartdrige += nBullets;
            else 
            {
                currentBulletOnCartdrige += d;
                spareAmmunition += nBullets - d;
            }
        }

        private void Update() 
        {
            if(Input.GetKeyDown(shootButton))
                Shoot();
        }


        
    }

}