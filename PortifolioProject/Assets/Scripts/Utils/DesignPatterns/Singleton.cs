using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.Utils 
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> 
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