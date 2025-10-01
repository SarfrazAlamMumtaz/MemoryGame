using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MemoryGame
{
    public class ReturnToPool : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;

        public void ReturnToPoolGameobject()
        {
            // Return to the pool
            pool.Release(gameObject);
        }

    }
}

