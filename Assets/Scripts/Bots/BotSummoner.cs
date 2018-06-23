using System.Collections;
using System.Collections.Generic;
using Graphs;
using UnityEngine;

namespace Bots
{
    public class BotSummoner : MonoBehaviour
    {
        [SerializeField] private GraphHolder GraphHolder;
        [SerializeField] private BotPrefabHolderSO PrefabHolder;

        public GameObject SummonBot(string botType, int nodeIndex)
        {
            Vector3 botPosition = GraphHolder.GetNodePosition(nodeIndex);
            return Instantiate(PrefabHolder.GetBotPrefab(botType), botPosition, Quaternion.identity);
        }
    }
}