using System.Collections;
using Bots;
using Graphs;
using MovementSystem;
using UnityEngine;
using UnityEngine.Assertions;

public class BotSummonerTest : MonoBehaviour
{
    [RuntimeTestAtribute]
    public IEnumerator Summon()
    {
        GraphHolder graphHolder = GameObject.FindObjectOfType<GraphHolder>();
        CheckAndRenderGraph(graphHolder);

        BotSummoner summoner = GameObject.FindObjectOfType<BotSummoner>();
        Assert.IsNotNull(summoner, "BotSummoner is null");

        GameObject bot = summoner.SummonBot("base", 15);
        Assert.IsNotNull(bot, "bot is null");

        Vector3 nodePos = graphHolder.GetNodePosition(15);
        MoveDataComponent moveData = bot.GetComponent<MoveDataComponent>();
        Assert.IsNotNull(moveData, "MoveDataComponent on bot is null");

        yield return null;
    }

    private void CheckAndRenderGraph(GraphHolder graphHolder)
    {
        Assert.IsNotNull(graphHolder, "GraphHolder is  null");
        graphHolder.RenderGraph();
    }
}