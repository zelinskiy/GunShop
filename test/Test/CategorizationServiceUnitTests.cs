using System;
using System.Collections.Generic;
using NUnit.Framework;
using GunShop.Services;

namespace GunShop.Test
{
    [TestFixture]
    public class CategorizationServiceUnitTests
    {
        [TestCase("1;2;3", new string[] {"1", "2", "3"})]
        public void CanLoadPossvals(string possvals, IEnumerable<string> expected)
        {
            Assert.That(
                CategorizationService.PossibleValsFromString(possvals), 
                Is.EqualTo(expected));
        }

        [TestCase(new string[] { "1", "2", "3" }, "1;2;3")]
        [TestCase(new string[] { "cat1", "cat2", "cat3" }, "cat1;cat2;cat3")]
        public void CanSavePossvals(IEnumerable<string> possvals, string expected)
        {
            Assert.That(
                CategorizationService.PossibleValsToString(possvals),
                Is.EqualTo(expected));
        }
    }
}
