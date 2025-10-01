using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryGame
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettingsScriptableObject", order = 2)]
    public class GameSetting : ScriptableObject
    {
        [Header("Card Layout")]
        public int rows = 2;
        public int columns = 2;
        public Vector2 spacing = new Vector2(10f, 10f);

        [Header("Card Polling")]
        public bool collectionChecks = true;
        public int maxPoolSize = 10;
    }
}

