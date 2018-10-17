using System;
using System.Collections.Generic;

namespace Mindmaster
{
    public class Sequence
    {
        private const int k_MaxLengthOfSequence = 4;
        private const int k_MinValueOfRange = 1;
        private const int k_MaxValueOfRange = 9;
        private const int k_IndexOfIndexAndElementMatches = 0;
        private const int k_IndexOfElementMatchesOnly = 1;

        private int[] m_Sequence;
        
        public void GenerateSequence()
        {
            m_Sequence = new int[k_MaxLengthOfSequence];
            Random indexGenerator = new Random();
            HashSet<int> setOfUniqueIndexes = new HashSet<int>();
            int NumOfElements = 0;
            while (NumOfElements < k_MaxLengthOfSequence)
            {
                int generatedNum = indexGenerator.Next(k_MinValueOfRange, k_MaxValueOfRange);
                if (setOfUniqueIndexes.Add(generatedNum))
                {
                    m_Sequence[NumOfElements] = generatedNum;
                    NumOfElements++;
                }
            }
        }

        public int[] FindMatchesInTwoSequences(int[] i_SequenceToMatch)
        {
            int numOfIndexAndElementMatches = 0;
            int numOfElementMatchOnly = 0;
            int[] resultArray = new int[2];

            for (int i = 0; i < k_MaxLengthOfSequence; i++)
            {
                int indexOfMatchLetter = Array.IndexOf(m_Sequence, i_SequenceToMatch[i]);
                
                if (indexOfMatchLetter == i)
                {
                    numOfIndexAndElementMatches++;
                    resultArray[k_IndexOfIndexAndElementMatches] = numOfIndexAndElementMatches;
                }
                else if (indexOfMatchLetter != i && indexOfMatchLetter != -1)
                {
                    numOfElementMatchOnly++;
                    resultArray[k_IndexOfElementMatchesOnly] = numOfElementMatchOnly;
                }
            }

            return resultArray;
        }

        public static int MaxSequenceLength
        {
            get { return k_MaxLengthOfSequence; }
        }

        public static int IndexOfElementAndIndexMatch
        {
            get { return k_IndexOfIndexAndElementMatches; }
        }

        public static int IndexOfElementMatchOnly
        {
            get { return k_IndexOfElementMatchesOnly; }
        }

        public int[] TheSequence
        {
            get { return m_Sequence; }
        }
    }
}
