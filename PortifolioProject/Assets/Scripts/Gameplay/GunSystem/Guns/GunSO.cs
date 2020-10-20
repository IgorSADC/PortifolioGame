using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.GunSystem
{
    public enum BulletDistributionMethod {
        RadialDistribution
    }
    public delegate void BulletsDistributionFunction(ref GameObject[] bullets);
    

    [CreateAssetMenu(fileName = "NewGun", menuName = "GunSystem/CreateNewGun", order = 0)]
    public class GunSO : ScriptableObject
    {
        public GunType Gun;
        public GameObject BulletPrefab;
        public BulletDistributionMethod DistributionMethod;
        public int BulletCost;
        public BulletsDistributionFunction GetDistributionMethod { get => MethodMapping[DistributionMethod]; }


        private Dictionary<BulletDistributionMethod, BulletsDistributionFunction> MethodMapping;

        private void OnEnable() {
            MethodMapping = new Dictionary<BulletDistributionMethod, BulletsDistributionFunction>();
            MethodMapping.Add(BulletDistributionMethod.RadialDistribution,
                                            BulletDistributionBehaviouors.RadialDistribution);
        }



    }

}