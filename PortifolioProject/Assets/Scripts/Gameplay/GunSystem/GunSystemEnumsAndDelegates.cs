using UnityEngine;

namespace Gameplay.GunSystem
{
    public enum BulletDistributionMethod 
    {
        RadialDistribution,
        Spray
    }

    public enum GunType
    {
        Right,
        Left,
        Front
    }

    public delegate void BulletsDistributionFunction(ref GameObject[] bullets, GunType type);

}