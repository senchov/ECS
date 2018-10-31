

using NUnit.Framework;
using UnityEngine;

namespace Fuzzy
{
    class ShootDecisionTest
    {
        [Test]
        [Category("Fuzzy")]
        public void StartTest()
        {
            ShootDecisionRuleSet nineRuleSet = new ShootDecisionRuleSet();
            float nineRuleSetDesirability = nineRuleSet.GetDesirability(200,8);

            ShootDecisionRulesetShrinked sixRuleSet = new ShootDecisionRulesetShrinked();
            float sixRuleSetDesirability = sixRuleSet.GetDesirability(200, 8);

            Debug.LogWarning("nine->" + nineRuleSetDesirability + " six->" + sixRuleSetDesirability);

        }
    }
}
