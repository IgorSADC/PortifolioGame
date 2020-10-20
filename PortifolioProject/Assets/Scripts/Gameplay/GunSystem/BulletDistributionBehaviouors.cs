using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions;


namespace Gameplay.GunSystem{
    public class BulletDistributionBehaviouors
    {
        /// <summary>
        /// This function distribute a series of bullets into an arc.It's higly recommended to use an even bullet number
        /// </summary>
        /// <param name="nBullet">The quantity of bullets to be distributed. Remeber to use an even number.></param>
        /// <returns></returns>
        public Vector3[] RadialDistribution(int nBullet)
        {
            var bulletsDirections = new Vector3[nBullet];
            var angleIncrement = 90/nBullet;
            var rightInitialVector = new Vector3(1, 0, 0);
            var rotation = Quaternion.Euler(0, angleIncrement, 0);
            var firstVector = rotation * rightInitialVector;

            bulletsDirections[0] = firstVector;
            for (int i = 1; i < nBullet; i++)
            {
                bulletsDirections[i] = rotation * bulletsDirections[i-1];
            }
            return bulletsDirections;
            
        }

        public static void RadialDistribution(ref GameObject[] bulletsGameObjects)
        {
            var nBullets = bulletsGameObjects.Length;
            var angleIncrement = 90/nBullets;
            
            for (int i = 0; i < nBullets; i++)
            {
                var rotation = new Vector3(0, angleIncrement * i, 0);
                bulletsGameObjects[i].transform.Rotate(rotation);

            }
            
        }

    }
        
}