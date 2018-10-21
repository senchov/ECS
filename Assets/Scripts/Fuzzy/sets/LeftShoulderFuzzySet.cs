namespace Fuzzy
{
    public struct LeftShoulderFuzzySet : IFuzzySet
    {
        private int Peak;
        private int RightOffset;

        public LeftShoulderFuzzySet(int peak, int rightOffset)
        {
            Peak = peak;
            RightOffset = rightOffset;
        }

        public float CalculateDom(int value)
        {
            if (value <= Peak)
                return 1;

            if (RightOffset < value)
                return 0;

            float wholeDiff = RightOffset - Peak;
            float valueDiff = value - Peak;
            return 1 - valueDiff / wholeDiff;
        }

        public int GetRepresentiveValue()
        {
            return Peak;
        }
    }
}
