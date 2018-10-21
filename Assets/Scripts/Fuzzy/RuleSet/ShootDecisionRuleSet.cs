using System.Collections.Generic;

namespace Assets.Scripts.Fuzzy.RuleSet
{
    class ShootDecisionRuleSet
    {
        private List<Rule> Rules;

        private int DistanceToTarget;
        private int Ammo;

        public ShootDecisionRuleSet()
        {
            Rules = new List<Rule>();
            Rules.Add(new Rule (DistanceToTargetAntecedent.Far,AmmoAntecedent.Loads,DecisionConsequent.Desirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Far, AmmoAntecedent.Ok, DecisionConsequent.UnDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Far, AmmoAntecedent.Low, DecisionConsequent.UnDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Medium, AmmoAntecedent.Loads, DecisionConsequent.VeryDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Medium, AmmoAntecedent.Ok, DecisionConsequent.VeryDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Medium, AmmoAntecedent.Low, DecisionConsequent.Desirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Close, AmmoAntecedent.Loads, DecisionConsequent.UnDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Close, AmmoAntecedent.Ok, DecisionConsequent.UnDesirable));
            Rules.Add(new Rule(DistanceToTargetAntecedent.Close, AmmoAntecedent.Low, DecisionConsequent.UnDesirable));
        }

        public void SetConditions(int distanceToTarget, int ammo)
        {
            DistanceToTarget = distanceToTarget;
            Ammo = ammo;
        }

        private class Rule
        {
            public Rule(DistanceToTargetAntecedent dis, AmmoAntecedent ammo , DecisionConsequent decision)
            {
                Distance = dis;
                Ammo = ammo;
                Decision = decision;
            }

            public DistanceToTargetAntecedent Distance;
            public AmmoAntecedent Ammo;
            public DecisionConsequent Decision;
        }
    }

    public enum DistanceToTargetAntecedent
    {
        Close,
        Medium,
        Far
    }

    public enum AmmoAntecedent
    {
        Low,
        Ok,
        Loads
    }

    public enum DecisionConsequent
    {
        UnDesirable,
        Desirable,
        VeryDesirable
    }
}
