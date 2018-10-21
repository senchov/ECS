namespace Fuzzy
{
    public struct TriangleFuzzySet : IFuzzySet
    {
        private int LeftOffset;
        private int Peak;
        private int RightOffset;

        public TriangleFuzzySet(int leftOffset, int peak, int rightOffset)
        {
            LeftOffset = leftOffset;
            Peak = peak;
            RightOffset = rightOffset;

        }

        public float CalculateDom(int value)
        {
            if (value < LeftOffset || RightOffset < value)
                return 0;

            float wholeDiff = 0;
            float valueDiff = 0;

            if (LeftOffset <= value && value <= Peak)
            {
                wholeDiff = Peak - LeftOffset;
                valueDiff = value - LeftOffset;
                return valueDiff / wholeDiff;
            }

            wholeDiff = RightOffset - Peak;
            valueDiff = value - Peak;
            return 1 - valueDiff / wholeDiff;
        }

        public int GetRepresentiveValue()
        {
            return Peak;
        }
    }
}
