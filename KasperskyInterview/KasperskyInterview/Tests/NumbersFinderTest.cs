using System;
using System.Collections.Generic;
using KasperskyInterview.Tasks;
using NUnit.Framework;

namespace KasperskyInterview.Tests
{
    [TestFixture]
    public class NumbersFinderTest
    {
        [TestCase(null)]
        [TestCase(new[] {1})]
        public void Find_EmptyArray_ThrowsException(int[] givenArray)
        {
            // Arrange
            var finder = new NumbersFinder();

            // Act             
            var ex = Assert.Catch<Exception>(() => finder.Find(givenArray, 0));

            // Assert
            StringAssert.AreEqualIgnoringCase("Входной массив не валиден", ex.Message);
            Assert.AreEqual(typeof(ArgumentException), ex.GetType());
        }

        [Test]
        public void Find_NumbersNotExist_ReturnEmpty()
        {
            // Arrange
            var finder = new NumbersFinder();
            var givenArray = new[] {1, 2, 3};

            // Act
            var result = finder.Find(givenArray, 0);

            // Assert
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void Find_RightNumber_ReturnPairs()
        {
            // Arrange
            var finder = new NumbersFinder();
            var givenArray = new[] { -14, 2, 28, 4, 9, -13, 29, 6, 5, 10, Int32.MaxValue  };
            var expectedResult = new List<FoundNumbers>
            {
                new FoundNumbers {FirstNumber = -13, SecondNumber = 28},
                new FoundNumbers {FirstNumber = 29, SecondNumber = -14},
                new FoundNumbers {FirstNumber = 6, SecondNumber = 9},
                new FoundNumbers {FirstNumber = 10, SecondNumber = 5}
            };

            // Act
            var result = finder.Find(givenArray, 15);

            // Assert
            CollectionAssert.AreEqual(expectedResult, result, new TestNumberFinderComparer());

        }

        private class TestNumberFinderComparer : Comparer<FoundNumbers>
        {
            public override int Compare(FoundNumbers x, FoundNumbers y)
            {
                if (x.FirstNumber != y.FirstNumber)
                    return x.FirstNumber.CompareTo(y.FirstNumber);

                if (x.SecondNumber != y.SecondNumber)
                    return x.FirstNumber.CompareTo(y.SecondNumber);

                return 0;
            }
        }


    }
}
