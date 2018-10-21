using NUnit.Framework;

namespace Fuzzy
{
    public class FuzzySetsTest
    {
        [Test]
        [Category("Fuzzy")]
        public void TriangelSet()
        {
            TriangleFuzzySet set = new TriangleFuzzySet(0, 50, 100);
            float dom = set.CalculateDom(50);
            Assert.AreEqual(1.0f, dom);

            dom = set.CalculateDom(25);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(75);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(100);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(0);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(125);
            Assert.AreEqual(0, dom);
        }

        [Test]
        [Category("Fuzzy")]
        public void TrapezoidalSet()
        {
            TrapezodialFuzzySet set = new TrapezodialFuzzySet(10, 20, 40, 50);
            float dom = set.CalculateDom(10);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(15);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(20);
            Assert.AreEqual(1.0f, dom);

            dom = set.CalculateDom(30);
            Assert.AreEqual(1.0f, dom);

            dom = set.CalculateDom(40);
            Assert.AreEqual(1.0f, dom);

            dom = set.CalculateDom(45);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(50);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(60);
            Assert.AreEqual(0, dom);
        }

        [Test]
        [Category("Fuzzy")]
        public void LeftShoulderSet()
        {
            LeftShoulderFuzzySet set = new LeftShoulderFuzzySet(0,20);
            float dom = set.CalculateDom(0);
            Assert.AreEqual(1, dom);

            dom = set.CalculateDom(5);
            Assert.AreEqual(0.75f, dom);

            dom = set.CalculateDom(10);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(15);
            Assert.AreEqual(0.25f, dom);

            dom = set.CalculateDom(20);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(45);
            Assert.AreEqual(0, dom);           
        }

        [Test]
        [Category("Fuzzy")]
        public void RightShoulderSet()
        {
            RightShoulderFuzzySet set = new RightShoulderFuzzySet(0, 20);
            float dom = set.CalculateDom(0);
            Assert.AreEqual(0, dom);

            dom = set.CalculateDom(5);
            Assert.AreEqual(0.25f, dom);

            dom = set.CalculateDom(10);
            Assert.AreEqual(0.5f, dom);

            dom = set.CalculateDom(15);
            Assert.AreEqual(0.75f, dom);

            dom = set.CalculateDom(20);
            Assert.AreEqual(1, dom);

            dom = set.CalculateDom(45);
            Assert.AreEqual(1, dom);
        }
    }
}
