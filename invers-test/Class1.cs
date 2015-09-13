using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invers_test
{
    using NUnit.Framework;
    public class DummyTest
    {
        [Test]
        void dummy_1_success()
        {
            Assert.That(true != false);
        }

        [Test]
        void dummy_2_fail()
        {
            Assert.That(false == true);
        }
    }
}
