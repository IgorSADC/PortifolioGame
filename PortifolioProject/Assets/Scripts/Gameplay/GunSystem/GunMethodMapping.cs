using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.GunSystem{


/// <summary>
/// This class is a singleton that maps de enum BulletDistributionMethod to the delegate BulletsDistributionFunction.
/// </summary>
    public class GunMethodMapping
    {
        private Dictionary<BulletDistributionMethod, BulletsDistributionFunction> methodMapping;
        private static GunMethodMapping _instance;
        private static GunMethodMapping Instance { get => GetInstance();}         
        private GunMethodMapping()
        {
            methodMapping = new Dictionary<BulletDistributionMethod, BulletsDistributionFunction>();
            methodMapping.Add(BulletDistributionMethod.RadialDistribution,
                                            BulletDistributionBehaviours.RadialDistribution);
        }

        private static GunMethodMapping GetInstance(){
            if (_instance != null)
                return _instance;
            _instance = new GunMethodMapping();
            return _instance;
        }

        public static BulletsDistributionFunction GetMethodByType(BulletDistributionMethod type)
        {
            return Instance.methodMapping[type];
        }
    }

}