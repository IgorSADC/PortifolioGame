using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GunSystem
{
    public class GunSystemBroker
    {
        public static event Action<GameObject> OnGunShot;

        public static void ActivateOnGunShot(GameObject shooter)
        {
            if (OnGunShot != null)
                OnGunShot(shooter);
        }
    }

}