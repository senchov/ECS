using System.Collections.Generic;

using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Fuzzy
{
    public class ShootDecisionRuleSet
    {
        private int DistanceToTarget;
        private int Ammo;
        private DecisionConsequent[,] DesireSet;
        private float[,] ConclusionSet;
        private FuzzyLinguisticVariables DistanceToTargetFLV;
        private FuzzyLinguisticVariables AmmoFLV;

        private int MaxX = 3;
        private int MaxY = 3;

        public ShootDecisionRuleSet()
        {
            ConclusionSet = new float[MaxX, MaxY];

            CreateAmmoFLV();
            CreateDistanceFLV();
            FillDesireSet();
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
            DesireSet = new DecisionConsequent[MaxX, MaxY];
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Loads] = DecisionConsequent.Desirable;
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Ok] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Far, (int)AmmoAntecedent.Low] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Loads] = DecisionConsequent.VeryDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Ok] = DecisionConsequent.VeryDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Medium, (int)AmmoAntecedent.Low] = DecisionConsequent.Desirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Loads] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Ok] = DecisionConsequent.UnDesirable;
            DesireSet[(int)DistanceToTargetAntecedent.Close, (int)AmmoAntecedent.Low] = DecisionConsequent.UnDesirable;
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
            for (int i = 0; i < MaxX; i++)
            {
                for (int j = 0; j < MaxY; j++)
                {                    
                    float distanceValue = DistanceToTargetFLV.GetSet(i).CalculateDom(distanceToTarget);                   
                    float ammoValue = AmmoFLV.GetSet(j).CalculateDom(ammo);
                    ConclusionSet[i, j] = math.min(distanceValue, ammoValue);                    
                }
            }
        }

        private float[] GetFinalDesirability()
        {
            float[] finalDesirability = new float[3];
            for (int i = 0; i < MaxX; i++)
            {
                for (int j = 0; j < MaxY; j++)
                {
                    int enumIndex = (int)DesireSet[i, j];
                    finalDesirability[enumIndex] = math.max(finalDesirability[enumIndex], ConclusionSet[i, j]);
                }
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
