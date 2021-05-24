using System;
using System.Collections.Generic;
using UnityEngine;

namespace YuSystem.Gameplay.Utilities
{
    public class GameObjectsPooler : MonoBehaviour
    {
        private static GameObjectsPooler instance = null;

        public static GameObjectsPooler Instance
        {
            get
            {
                if (!instance) instance = FindObjectOfType<GameObjectsPooler>();
                return instance;
            }
        }

        [Serializable]
        public struct Pool
        {
            public GameObject prefab;
            public int num;
        }

        [SerializeField] private Pool[] pools = null;

        private readonly Dictionary<string, Queue<GameObject>> poolsDict = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            CreateInstance();
        }

        private void Start()
        {
            InstantiateObjectsOnStart();
        }

        private void OnDestroy()
        {
            instance = null;
        }

        public GameObject Spawn(PoolerPrefabNames name, Transform parent)
        {
            ObjectSanityCheck(ref name, out GameObject obj);

            if (obj)
            {
                obj.transform.parent = parent;
                obj.transform.position = parent.position;
                obj.transform.localScale = parent.localScale;
            }
            obj.SetActive(true);

            return obj;
        }

        public GameObject Spawn(PoolerPrefabNames name, Vector3 position)
        {
            ObjectSanityCheck(ref name, out GameObject obj);

            if (obj) obj.transform.position = position;
            obj.SetActive(true);

            return obj;
        }

        public GameObject Spawn(PoolerPrefabNames name, Vector3 position, Quaternion rotation)
        {
            ObjectSanityCheck(ref name, out GameObject obj);

            if (obj)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }
            obj.SetActive(true);

            return obj;
        }

        public GameObject Spawn(PoolerPrefabNames name, Vector3 position, Vector3 scale)
        {
            ObjectSanityCheck(ref name, out GameObject obj);

            if (obj)
            {
                obj.transform.localScale = scale;
                obj.transform.position = position;
            }
            obj.SetActive(true);

            return obj;
        }

        private void ObjectSanityCheck(ref PoolerPrefabNames name, out GameObject obj)
        {
#if UNITY_EDITOR
            if (!poolsDict.ContainsKey($"{name}"))
            {
                Debug.LogError($"{name} does not exist in the dictionary.");
            }
#endif
            obj = poolsDict[$"{name}"].Dequeue();

            if (obj.activeSelf)
            {
                //Put the previous obj back
                poolsDict[$"{name}"].Enqueue(obj);

                obj = Instantiate(obj);
                obj.transform.parent = transform;
            }
            poolsDict[$"{name}"].Enqueue(obj);
        }

        private void InstantiateObjectsOnStart()
        {
            int cloneNum;
            Queue<GameObject> queue;
            GameObject obj;

            foreach (Pool pool in pools)
            {
                queue = new Queue<GameObject>();
                cloneNum = pool.num;

                for (int i = 0; i < cloneNum; i++)
                {
                    obj = Instantiate(pool.prefab);
                    obj.SetActive(false);

                    if (obj.TryGetComponent(out RectTransform rectTrans)) rectTrans.SetParent(transform);
                    else obj.transform.parent = transform;

                    queue.Enqueue(obj);
                }

                poolsDict.Add(pool.prefab.name, queue);
            }
        }

        private void CreateInstance()
        {
            if (Instance == null) instance = this;
            else if (Instance != this) Destroy(Instance.gameObject);
        }
    }
}
