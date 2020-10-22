using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.GunSystem
{
    [CreateAssetMenu(fileName = "NewGun", menuName = "GunSystem/CreateNewGun", order = 0)]
    public class GunSO : ScriptableObject
    {
        public GunType Gun;
        public GameObject BulletPrefab;
        public BulletDistributionMethod DistributionMethod;
        public int BulletCost;
        public int BulletsPerCartridge = 15;
        public BulletsDistributionFunction GetDistributionMethod { get => GunMethodMapping.GetMethodByType(DistributionMethod); }
    
    }

}