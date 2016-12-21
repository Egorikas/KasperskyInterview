using System;
using System.Collections.Generic;

namespace KasperskyInterview.Tasks
{
    public class FoundNumbers
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
    }

    public class NumbersFinder
    {
        public List<FoundNumbers> Find(int[] givenArray, int givenNumber)
        {
            if(givenArray == null || givenArray.Length < 2)
                throw new ArgumentException("Входной массив не валиден");

            var set = new HashSet<int>();
            var foundNumbers = new List<FoundNumbers>();

            foreach (var i in givenArray)
            {
                var diff = givenNumber - i;
                if (set.Contains(diff))
                {
                    foundNumbers.Add(new FoundNumbers
                    {
                        FirstNumber = i,
                        SecondNumber = diff
                    });
                    set.Remove(diff);
                }
                else
                {
                    set.Add(i);
                }
            }

            return foundNumbers;
        }
    }
}
