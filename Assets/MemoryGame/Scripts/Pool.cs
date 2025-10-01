using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MemoryGame
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private GameObject pollItems;

        IObjectPool<GameObject> m_Pool;
        public IObjectPool<GameObject> PoolGameobject
        {
            get
            {
                if (m_Pool == null)
                {
                    m_Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, gameSetting.collectionChecks, 10, gameSetting.maxPoolSize);
                }
                return m_Pool;
            }
        }

        GameObject CreatePooledItem()
        {
            var go = Instantiate(pollItems);
            var returnToPool = go.GetComponent<ReturnToPool>();
            returnToPool.pool = PoolGameobject;

            return go;
        }

        void OnReturnedToPool(GameObject system)
        {
            system.gameObject.SetActive(false);
        }

        void OnTakeFromPool(GameObject system)
        {
            system.gameObject.SetActive(true);
        }

        void OnDestroyPoolObject(GameObject system)
        {
            Destroy(system.gameObject);
        }
    }
}

