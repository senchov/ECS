namespace Fuzzy
{
    class FuzzyLinguisticVariables
    {
        private IFuzzySet[] Sets;

        public FuzzyLinguisticVariables(params IFuzzySet[] sets)
        {
            Sets = sets;
        }

        public IFuzzySet GetSet(int index)
        {
            return Sets[index];
        }
    }    
}
