using System.Collections.Generic;

namespace Fuzzy
{
    class ShootDecisionRuleSet
    {
        private int DistanceToTarget;
        private int Ammo;
        private DecisionConsequent[,] DesireSet;
        private float[,] ConclusionSet;
        private FuzzyLinguisticVariables DistanceToTargetFLV;
        private FuzzyLinguisticVariables AmmoFLV;

        public ShootDecisionRuleSet()
        {
            ConclusionSet = new float[3,3];

            DesireSet = new DecisionConsequent[3, 3];
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Loads] = DecisionConsequent.Desirable;
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Ok] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Low] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Loads] = DecisionConsequent.VeryDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Ok] = DecisionConsequent.VeryDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Low] = DecisionConsequent.Desirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Loads] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Ok] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Low] = DecisionConsequent.UnDesirable;

            IFuzzySet[] distanceFlVFuzzySets = new IFuzzySet[3];
            distanceFlVFuzzySets[0] = new LeftShoulderFuzzySet(25, 150);
            distanceFlVFuzzySets[1] = new TriangleFuzzySet(25, 150, 300);
            distanceFlVFuzzySets[2] = new RightShoulderFuzzySet(150, 300);
            DistanceToTargetFLV = new FuzzyLinguisticVariables(distanceFlVFuzzySets);

            IFuzzySet[] ammoFlVFuzzySets = new IFuzzySet[3];
            distanceFlVFuzzySets[0] = new LeftShoulderFuzzySet(0, 10);
            distanceFlVFuzzySets[1] = new TriangleFuzzySet(0, 10, 30);
            distanceFlVFuzzySets[2] = new RightShoulderFuzzySet(10, 30);
            AmmoFLV = new FuzzyLinguisticVariables(ammoFlVFuzzySets);
        }

        private void Deffuzify(int distanceToTarget, int ammo)
        {
            DistanceToTarget = distanceToTarget;
            Ammo = ammo;
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
