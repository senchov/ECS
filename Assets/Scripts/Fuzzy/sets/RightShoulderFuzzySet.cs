namespace Fuzzy
{
    public struct RightShoulderFuzzySet : IFuzzySet
    {
        private int Peak;
        private int LeftOffset;

        public RightShoulderFuzzySet(int leftOffset, int peak)
        {
            LeftOffset = leftOffset;
            Peak = peak;            
        }

        public float CalculateDom(int value)
        {
            if (Peak <= value)
                return 1;

            if (value < LeftOffset)
                return 0;

            float wholeDiff = Peak - LeftOffset;
            float valueDiff = value - LeftOffset;
            return valueDiff / wholeDiff;
        }

        public int GetRepresentiveValue()
        {
            return Peak;
        }
    }
}
