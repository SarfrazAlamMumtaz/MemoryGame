using UnityEngine;
using UnityEngine.Pool;

namespace MemoryGame
{
    public interface IReturnToPool
    {
        public void SetPool(IObjectPool<GameObject> poolObj);
        public void ReturnToPoolGameobject();
    }
}

