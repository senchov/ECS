

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
            ShootDecisionRuleSet set = new ShootDecisionRuleSet();
            float desirability = set.GetDesirability(200,8);
            Debug.LogError(desirability);
        }
    }
}
