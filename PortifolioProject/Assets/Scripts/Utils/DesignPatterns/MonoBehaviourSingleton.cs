using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{

    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T> 
    {
        private static T instance;
        public T Instance { get =>instance; }

        public static bool IsInitialize { get =>  instance != null;  }
        
        protected virtual void Awake()
        {
            if (instance != null) 
            {
                Destroy(this.gameObject);
            }
            else 
            {
                instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

    }

}
