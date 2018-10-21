namespace Fuzzy
{
    public struct TrapezodialFuzzySet : IFuzzySet
    {
        private int LeftOffset;
        private int LeftPeak;
        private int RightPeak;
        private int RightOffset;

        public TrapezodialFuzzySet(int leftOffset, int leftPeak, int rightPeak, int rightOffset)
        {
            LeftOffset = leftOffset;
            LeftPeak = leftPeak;
            RightPeak = rightPeak;
            RightOffset = rightOffset;
        }

        public float CalculateDom(int value)
        {
            if (value < LeftOffset || RightOffset < value)
                return 0;

            float wholeDiff = 0;
            float valueDiff = 0;

            if (LeftOffset <= value && value < LeftPeak)
            {
                wholeDiff = LeftPeak - LeftOffset;
                valueDiff = value - LeftOffset;
                return valueDiff / wholeDiff;
            }

            if (LeftPeak <= value && value <= RightPeak)
            {
                return 1;
            }

            wholeDiff = RightOffset - RightPeak;
            valueDiff = value - RightPeak;
            return 1 - valueDiff / wholeDiff;
        }

        public int GetRepresentiveValue()
        {
            return (LeftPeak + RightPeak) / 2;
        }
    }
}
