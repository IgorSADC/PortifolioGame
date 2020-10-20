using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.DesignPatterns{
    public class ObjectPool
    {
        public GameObject ObjectPrefab { get; private set; }
        public int NumberOfObjects { get; private set; }
        private List<GameObject> _pool;
        private Transform parentToObjs;

        public ObjectPool(GameObject prefab, int numberOfObjects, Transform parentToObjs = null)
        {
            _pool = new List<GameObject>();
            ObjectPrefab = prefab;
            NumberOfObjects = numberOfObjects;
            this.parentToObjs = parentToObjs;
            GeneratePool();
        }
        private void GeneratePool()
        {
            for (int i = 0; i < NumberOfObjects; i++) 
            {
                CreateNewObj();
            }
        }
        public GameObject GetObject(bool returnActive = true) 
        {
            foreach (var obj in _pool)
            {
                if (!obj.activeSelf) 
                {
                    Activate(obj);
                    return obj;
                }
            }
            var newObj = CreateNewObj();
            if(returnActive)
                Activate(newObj);
            return newObj;
        }

        public GameObject[] GetObjects(int nObjs, bool returnActive = true)
        {
            var objs = new GameObject[nObjs];
            var cIndex = 0;
            foreach (var obj in _pool)
            {
                if(!obj.activeSelf)
                {
                    if(returnActive)
                        Activate(obj);
                    objs[cIndex] = obj;
                    cIndex++;
                    if (cIndex == nObjs)
                        break;
                }
            }
            while(cIndex < nObjs)
            {

                var newObj = CreateNewObj();
                if(returnActive)
                    Activate(newObj);
                objs[cIndex] = newObj;
                cIndex++;
            }


            return objs;
        }

        private GameObject CreateNewObj()
        {
            var newObj = Object.Instantiate(ObjectPrefab);
            newObj.SetActive(false);
            newObj.transform.parent = parentToObjs;
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
            obj.transform.parent = parentToObjs;
            obj.GetComponent<IPool>().Initialize();
        }

    }
}