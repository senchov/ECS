using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bots
{
    [CreateAssetMenu(fileName = "BotPrefabHolderSO", menuName = "ScriptableObject/Bots/BotPrefabHolderSO")]
    public class BotPrefabHolderSO : ScriptableObject
    {
        [SerializeField] private KeyValuePair[] BotPrefabByKey;

        public GameObject GetBotPrefab(string key)
        {
            KeyValuePair pair = BotPrefabByKey.FirstOrDefault(x => x.Key == key) ?? new KeyValuePair();
            return pair.Value;
        }

        [Serializable]
        private class KeyValuePair
        {
            public KeyValuePair()
            {
                Key = "base";
                Value = new GameObject("base");
            }
            
            public string Key;
            public GameObject Value;
        }
    }
}