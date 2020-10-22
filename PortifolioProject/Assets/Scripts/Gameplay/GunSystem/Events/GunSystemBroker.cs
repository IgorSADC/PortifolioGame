using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An event broker for the gun system. This class is a publisher subscriber design pattern.
/// </summary>
namespace Gameplay.GunSystem.Events
{
    public class GunSystemBroker
    {

        /// <summary>
        /// This event is trigger by the bullet behaviour.
        /// It's primary objective is to notify the gun that the bullet is ready to be active.
        /// </summary>
        public static event Action <GameObject> OnBulletDone;
        public static void ActivateOnBulletDone(GameObject bullet)
        {
            if(OnBulletDone != null)
                OnBulletDone(bullet);
        }


        /// <summary>
        /// This event is trigger everytime a gameObject shot using the gun Script.
        /// It's useful to add effects like recoil or just counting how many shoots.
        /// </summary>
        public static event Action<GameObject> OnGunShot;

        public static void ActivateOnGunShot(GameObject shooter)
        {
            if (OnGunShot != null)
                OnGunShot(shooter);
        }


    }

}