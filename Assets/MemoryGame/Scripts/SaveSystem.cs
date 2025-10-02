using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace MemoryGame
{
    public class SaveSystem : MonoBehaviour
    {
        private string savePath;

        void Awake()
        {
            savePath = Application.persistentDataPath + "/save.json";
        }
        public void OnGameSave(List<Card> cards, GameSaveData gameSaveData)
        {
            SaveCurrentGame(cards, gameSaveData);
        }
        public void OnGameLoad(Action<GameSaveData> OnLoaded,Action OnSaveNotFound)
        {
            GameSaveData loaded = LoadGame();
            if (loaded == null)
            {
                OnSaveNotFound.Invoke();
                return;
            }

            OnLoaded.Invoke(loaded);
        }

        private void SaveCurrentGame(List<Card> cards, GameSaveData gameSaveData)
        {
            foreach (var card in cards)
            {
                CardData cd = new CardData(card.id, card.active);
                gameSaveData.cards.Add(cd);
            }

            SaveGame(gameSaveData);
        }

        private void SaveGame(GameSaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            //Debug.Log("Game Saved to " + savePath);
        }

        private GameSaveData LoadGame()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
                return data;
            }

            //Debug.LogWarning("No save file found.");

            return null;
        }

        public void DeleteSave()
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
        }
    }
}

