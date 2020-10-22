using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils.DesignPatterns{
    public delegate void OnModifyPool(GameObject obj);
    public interface IPool
    {
        event OnModifyPool DeactivateObject;
        event OnModifyPool ActivateObject;

        bool IsAvailable { get; set; }

        void Initialize();
    }
}