using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MemoryGame
{
    public class ReturnToPool : MonoBehaviour ,IReturnToPool
    {
        private IObjectPool<GameObject> pool { get; set; }

        public void ReturnToPoolGameobject()
        {
            pool.Release(gameObject);
        }

        public void SetPool(IObjectPool<GameObject> poolObj)
        {
            pool = poolObj;
        }
    }
}

