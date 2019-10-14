using NUnit.Framework;
using RateMyP.Helpers;

namespace RateMyP.Tests
    {
    public class StringExtensionsTests
        {
        [Test]
        public void Denationalize_NoLithuanianLetters_StringIntact()
            {
            const string str = "no lithuanian letters";
            Assert.AreEqual(str, str.Denationalize());
            }

        [Test]
        public void Denationalize_SomeLithuanianLetters_LithuanianLettersModified()
            {
            const string str = "Šešios žąsys su šešiais žąsyčiais.";
            Assert.AreEqual("Sesios zasys su sesiais zasyciais.", str.Denationalize());
            }

        [Test]
        public void Denationalize_AllLithuanianLetters_AllLettersModified()
            {
            const string str = "ąĄčČęĘėĖįĮšŠųŲūŪžŽ";
            Assert.AreEqual("aAcCeEeEiIsSuUuUzZ", str.Denationalize());
            }
        }
    }
