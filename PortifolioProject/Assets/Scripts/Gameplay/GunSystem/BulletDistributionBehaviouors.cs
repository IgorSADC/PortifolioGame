using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions;
using System;


namespace Gameplay.GunSystem{
    public class BulletDistributionBehaviouors
    {
        /// <summary>
        /// This function distribute a series of bullets into an arc.It's higly recommended to use an even bullet number
        /// </summary>
        /// <param name="nBullet">The quantity of bullets to be distributed. Remeber to use an even number.></param>
        /// <returns></returns>
        public static void RadialDistribution(ref GameObject[] bulletsGameObjects, GunType type)
        {
            if (type == GunType.Front) 
                RadialDistributionTwoHanded(bulletsGameObjects);
            else
                RadialDistributionOneHanded (bulletsGameObjects, type);

        }

        private static void RadialDistributionTwoHanded(GameObject[] bulletsGameObjects)
        {
            var nBullets = bulletsGameObjects.Length;
            if(nBullets % 2 == 0)
            {
                throw new ArgumentException("PLEASE USE ONLY AN EVEN NUMBER OF BULLETS");
            }
            var i = 1;
            var loopCondition = (nBullets-1)/2;
            var angleIncrement = 90 / nBullets;

            while (i < nBullets)
            {
                var rotationRight = new Vector3(0, angleIncrement * i , 0);
                var rotationLeft= new Vector3(0, angleIncrement * -i , 0);
                bulletsGameObjects[i].transform.Rotate(rotationRight);
                bulletsGameObjects[i + 1].transform.Rotate(rotationLeft);
                i += 2;
            }

        }

        private static void RadialDistributionOneHanded(GameObject[] bulletsGameObjects, GunType type)
        {
            var invert = type == GunType.Left ? -1 : 1;
            var nBullets = bulletsGameObjects.Length;
            var angleIncrement = 90 / nBullets;

            for (int i = 0; i < nBullets; i++)
            {
                var rotation = new Vector3(0, angleIncrement * i * invert, 0);
                bulletsGameObjects[i].transform.Rotate(rotation);

            }
        }
    }
        
}