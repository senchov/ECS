using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace Fuzzy
{
    public class ShootDecisionRulesetShrinked
    {
        private int DistanceToTarget;
        private int Ammo;

        private DecisionConsequent[] DistanceDesireSet;
        private DecisionConsequent[] AmmoDesireSet;

        private float[] ConclusionDistance;
        private float[] ConclusionAmmo;

        private FuzzyLinguisticVariables DistanceToTargetFLV;
        private FuzzyLinguisticVariables AmmoFLV;

        private int MaxX = 3;
        private int MaxY = 3;

        public ShootDecisionRulesetShrinked()
        {
            InitializeConclusionSets();
            CreateAmmoFLV();
            CreateDistanceFLV();
            FillDesireSet();
        }

        private void InitializeConclusionSets()
        {
            ConclusionDistance = new float[MaxX];
            ConclusionAmmo = new float[MaxY];
        }

        private void CreateAmmoFLV()
        {
            IFuzzySet[] ammoFlVFuzzySets = new IFuzzySet[3];
            ammoFlVFuzzySets[0] = new LeftShoulderFuzzySet(0, 10);
            ammoFlVFuzzySets[1] = new TriangleFuzzySet(0, 10, 30);
            ammoFlVFuzzySets[2] = new RightShoulderFuzzySet(10, 30);
            AmmoFLV = new FuzzyLinguisticVariables(ammoFlVFuzzySets);
        }

        private void CreateDistanceFLV()
        {
            IFuzzySet[] distanceFlVFuzzySets = new IFuzzySet[3];
            distanceFlVFuzzySets[0] = new LeftShoulderFuzzySet(25, 150);
            distanceFlVFuzzySets[1] = new TriangleFuzzySet(25, 150, 300);
            distanceFlVFuzzySets[2] = new RightShoulderFuzzySet(150, 300);
            DistanceToTargetFLV = new FuzzyLinguisticVariables(distanceFlVFuzzySets);
        }

        private void FillDesireSet()
        {
            DistanceDesireSet = new DecisionConsequent[MaxX];
            DistanceDesireSet[(int)DistanceToTargetAntecedent.Close] = DecisionConsequent.UnDesirable;
            DistanceDesireSet[(int)DistanceToTargetAntecedent.Medium] = DecisionConsequent.VeryDesirable;
            DistanceDesireSet[(int)DistanceToTargetAntecedent.Far] = DecisionConsequent.UnDesirable;

            AmmoDesireSet = new DecisionConsequent[MaxY];
            AmmoDesireSet[(int)AmmoAntecedent.Low] = DecisionConsequent.UnDesirable;
            AmmoDesireSet[(int)AmmoAntecedent.Ok] = DecisionConsequent.Desirable;
            AmmoDesireSet[(int)AmmoAntecedent.Loads] = DecisionConsequent.VeryDesirable;
        }

        public int GetDesirability(int distanceToTarget, int ammo)
        {
            ExecuteRules(distanceToTarget, ammo);
            float[] finalDesirability = GetFinalDesirability();
            int[] confidenceSet = GetConfidenceSet(finalDesirability);
            float representiveMulConfidenceSum = GetRepresentiveMulConfidenceSum(finalDesirability, confidenceSet);
            float finalDesirabilitySum = finalDesirability.Sum();

            return finalDesirabilitySum > 0 ? (int)(representiveMulConfidenceSum / finalDesirabilitySum) : 0;
        }

        private void ExecuteRules(int distanceToTarget, int ammo)
        {
            for (int i = 0; i < ConclusionDistance.Length; i++)
            {
                float distanceValue = DistanceToTargetFLV.GetSet(i).CalculateDom(distanceToTarget);
                ConclusionDistance[i] = distanceValue;
            }

            for (int i = 0; i < ConclusionAmmo.Length; i++)
            {
                float ammoValue = AmmoFLV.GetSet(i).CalculateDom(ammo);
                ConclusionAmmo[i] = ammoValue;
            }
        }

        private float[] GetFinalDesirability()
        {
            float[] finalDesirability = new float[3];

            for (int i = 0; i < ConclusionDistance.Length; i++)
            {
                int enumIndex = (int)DistanceDesireSet[i];
                finalDesirability[enumIndex] = math.max(finalDesirability[enumIndex], ConclusionDistance[i]);
            }

            for (int i = 0; i < ConclusionAmmo.Length; i++)
            {
                int enumIndex = (int)AmmoDesireSet[i];
                finalDesirability[enumIndex] = math.max(finalDesirability[enumIndex], ConclusionAmmo[i]);
            }

            return finalDesirability;
        }

        private int[] GetConfidenceSet(float[] finalDesirability)
        {
            int[] confidenceSet = new int[3];

            //started desire set left shoulder (25,50)
            int leftPeakValue = 25 + (int)(25.0f * finalDesirability[(int)DecisionConsequent.UnDesirable]);
            confidenceSet[(int)DecisionConsequent.UnDesirable] = leftPeakValue / 2;

            //started desire set triangle (25,50,75)
            int diff = (int)(25.0f * finalDesirability[(int)DecisionConsequent.Desirable]);
            int leftTrapezodialPeak = 25 + diff;
            int rightTrapezodialPeak = 75 - diff;
            confidenceSet[(int)DecisionConsequent.Desirable] = (leftTrapezodialPeak + rightTrapezodialPeak) / 2;

            //started desire set right shoulder (50,75) and the right border 100
            int rightPeakValue = 50 + (int)(25.0f * finalDesirability[(int)DecisionConsequent.VeryDesirable]);
            confidenceSet[(int)DecisionConsequent.VeryDesirable] = (rightPeakValue + 100) / 2;

            return confidenceSet;
        }

        private float GetRepresentiveMulConfidenceSum(float[] finalDesirability, int[] confidenceSet)
        {
            float representiveMulConfidenceSum = 0;
            for (int i = 0; i < confidenceSet.Length; i++)
            {
                representiveMulConfidenceSum += finalDesirability[i] * confidenceSet[i];
            }
            return representiveMulConfidenceSum;
        }
    }
}
