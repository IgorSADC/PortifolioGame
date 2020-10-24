using UnityEngine;
using Utils.DesignPatterns;
using Gameplay.GunSystem.Events;
using System;


namespace Gameplay.GunSystem
{
    public class Gun : MonoBehaviour
    {
        public int pooledBulletsObjectsPerGun = 8;

        private ObjectPool bulletPool;

        [SerializeField] private int currentBulletOnCartdrige;
        [SerializeField] private int spareAmmunition;

        public GunSO GunScriptableObject;
        public GunType HolderType;
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
            GunSystemBroker.OnBulletDone += ActivateBullet;
        }

        public void Shoot()
        {
            var shouldDestroy = false;
            if(HasGun)
            {
                if(canShoot)
                {
                    var bullets = bulletPool.GetObjects(NumberOfBulletsPerShot, false);
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

/// <summary>
/// This method changes the gun for one of the same type of the Holder.
/// It's not supposed to be called directly. The responsable for that is the gunController
/// </summary>
/// <param name="newGun"></param>
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
            spareAmmunition += nBullets;
        }

        private void ActivateBullet(GameObject bullet)
        {
            bulletPool.Activate(bullet);
        }

        private void Update() 
        {
            if(Input.GetKeyDown(shootButton))
                Shoot();
            if(Input.GetKeyDown(KeyCode.R))
                Recharge();
        }
    }

}