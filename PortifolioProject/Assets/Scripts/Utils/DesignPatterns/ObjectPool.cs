using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.DesignPatterns{
    public class ObjectPool
    {
        public GameObject ObjectPrefab { get; private set; }
        public int NumberOfObjects { get; private set; }
        private List<GameObject> _pool;

        public ObjectPool(GameObject prefab, int numberOfObjects)
        {
            _pool = new List<GameObject>();
            ObjectPrefab = prefab;
            NumberOfObjects = numberOfObjects;
            GeneratePool();
        }
        private void GeneratePool()
        {
            for (int i = 0; i < NumberOfObjects; i++) 
            {
                CreateNewObj();
            }
        }
        public GameObject GetObject() 
        {
            foreach (var obj in _pool)
            {
                if (!obj.activeSelf) 
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            var newObj = CreateNewObj();
            newObj.SetActive(true);
            return newObj;
        }

        private GameObject CreateNewObj()
        {
            var newObj = Object.Instantiate(ObjectPrefab);
            newObj.SetActive(false);
            var IPool = newObj.GetComponent<IPool>();
            IPool.DeactivateObject += Deactivate;
            IPool.ActivateObject += Activate;
            _pool.Add(newObj);

            return newObj;
        }

        private void Deactivate(GameObject obj) 
        {
            obj.SetActive(false);
        }

        private void Activate(GameObject obj) 
        {
            obj.SetActive(true);
            obj.GetComponent<IPool>().Initialize();
        }

    }
}