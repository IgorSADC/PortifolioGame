using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions;
using System;
using Gameplay.GunSystem.Events;
using Utils.DesignPatterns;


namespace Gameplay.GunSystem{
    public class BulletDistributionBehaviours : MonoBehaviourSingleton<BulletDistributionBehaviours>
    {
        /// <summary>
        /// This function distribute a series of bullets into an arc.It's higly recommended to use an even bullet number
        /// </summary>
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
            ActivateOnBulletDone(bulletsGameObjects[0]);

            while (i < nBullets)
            {
                var rotationRight = new Vector3(0, angleIncrement * i , 0);
                var rotationLeft= new Vector3(0, angleIncrement * -i , 0);
                bulletsGameObjects[i].transform.Rotate(rotationRight);
                bulletsGameObjects[i + 1].transform.Rotate(rotationLeft);
                ActivateOnBulletDone(bulletsGameObjects[i]);
                ActivateOnBulletDone(bulletsGameObjects[i+1]);
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
                ActivateOnBulletDone(bulletsGameObjects[i]);

            }
        }



        public static void SprayBulletBehaviour(ref GameObject[] bulletsGameObjects, GunType type)
        {
            BulletDistributionBehaviours
                                .instance
                                .StartCoroutine(WaitToSpawn(bulletsGameObjects, .05f));
        }



        private static IEnumerator WaitToSpawn(GameObject[] bulletsGameObjects, float s)
        {
            foreach (var bullet in bulletsGameObjects)
            {
                ActivateOnBulletDone(bullet);
                yield return new WaitForSeconds(s);
            }
        }

        private static void ActivateOnBulletDone(GameObject bullet)
        {
            GunSystemBroker.ActivateOnBulletDone(bullet);
        }
    }
        
}